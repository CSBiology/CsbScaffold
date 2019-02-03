// Include CsbScaffold
#load "../../.env/CsbScaffold.fsx"
#I @"../../.env/.aux/"
#load @"DeedleExtensions.fsx"
#nowarn "10001"

open Deedle
open FSharpAux
open FSharp.Stats

/// Create multiple Series
let firstNames      = Series.ofValues ["Kevin";"Lukas";"Benedikt";"Michael"] 
let coffeesPerWeek  = Series.ofValues [15;12;10;11] 
let lastNames       = Series.ofValues ["Schneider";"Weil";"Venn";"Schroda"]  
let group           = Series.ofValues ["CSB";"CSB";"CSB";"MBS"] 

/// Create Data Frame of collections of Series
let persons = Frame(["fN";"lN";"cpw";"g"],[firstNames;lastNames;coffeesPerWeek;group])

/// Get column "cpw" as Series
let coffeePerWeek' :Series<int,int> = Frame.getCol ("cpw") persons 

/// Group rows by column "g" 
let groupedByG :Frame<string*int,_> = persons |> Frame.groupRowsBy "g"

/// Return new Frame with the Columns "fN", "lN" and "cpw"
let withOutG :Frame<string*int,_> = groupedByG |> Frame.sliceCols ["fN";"lN";"cpw"]

/// Get column "cpw" as Series
let coffeePerWeek'' :Series<string*int,int>= groupedByG |> Frame.getCol ("cpw")

/// Aggregate column "cpw" based on the first level
let coffeePerWeekPerGroup = Series.applyLevel Pair.get1Of2 (Series.values >> Seq.sum) coffeePerWeek''


