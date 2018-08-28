#nowarn "211"
#I "../packages"
#I "../.lib/"

#r "Deedle/lib/net40/Deedle.dll"
#r "./../packages/FSharpAux/lib/netstandard2.0/FSharpAux.dll"

namespace Deedle
open Deedle
open FSharpAux
//open FSharpAux.Collections

[<AutoOpen>]
module Frame =
    
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
