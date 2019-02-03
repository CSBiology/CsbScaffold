#ligth

//************************************************************************************
//* F# for scientific programming                                                    * 
//*                                                                                  *
//************************************************************************************


//+++++++++++++++++++
// Defining function
//+++++++++++++++++++
//
// In functional programming functions are first class citizens.
// Therefore, it is most essential knowing how to define a function in F#.

// *TASK:  -----------------------------------------------------------------------------------------
// Define a function 'double', that takes a value 'v' and returns its double.
// Make sure that the domain is of type 'float' as well as the range of the function.
let task1 x = x


// **SubTASK: 
// Apply the function 'double' to a float point number.
task1
 


//+++++++++++++++++++
// Defining types
//+++++++++++++++++++
//
// In functional programming functions are first class citizens.
// Hower modelling the data structure is also important.

// *TASK:  -----------------------------------------------------------------------------------------
// Define a record type called 'row', that models a row in your data table.
// The column of your data table are 'ProteinId | ProteinName | Function | ProtAbundance_WT | ProtAbundance_MT'
let task2 x = x


// **SubTASK: 
// Instanciate a row containing values you can think of ...
task2


// **SubTASK (optional): 
// Define a create function 'createRow' that takes all the values as parameters and returns a row 
task2



//++++++++++++++++++++++++++++++
// Branching control using 
//    'if ... then ... else ...'
//++++++++++++++++++++++++++++++
//
// *TASK:  -----------------------------------------------------------------------------------------
// Define a function named 'sign' that returns the sign of a number: -1 for negative numbers, +1 for
// positive numbers, and 0 for 0.
// Make sure that the function domain is of type 'float' and the range is type 'int'
let task3 x = x


// *SubTASK:  -----------------------------------------------------------------------------------------
// Apply the function 'sign' to a float point number.
task3







//++++++++++++++++++++++++++++++++++++++++++++++++++
// Mapping from normal to elevated functional space 
//++++++++++++++++++++++++++++++++++++++++++++++++++
//
// The function 'double' can be thought of as an example for a function acting in the normal space.
// It maps from 'float' --> 'float'. 
// Imagine a list of numbers as the elveated space of the normal numeric space 

// *TASK:  -----------------------------------------------------------------------------------------
// Create a list named 'nList' of float point numbers from 1.0 to 100.0
let task4 x = x


// A common task in programming is to apply a function on multiple items to repeate a certain task.
//
// This means to iterates through each element in a  collection calling a function for each element
// and collect the result.
//
// In the functional paradigmn we think of that scenario differently. A function working at the
// normal space must work on the elevated space. Therefore the 'normal-space-function' needs to be
// lifted to the elevated space. That means a new function (generaly refered to as 'map' function)
// is needed that lift a function into the elevated space.
//
//   x -->  y --> z --> []   (elevated space)
//   |      |     |                          
// f(x)   f(y)  f(z)         (normal space)  
//   |      |     |                          
//   x'-->  y'--> z'--> []   (elevated space)
//
// *TASK:  -----------------------------------------------------------------------------------------
// Transform every value in 'nList' into its double by appling the function 'double' to each element
// of the list. Use built-in function of the List module 'List.map'.
// Hint: Try to understand the function signiture 
//                                    (goto: https://msdn.microsoft.com/en-us/library/ee370378.aspx)

// *clasical notation:
let task5 x = x


// *rewrite the classical notation into '|>' pipe notation:
let task5Pipe x = x



//++++++++++++++++++++++++++++++++++++++++++++++
// Aggregating elevated space into normal space
//++++++++++++++++++++++++++++++++++++++++++++++
// 
// Suppose you need to write a function that aggregates all values in an elevated space and returns
// a value of normal space. A simple but concreate example would be to sum a list of numeric values.
//
// The most basic and powerful function that reduces the elevated space into the normal space by
// threading state through an iteration has the generic name 'fold'.
//
//    :      ---- fold ---->       f(state,.)           --   
//   / \                          / \                    |
//  a   :                        a   f(state,a)          |
//     / \                          / \                  |-- (elevated space)
//    b   :                        b   f(state,b)        |
//       / \                          / \                |
//      c   :                        c   f(state,c)     --
//         / \                          / \      
//      ...   []                     ...   z          ---- (normal space)  
       
// *TASK:  -----------------------------------------------------------------------------------------
// Transform the list named 'nlist' to its sum. Use built-in function of the List module 'List.fold'
// Hint: Try to understand the function signiture 
//                                    (goto: https://msdn.microsoft.com/en-us/library/ee353894.aspx)
let task6 x = x 