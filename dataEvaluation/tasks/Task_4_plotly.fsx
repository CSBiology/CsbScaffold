// Include CsbScaffold
#nowarn "10001"
#load "../../.env/CsbScaffold.fsx"
#load "Task_3_2_Deedle.fsx"
// If you want to use the wrappers for unmanaged LAPACK functions from of FSharp.Stats 
// include the path to the .lib folder manually to your PATH environment variable and make sure you set FSI to 64 bit

// use the following lines of code to ensure that LAPACK functionalities are enabled if you want to use them
// fails with "MKL service either not available, or not started" if lib folder is not included in PATH.
//open FSharp.Stats
//FSharp.Stats.Algebra.LinearAlgebra.Service()

open BioFSharp
open Deedle
open FSharpAux
open FSharpAux.IO
open System.IO
open Task_3_2_Deedle
open FSharp.Stats
open FSharp.Plotly



//Task 1: For each timepoint, plot the distribution of the mean values for all proteins
//hint1: you want to create a chart for every column of the frame
//hint2: the correct Chart function is Chart.Histogram(...)
//The standard sizing will make the resulting plot very small. apply adequate styling to the chart.
//Create a combined chart for better comparison
//start with this snippet:
meanAcrossBiologicalReplicates
|> Frame.getNumericCols 

//Task 2: For a proteins of your interest, create a range plot or line plot showing the time course of its abundance and its dispersion.
//hint1: first, get the mean series for your protein of interest. you want to get one row out of the mean frame.
//hint2: do the same for the protein for the standard deviation frame
//hint3: the Chart.Range function needs information about the standard deviation applied to the mean in both directions.
//hint4: example usage of the chart.Range function can be seen at http://muehlhaus.github.io/FSharp.Plotly/range-plots.html

