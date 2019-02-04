// Include CsbScaffold
#nowarn "10001"
#load "../../.env/CsbScaffold.fsx"

open FSharp.Plotly
open FSharp.Plotly.StyleParam

//To find help and documentation about the functionality of FSharp.plotly, head over to our docs at http://muehlhaus.github.io/FSharp.Plotly/

//Task1: create a list named 'xValues' which ranges from 0.0 to 12.0 with a stepsize of 0.001 



//Task2: create two lists called 'sinValues' and 'cosValues' by mapping the 'xValues' list with the respective trigonometric function.



//Task3: create two spline plots, plotting each 'sinValues' and 'cosValues' against 'xValues'.


//Task4: combine the two plots created in Task3.



//Task5: stack the two plots created in Task3.



//Task6: 'sinValues' and 'cosValues' against each other using a point plot. 



//Task6:    Define two functions called yAxis and xAxis that have unit as parameter and return a Linear axis with the following properties:
//
//          - The axis is mirrored
//          - The ticking strockes are inside of the returned axis
//          - the grid inside the rturned axis is not shown.
//          - the line of the returned axis is shown.
//
//          hint:   you can create a Linear axis using the LinearAxis.init function and use the withX_AxisStyle/withY_AxisStyle function to use the axis in your plot

//Task7: use the xAxis and yAxis functions to create x and y axis for all plots you created so far.