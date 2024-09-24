(*Partial Application - How to Fix Some of a Function's Parameters*)

(*
    -complicated functions with many parameters can have some of them fixed
        or "baked in" and yet leave other parameters open
*)

//Trivial Function:
// define a adding function
let add x y = x + y

// normal use
let z = add 1 2

//call it but only use one parameter
let add42 = add 42

(*
    The result is "42" baked in and now only takes one parameter instead of 2. This is partial application.
        -any function, you can "fix" some of the parameters and leave 
            other ones open to filled in later
*)
// use the new function
add42 2
add42 3


(*
    The “before” and “after” actions are passed in as explicit parameters as well as the function and its input.
*)

let genericLogger before after anyFunc input =
   before input               //callback for custom behavior
   let result = anyFunc input //evaluate the function
   after result               //callback for custom behavior
   result                     //return the result



(*
    This is more flexible. 
*)


let add1 input = input + 1

// reuse case 1
genericLogger
    (fun x -> printf "before=%i. " x) // function to call before
    (fun x -> printfn " after=%i." x) // function to call after
    add1                              // main function
    2                                 // parameter

// reuse case 2
genericLogger
    (fun x -> printf "started with=%i " x) // different callback
    (fun x -> printfn " ended with=%i" x)
    add1                              // main function
    2                                 // parameter




// define a reusable function with the "callback" functions fixed
let add1WithConsoleLogging =
    genericLogger
        (fun x -> printf "input=%i. " x)
        (fun x -> printfn " result=%i" x)
        add1
        // last parameter NOT defined here yet!



(*
    Partial Application --> calling a function w/ fewer arguments than it
        expects, returning a new function that takes the remaining 
        arguments
            -allows you to "pre-fill" some of the arguments of a function 
                and create a new, specialized function

        -this is a key feature of currying, where every function in F# is treated as a series of one argument functions taht return another function

        -makes it easy to create customzed functions by providing only some of the arguments
*)
let functionName arg1 arg2 = arg1 + arg2

let addFive = functionName 5
//addFive is a function that takes one argument and adds 5 to it




let add x y = x + y

let addTen = add 10

printfn "%d" (addTen 5)
    //result --> 15

(*
    The function "add" is partially applied with the value 10, and the new 
    function "addTen" only needs the second argument(y)
*)