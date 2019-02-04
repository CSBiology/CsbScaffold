// Include CsbScaffold
#nowarn "10001"
#load "../../.env/CsbScaffold.fsx"
#I @"../../.env/.aux/"

open FSharp.Stats
open FSharp.Plotly

//Task 1.1 Generate a continuous normal distribution with mean = 3.0 and standard deviation = 2.0 and bind it to the name gauss1. (explore the Distributions.Continuous module)

//Task 1.2 Get a random n=1-sample of gauss1 with gauss1.Sample()

//Task 1.3 Describe what the following function does. What does it take/give?
//
let sampleFrom (distribution:Distributions.Distribution<float,float>) sampleSize =
    Array.init sampleSize (fun x -> distribution.Sample())

//Task 1.4 Apply the funcion 'sampleFrom' to gauss1 and a sample size you like.
//Hint: Use the Chart.Histogram function for visualization.

//Task 1.5 Define other distributions and name them gauss2 and gauss3.

//Task 1.6 Create samples of the distributions and combine their Histograms.


//Task 2.1 Define a function called 'meanOfSample' that takes a distribution and a sampleSize (see Task 1.3) and reports the mean of the sample. (explore the Seq module) 

//Task 2.2 Define a function called 'stdDevOfSample' that takes a distribution and a sampleSize (see Task 1.3) and reports the standard deviation of the sample. (explore the Seq module) 

//Task 2.3 Apply both previous defined functions to gauss1 and a sample size you like.

//Task 2.4 Calculate means of different distributions and different sample sizes and compare them.
//Hint1: You can make use of 'map' functions to test several sample sizes at once.
//Hint2: Use FSharp.Plotly to visualise your results.


//Testing  
//Task 3.1 Define two samples of gauss1 and gauss2 with sampleSize 10 and convert them into Vectors using 'Vector.ofArray'.

//Task 3.2 Add the missing parameters

//Task 3.3 Test samples of different distributions, mediate the sample size and compare the p values.
