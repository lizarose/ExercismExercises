(* Basics for F# - from the Lucians Luscious Lasagna exercise*)

(*
    Binding --> assigning a value to a name 
        -immutable, which makes them similar to constants
        -bc F# is statically-typed --> each binding has type at compile
        -defined using the "let" keyword
        -specifying type is optional bc of type inference 
            -usually infer the type based on their value
*)
//binding example:
// Automatically inferred type
let fingers = 10



(*
    Functions are also regular bindings, but with one or more parameters.
    -a function automatically returns its last expression
    -type inference --> analyzes what values the function is called with 
        and what value the function returns
*)

//Automatically inferred types for paramters and return type
let add x y = x + y

//Invoking a function is done by specifying its name and passing arguments for each of the function's parameters
let five = add 2 3




//If a binding's type can't be inferred, compiler will report error
//To fix, add an explicit type annotation to the binding

//Explicit type annotation
let fingers: int = 10

//Explicit type annotation (also for parameters)
let add (x: int) (y: int): int = x + y





//Bindings can only be used AFTER they have been defined
//significant whitespace is used to define scope
    //indenting the code with spaces, relative to the line declaring the binding --> default is to use four spaces for indentation

let toes =
    let left = 5
    let right = 5
    left + right

let multiplyPlusTwo x y =
    let product = x * y
    product + 2

// Trying to access the left, right or product bindings
// here would result in a compile error



//bindings usually organized in modules
//a module groups realted functionality and is defined using the "module" keyword - must precede its bindings:

module Calculator

let pi = 3.14

let add x y = x + y



//can use type inference --> don't specify any types
//can use explicit typing --> specify types















(* Pattern Matching - Guessing Game exercise*)

(*
    -a value can be tested against one or more patterns
    -done through "match" keyword
*)

let describe number = 
    match number with 
    | 0 -> "Zero"
    | 1 -> "One"

(*
    -this looks like switch/case statement







    -varibale pattern --> allows you to capture a value
*)
match number with 
| 0 -> "Zero"
| i -> sprintf "Non zero: %d" i 








(*
    -add additional condition to a pattern --> Guard (clause) using "when" keyword
*)
match number with 
| 0 -> "Zero"
| i when i < 0 -> "Negative number"




(*
    not all possible input will have a matching pattern
        -compiler will detect this and output warning --> Exhaustive Checking
            -to solve, handle all cases
                -Wildcard Pattern --> pattern taht matches on any value
*)
match number with 
| i when i < 0 -> "Negative number"
| _ -> "Positive number"


(*
    pattern matching will test a value against each patter nfrom top to 
    bottom, until it finds a matching pattern and executes the logic
    associated with that pattern
        the order of the patterns matters!
*)











(* Lists - Tracks On Tracks On Tracks exercise*)

(*
    -a list is an immutable collection of zero or more values
    -the values in a list must all have the same type
    -once a list has been constructed, its value can never change
    -any functions/operators that appear to modify a list (such as adding
        an element), will actually return a new list
*)

let empty = []
let singleValue = [5]
let threeValues = ["a"; "b"; "c"]

//the most common way to add an element to a list is through the :: (cons) operator

let twoToFour = [2; 3; 4]
let oneToFour = 1 :: twoToFour
    //result --> [1; 2; 3; 4]



(*
    -a list has a head (the first element) and a tail (everythig after the first element)
    -the tail of a list is itself a list

    -lists are either manipulated by functions and operators defined in the List module, or manually using pattern matching using the list and cons patterns:
*)

let describe list =
    match list with 
    | [] -> "Empty list"
    | head :: tail -> sprintf "Non-empty list with head: %d" head

describe []
    //result --> "Empty list"
describe [1]
    //result --> "Non-empty list with head: 1"
describe [5; 7; 9]
    //result --> "Non-empty list with head: 5"


(*
    -you can also discard a value when pattern matching
    -when you do not care about a value in a specific case or just won't use that value, you use and underscore _ 

    _ should always come last when pattern matching, as every value that 
    doesn't match any of teh other cases will be handled by this case
*)

let describe list =
    match list with
    | [] -> "Empty list"
    | [x] -> "List with one item"
    | [_; y] -> "List with two items (first item ignored)"
    | _ -> "List with many items (all items ignored)"

describe []
    //result --> "Empty list"
describe [1]       
    //result --> "List with one item"
describe [5; 7]     
    //result --> "List with two items (first item ignored)"
describe [5; 7; 9] 
    //result --> "List with many items (all items ignored)"