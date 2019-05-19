#nowarn "211"
#I "../packages"
#I "../.lib/"

#r "Deedle/lib/net40/Deedle.dll"
#r "./../packages/FSharpAux/lib/netstandard2.0/FSharpAux.dll"

namespace Deedle
open Deedle
open Deedle.Vectors
open Deedle.Indices
open Deedle.Indices.Linear
open FSharpAux
//open FSharpAux.Collections

[<AutoOpen>]
module Frame =

    /// A generic transformation that works when at most one value is defined
    let private atMostOne = 
        { new IBinaryTransform with
            member vt.GetFunction<'R>() = (fun (l:OptionalValue<'R>) (r:OptionalValue<'R>) -> 
              if l.HasValue && r.HasValue then invalidOp "Combining vectors failed - both vectors have a value."
              if l.HasValue then l else r)
            member vt.IsMissingUnit = true }
        |> VectorListTransform.Binary

    let private transformColumn (vectorBuilder:IVectorBuilder) scheme rowCmd (vector:IVector) = 
      { new VectorCallSite<IVector> with
          override x.Invoke<'T>(col:IVector<'T>) = 
            vectorBuilder.Build<'T>(scheme, rowCmd, [| col |]) :> IVector }
      |> vector.Invoke

    /// Reorder elements in the index to match with another index ordering after applying the given function to the first index
    let private reindexBy (keyFunc : 'RowKey1 -> 'RowKey2) (index1:IIndex<'RowKey1>) (index2:IIndex<'RowKey2>) vector = 
        let relocations = 
                seq {  
                    for KeyValue(key, newAddress) in index1.Mappings do
                    let key = keyFunc key
                    let oldAddress = index2.Locate(key)
                    if oldAddress <> Addressing.Address.invalid then 
                        yield newAddress, oldAddress }
        Vectors.Relocate(vector, index1.KeyCount, relocations)

    ///Returns all possible combinations of second and first frame keys sharing the same keyfunction result
    let private combineIndexBy (keyFunc1 : 'RowKey1 -> 'SharedKey) (keyFunc2 : 'RowKey2 -> 'SharedKey) (index1:IIndex<'RowKey1>) (index2:IIndex<'RowKey2>) =
        // Group by the shared key
        let g1 = index1.Keys |> Seq.groupBy keyFunc1
        let g2 = index2.Keys |> Seq.groupBy keyFunc2
    
        //Intersect over the shared Keys
        let s1,s2 = g1 |> Seq.map fst |> Set.ofSeq, g2 |> Seq.map fst |> Set.ofSeq    
        let keyIntersect =  Set.intersect s1 s2 

        //For each shared key, create all possible combinations of index1 and index2 keys
        let m1,m2 = g1 |> Map.ofSeq,g2 |> Map.ofSeq
        let newKeys = 
            keyIntersect
            |> Seq.collect (fun key -> 
                    Map.find key m1
                    |> Seq.collect(fun k1 -> 
                        Map.find key m2
                        |> Seq.map (fun k2 -> 
                            key,k1,k2)
                        )
                )

        //Return new index
        LinearIndexBuilder.Instance.Create(newKeys,None)
  
    /// Create transformation on indices/vectors representing the align operation
    let private createAlignTransformation (keyFunc1 : 'RowKey1 -> 'SharedKey) (keyFunc2 : 'RowKey2 -> 'SharedKey) (thisIndex:IIndex<_>) (otherIndex:IIndex<_>) thisVector otherVector =
        let combinedIndex = combineIndexBy keyFunc1 keyFunc2 thisIndex otherIndex
        let rowCmd1 = reindexBy (fun (a,b,c) -> b) combinedIndex thisIndex thisVector
        let rowCmd2 = reindexBy (fun (a,b,c) -> c) combinedIndex otherIndex otherVector
        combinedIndex,rowCmd1,rowCmd2

    
    let distinctRowValues colName (df:Frame<int,_>) = 
        df
        |> Frame.groupRowsByString colName
        |> Frame.applyLevel fst (fun os -> os.FirstValue())
        |> Frame.indexRowsOrdinally


    // Appends a given series by a key it's value 
    let append (s:Series<'key,'value>) key value =
        s
        |> Series.observations
        |> Seq.append [(key,value)]
        |> Series.ofObservations



    let composeRowsBy f (keyColName:'a) (valueColName:'a) (table:Frame<_,'a>)=
        let keys =
            table.GetColumn<'a>(keyColName)
            |> Series.values
        let values = 
            table.GetColumn<'a>(valueColName)
            |> Series.values
    
        Seq.zip keys values
        |> Map.compose
        |> Map.map (fun k v -> f v )
        |> Map.toSeq
        |> Series.ofObservations


    let decomposeRowsBy f colName (df:Frame<int,_>) =
        df
        |> Frame.groupRowsByString colName
        |> Frame.rows
        |> Series.observations
        |> Seq.collect (fun (k,os) -> f k
                                      |> Seq.mapi (fun i v -> let k',i' = k
                                                              (k',i'+ i),append os "decomposed" (v))) // * (box v)
        |> Frame.ofRows
        |> Frame.indexRowsOrdinally
    
 
     // Frame toJaggedArray
    let Frame_ofJaggedArrayCol (rowNames:'rKey seq) (colNames:'cKey seq) (colJarray:'value array array) =       
        //TODO:
        // If length
    
        colJarray
        |> Seq.map2 (fun colKey arr -> colKey,Series(rowNames, arr)  ) colNames 
        |> frame 


    /// Align two data frames by a shared key received through mapping functions. 
    ///
    /// The columns of the joined frames must not overlap and their rows are aligned and multiplied
    /// by the shared key. The keyFuncs are used to map the rowKeys of the two frames to a shared key. 
    ///
    /// The key of the resulting frame will be a triplet of shared key and the two input keys.
    let align (keyFunc1 : 'RowKey1 -> 'SharedKey) (keyFunc2 : 'RowKey2 -> 'SharedKey) (frame1 : Frame<'RowKey1, 'TColumnKey>) (frame2 : Frame<'RowKey2, 'TColumnKey>) =  
        //Get needed transformation objects and data form the Frame
        let index1 = frame1.RowIndex
        let index2 = frame2.RowIndex
        let indexBuilder = LinearIndexBuilder.Instance
        let vectorbuilder = ArrayVector.ArrayVectorBuilder.Instance
        let data1 = frame1.GetFrameData().Columns |> Seq.map snd |> Seq.toArray |> vectorbuilder.Create
        let data2 = frame2.GetFrameData().Columns |> Seq.map snd |> Seq.toArray |> vectorbuilder.Create
        // Intersect mapped row indices and get transformations to apply to input vectors
        let newRowIndex, rowCmd1, rowCmd2 = 
          createAlignTransformation keyFunc1 keyFunc2 index1 index2 (Vectors.Return 0) (Vectors.Return 0)
        // Append the column indices and get transformation to combine them
        let newColumnIndex, colCmd = 
            indexBuilder.Merge( [(frame1.ColumnIndex, Vectors.Return 0); (frame2.ColumnIndex, Vectors.Return 1) ], atMostOne)
        // Apply transformation to both data vectors
        let newData1 = data1.Select(transformColumn vectorbuilder newRowIndex.AddressingScheme rowCmd1)
        let newData2 = data2.Select(transformColumn vectorbuilder newRowIndex.AddressingScheme rowCmd2)
        // Combine column vectors a single vector & return results
        let newData = vectorbuilder.Build(newColumnIndex.AddressingScheme, colCmd, [| newData1; newData2 |])
        Frame(newRowIndex, newColumnIndex, newData, indexBuilder, vectorbuilder)