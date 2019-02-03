// Include CsbScaffold
#nowarn "10001"
#load "../../.env/CsbScaffold.fsx"
#I @"../../.env/.aux/"


open FSharp.Plotly
let x = [0. .. 0.001 .. 12.65]
let y = x |> List.map sin

Chart.Spline(x,y)
|> Chart.withTitle("Simple line plot showing sin(x)")
|> Chart.withX_AxisStyle("x",Showline=true,Showgrid=false)
|> Chart.withY_AxisStyle("y",Showline=true,Showgrid=false)
|> Chart.SaveHtmlAs(@"C:\Users\Kevin\source\repos\CSBlog\Charts\Plotly\ExamplePlot1.html")
open FSharp.Stats

//generation of normal distributions
let gauss1 = Distributions.Continuous.normal 3. 2.0
let gauss2 = Distributions.Continuous.normal 3. 0.5

//get a sample of normal distributions (n=1)
gauss1.Sample()

//generates samples of sampleSize and distribution
let sampleFrom (distribution:Distributions.Distribution<float,float>) sampleSize =
    Vector.init sampleSize (fun x -> distribution.Sample())

//example of 'sampleFrom'
sampleFrom gauss1 50

//calculates mean of sample
let meanOfSample distribution sampleSize =
    sampleFrom distribution sampleSize
    |> Seq.mean

//example of 'meanOfSample'
meanOfSample gauss1 7

//1.1 define your own normal distribution with mean <> 3 and stDev < 0.8
//1.2 write a function that takes a distribution and a sample size and reports the standard deviation
//1.3 calculate means of different distributions and different sample sizes and compare them




//testing function 
//2.1 add missing parameters
Testing.TTest.twoSample 


//2.2 test samples of different distributions (regarding to p value) and mediate the sample size
//Hint: You can make use of map functions to test several sample sizes at once