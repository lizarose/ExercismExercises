(*Type Inference and How to Avoid Getting Distracted By Complex Type Syntax*)

(*
    Type inference is a technique used to greatly reduce the number of 
    type annontations that need to be explicitly sepecified in code.

*)

let Where source predicate =
    //use the standard F# implementation
    Seq.filter predicate source

let GroupBy source keySelector =
    //use the standard F# implementation
    Seq.groupBy keySelector source

(*
    *You might notice that the standard F# implementations for “filter” and “groupBy” have the parameters in exactly the opposite order from the LINQ implementations used in C#. The “source” parameter is placed last, rather than first. 
*)

(*
    Type inference algorithm is excellent at gathering information from amny sources to determin the types.

    In the following example, it correctly deduces that the list value
    is a list of strings.
*)
let i = 1
let s = "hello"
let tuple = s,i       // pack into tuple
let s2,i2 = tuple     // unpack
let list = [s2]       // type is string list

(*
    And in this example, it correctly deduces that the sumLengths function 
    takes a list of strings and returns an int.
*)
let sumLengths strList =
    strList |> List.map String.length |> List.sum

// function type is: string list -> int