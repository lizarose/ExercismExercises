(* This is a multi-line comment*)

//Introduction to F#




// ======== "Variables" (but not really) ==========
// The "let" keyword defines an (immutable) value
let myInt = 5
let myFloat = 3.14
let myString = "hello"              //note that no types are needed


// ======== Lists ============
let twoToFive = [2;3;4;5]           //Square brackets create a list with semicolon delimiters
let oneToFive = 1 :: twoToFive      // :: creates list with new 1st element
    //result --> int list = [1;2;3;4;5]

let zeroToFive = [0;1] @ twoToFive  // @ concats two lists

// IMPORTANT: commas are never used as delimiters, only semicolons!


// ======== Functions ========
// The "let" keyword also defines a named function.
let square x = x * x                //Note no parens are used
square 3                            //running the function
    //result --> int = 9

let add x y = x + y                 //Don't use (x,y)! It means something different
add 2 3
    //result --> int = 5

// To define a multiline function, just use indents. No semicolons needed.
let evens list = 
    let isEven x = x%2 = 0          //Define "isEven" as an innter ("nested") function
    List.filter isEven list         //List.filter is a library function
                                    //with two parameters: a boolean function
                                    //and a list to work on


evens oneToFive                     //running the function
    //result --> int list = [2; 4]

// You can use parens to clarify precedence. In this example,
// do "map" first, with two args, then do "sum" on the result.
// Without the parens, "List.map" would be passed as an arg to List.sum
let sumOfSquaresTo100 =
    List.sum ( List.map square [1..100] )
    //result --> int = 338350
                                    //[1..100] --> this creates a list of integers from 1 to 100
                                    //sqaure --> this refers to a function that squares a number
                                        //square has already been defined above
                                    //List.map square [1..100] --> the List.map function applies the square function to each element in the list [1..100]. As a result, it returns a list of squares of numbers from 1 to 100.
                                    //List.sum --> this function sums up all the values in the list produced by List.map square
                                    //parentheses first then apply that to List.sum
                                    //List.map --> this function takes two arguments: a function('square' in this example) and a list([1..100])

(*----->    Pipeline Operator   |>      <-----*)
// You can pipe the output of one operation to the next using "|>"
// Here is the same sumOfSquares function written using pipes
let sumOfSquaresTo100piped = 
    [1..100] |> List.map square |> List.sum   
    //result --> int 338350

(*The pipeline operator |> is used to pass the result of the expression on
the left side as an argument to the function on the right side.
It helps to make the code more readable.*)

(*So a list 1 to 100 is passed in to 'square' where they are squared,
then the list is summed together.*)

(*----->    Composition Operator   >>      <-----*)
//used to compose two functions together
let sumOfSquaresTo100Comp = 
    (List.map square >> List.sum) [1..100]
    //result --> int 338350


(*----->    Anonymous Functions   'fun'      <-----*)
// you can define lambdas (anonymous functions) using the "fun" keyword
let sumOfSquaresTo100Fun =
    [1..100] |> List.map (fun x->x*x) |> List.sum

//'fun' keyword is an anonymous function inline 

// In F# returns are implicit -- no "return" needed. A function always
// returns the value of the last expression used.



// ======== Pattern Matching ========
// Match..with.. is a supercharged case/switch statement.
let simplePatternMatch = 
    let x = "a"
    match x with 
        | "a" -> printfn "x is a"
        | "b" -> printfn "x is b"
        | _ -> printfn "x is something else"   // underscore matches anything

(*allows you to handle different cases based on the structure
or value of an expression
-handling data forms: discriminated unions, lists, tuples*)


(*----->    Some and None      <-----*)
// Some(..) and None are roughly analogous to Nullable wrappers
let validValue = Some(99)
let invalidValue = None
(*Some and None are part of the option type, 
which is used to represent values that might be absent.
The option type is a safer alternative to null values.*)

(*Some(value) --> indicates that there is a value present,
and that the value is wrapped in a Some Constructor.*)

(*None --> represents the absence of a value.*)

//List.tryFind --> this function tries to find an element in a list that satisfies a condition.

// In this example, match..with matches the "Some" and the "None",
// and also unpacks the value in the "Some" at the same time.
let optionPatternMatch input =
   match input with
    | Some i -> printfn "input is an int=%d" i
    | None -> printfn "input is missing"

optionPatternMatch validValue
optionPatternMatch invalidValue



// ========= Complex Data Types =========
(*Complex data types allow you to group and organize data more meaningfully.*)


(*----->    Tuples      <-----*)
(*Allow you to group multiple values together w/out defining a new type.
The elements can have different types.*)
// Tuple types are pairs, triples, etc. Tuples use commas.
let twoTuple = 1,2
let threeTuple = "a",2,true


(*----->    Records      <-----*)
(*Records are similar to structs in other languages.
They are used to group named fields inot a single data structure.*)
// Record types have named fields. Semicolons are separators.
type Person = {First:string; Last:string}
let person1 = {First="john"; Last="Doe"}


(*----->    Discriminated Unions (DU)      <-----*)
(*Allow you to define a type that can hold multiple kinds of values,
each tagged with a specific case.
This is useful for modeling data that can take different forms.*)
// Union types have choices. Vertical bars are separators.
type Temp =
  | DegreesC of float
  | DegreesF of float
let temp = DegreesF 98.6

// Types can be combined recursively in complex ways.
// E.g. here is a union type that contains a list of the same type:
type Employee =
  | Worker of Person
  | Manager of Employee list
let jdoe = {First="John";Last="Doe"}
let worker = Worker jdoe


//Lists --> immmutable and singly linked collections of elements of the same type.
//Arrays --> fixed-size, mutable collections of elements of the same type.
            //allow random access and mutation, but less common in functional programming compared to lists due to mutability.

//Sequences --> are lazily evaluated collections that compute their values on demand. Useful when dealing with potentially large or infinite collections.

//Maps and Sets --> 
                //maps --> collections of key-value pairs, useful for lookups
                //sets --> collections that contain unique elements


// ========= Printing =========
// The printf/printfn functions are similar to the
// Console.Write/WriteLine functions in C#.
printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
printfn "A string %s, and something generic %A" "hello" [1;2;3;4]

// all complex types have pretty printing built in
printfn "twoTuple=%A,\nPerson=%A,\nTemp=%A,\nEmployee=%A"
         twoTuple person1 temp worker

// There are also sprintf/sprintfn functions for formatting data
// into a string, similar to String.Format.
