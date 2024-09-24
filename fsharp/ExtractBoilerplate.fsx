(*Using Functions to Extract Boilerplate Code - DRY principle*)

let product n =
    let initialValue = 1
    let action productSoFar x = productSoFar * x
    [1..n] |> List.fold action initialValue

//test
product 10

let sumOfOdds n =
    let initialValue = 0
    let action sumSoFar x = if x%2=0 then sumSoFar else sumSoFar+x
    [1..n] |> List.fold action initialValue

//test
sumOfOdds 10

let alternatingSum n =
    let initialValue = (true,0)
    let action (isNeg,sumSoFar) x = if isNeg then (false,sumSoFar-x)
                                             else (true ,sumSoFar+x)
    [1..n] |> List.fold action initialValue |> snd

//test
alternatingSum 100

(*
    All 3 of these functions have the same pattern:
        1. set up initial value
        2. set up action function that will be performed on each element
            inside of the loop
        3. call the library function List.fold
            -this starts with the initial value and then runs the action
                function for each element in the list 


    Action Function:
        -always has 2 paramenters
            1. running total (state)
            2. list element to act on (called "x" in the above example)


    By using List.fold and avoiding any loop logic, benefits gained:
        - the key program logic is emphasized and made explicit
            -important differences between the functions become clear
        -boilerplate loop code has been eliminated
            -code is more condensed
        -there can never be an error in the loop logic
*)





(* "Fold" *)

let sumOfSquaresWithFold n =
    let initialValue = 0
    let action sumSoFar x = sumSoFar + (x*x)
    [1..n] |> List.fold action initialValue

//test
sumOfSquaresWithFold 100



type NameAndSize= {Name:string;Size:int}

let maxNameAndSize list =

    let innerMaxNameAndSize initialValue rest =
        let action maxSoFar x = if maxSoFar.Size < x.Size then x else maxSoFar
        rest |> List.fold action initialValue

    // handle empty lists
    match list with
    | [] ->
        None
    | first::rest ->
        let max = innerMaxNameAndSize first rest
        Some max


(*
    The F# code has two parts:
        1. the innerMaxNameAndSize function is similar to what we have seen before.
        2. the second bit, match list with, branches on whether the list is empty or not. With an empty list, it returns a None, and in the non-empty case, it returns a Some. Doing this guarantees that the caller of the function has to handle both cases.
*)

//test
let list = [
    {Name="Alice"; Size=10}
    {Name="Bob"; Size=1}
    {Name="Carol"; Size=12}
    {Name="David"; Size=5}
    ]
maxNameAndSize list
maxNameAndSize []

(*
    Don't need to write that ^ as F# has a maxBy function 
*)

// use the built in function
list |> List.maxBy (fun item -> item.Size)
[] |> List.maxBy (fun item -> item.Size)

(*
    maxBy function doesn't handle empty lists well tho so need to wrap it
*)

let maxNameAndSize list =
    match list with
    | [] ->
        None
    | _ ->
        let max = list |> List.maxBy (fun item -> item.Size)
        Some max





(*
    List.fold is a function for aggregating or reducing a list into a single value.

    Syntax:
        - folder --> a function that takes 2 arguments
            1. the acccumulator --> stores the running total or state, and      current list element
            2. initialValue --> starting value of the accumulator
            3. list --> the list of elements to fold over
*)


let numbers = [1; 2; 3; 4; 5]
let sum = List.fold (fun acc x -> acc + x) 0
numbers
printfn "Sum: %d" sum 


(*
    - fun acc x -> acc + x --> this is the folder function where acc is 
            the accumulator, and x is the current element from the list.
                -this function adds x to acc
    - 0 --> this is the initial value of the accumulator
    - the result is 15, which is the sum of [1; 2; 3; 4; 5]
*)