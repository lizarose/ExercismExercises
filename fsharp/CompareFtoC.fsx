(*Comparing F# with C#: A Simple Sum*)



//Sum the squares from 1 to N:

let square x = x * x                //define the square function

let sumOfSquares n =                //define the sumOfSquares function
    [1..n] |> List.map square |> List.sum

sumOfSquares 100                    //run function
    //result --> int: 338350
(* [1..n] --> this creates a list of integers from 1 to n(inclusive) - this is called a range expression.

 List.map square --> applies the given function(square), to each element in the list.

 |> List.sum --> this has a pipe operator( |> ), which is used to pass the result of the previous operation(which would be a list of squared values),
 and this calculates the sum of the list.*)

 (*Pipe Operator --> |> --> think of it as a conveyor belt. You are starting with the first task, then taking that output and "piping" it through to the next, moving everything down the line.*)

 (*In F#, it is common for entire functions to be written on one line,
 as the "square" function is. The "sumOfSquares" function could also have
 been written on one line. In C#, this is frowned upon.
 
 When a function in F# does have multiple lines, use indents to indicate
 a block of code, which eliminates the need for braces.*)

//sumOfSquares could have also been written this way:
let sumOfSquares n = 
    [1..n]
    |> List.map square
    |> List.sum 
//they both read the same way



(*No Type Declarations*)
(*In F#, looks like an untyped language, but it is actually jst as type-safe
as C#, in fact, even more so! 
F# uses a technique called "type inference" to infer the types you are
using from their context. It reduces the code complexity.

In the example above,
the type inference algorithm notes that we started with a list of integers.
That implies that the square function and the sum function must be taking ints as well, and that the final value must be an int. 
You can see what the infered types are by looking at the result of the 
compilation in the interactive window:

    val square : int -> int
    
This means that the "square" function takes an int and returns an int.*)

//Using a Float:
let squareF x = x * x

let sumOfSquaresF n = 
    [1.0 ..n] |> List.map squareF |> List.sum

sumOfSquaresF 100.0 
    //result --> float = 338350.0
(*Type checking is very strict, so if you want to use a float,
you must make sure every number is a float.*)



(*Interactive Development*)
(*F# has an interactive window where you can test the code immediately
and play around with it. C# does not have an easy way to do this.
The interactivity encourages an incremental approach to coding
that can become addictive!

Many people claim that designing code interactively enforces good design 
practices such as decoupling and explicit dependencies, and therfore, code 
that is suitable for interactive evaluation will also be code that is easy
to test.*)


(*Many C# developers may find this trivial and resort back to loops when 
the logic becomes more complicated. 
In F#, you will almost never see explicit loops.*)