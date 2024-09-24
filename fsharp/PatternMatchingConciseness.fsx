(*Pattern Matching for Conciseness*)

(*
    Pattern matching can be match..with (switch/case), but it can also
    be more general like comparing expressions by matching on values,
    conditions, and types, and then assign or extract values, all at 
    the same time.

    Pattern Matching for Binding Values to Expressions:
*)
//matching tuples directly
let firstPart, secondPart, _ =  (1,2,3)  // underscore means ignore

//matching lists directly
let elem1::elem2::rest = [1..10]       // ignore the warning for now

//matching lists inside a match..with
let listMatcher aList =
    match aList with
    | [] -> printfn "the list is empty"
    | [firstElement] -> printfn "the list has one element %A " firstElement
    | [first; second] -> printfn "list is %A and %A" first second
    | _ -> printfn "the list has more than two elements"

listMatcher [1;2;3;4]
listMatcher [1;2]
listMatcher [1]
listMatcher []

(*
    You can also bind values to the inside of complex structures suchs as
    records.
*)

// create some types
type Address = { Street: string; City: string; }
type Customer = { ID: int; Name: string; Address: Address}

// create a customer
let customer1 = { ID = 1; Name = "Bob";
      Address = {Street="123 Main"; City="NY" } }

// extract name only
let { Name=name1 } =  customer1
printfn "The customer is called %s" name1

// extract name and id
let { ID=id2; Name=name2; } =  customer1
printfn "The customer called %s has id %i" name2 id2

// extract name and address
let { Name=name3;  Address={Street=street3}  } =  customer1
printfn "The customer is called %s and lives on %s" name3 street3






(*
    Pattern matching allows you to compare values against a set of patterns
    and execute code based on which pattern matches. It's much more than
    a simple switch or if-else statement, as it can deconstruct data 
    structures, check types, and extract values all in one concise 
    expression.

    Syntax:
*)
match value with 
| pattern1 -> result1
| pattern2 -> result2
| pattern3 -> result3

(*
    -value --> the input being matched against the patterns
    -pattern1 --> the patterns to be matched
    -result1 --> the expressions that will be executed if the corresponding
                    pattern matches
*)





(*
    Pattern Matching with Tuples:
    -useful for working with pairs or larger collections of values
*)

let describePair (x, y) = 
    match (x, y) with
    | (0, 0) -> "Both are zero"
    | (0, _) -> "First is zero"
    | (_, 0) -> "Second is zero"
    | _      -> "Neither is zero" 

printfn "%S" (describePair (0, 5))
    //result --> "First is zero"
printfn "%S" (describePair (1, 0))
    //result --> "Second is zero"





(*
    Pattern Matching with Lists
    -can be used to deconstruct lists
*)
let describeList 1st = 
    match 1st with
    | [] -> "Empty list"
    | [x] -> sprintf "Single element: %d" x
    | [x; y] -> sprintf "Two elements: %d and %d" x y 
    | _ -> "A list with more than two elements"

printfn "%S" (describeList [])
    //result --> "Empty list"
printfn "%S" (describeList [1])
    //result --> "Single element: 1"
printfn "%S" (describeList [1; 2])
    //result --> "Two elements: 1 and 2"
printfn "%S" (describeList [1; 2; 3])
    //result --> "A list with more than two elements"



(*
    Pattern Matching with Records
*)
type Person = { Name: string; Age: int }

let describePerson person = 
    match person with
    | { Name = "Alice"; Age = 30 } -> "This is Alice, who is 30 years old."
    | { Name = name; Age = age } -> sprintf "%s is %d years old." name age

let alice = { Name = "Alice"; Age = 30 }
let bob = { Name = "Bob"; Age = 25 }

printfn "%s" (describePerson alice)
    //result --> "This is Alice, who is 30 years old."
printfn "%s" (describePerson bob)
    //result --> "Bob is 25 years old."




(*
    Pattern Matching with Options
    -F# uses the Option type for values that might or might not exist
    (similar to Nullable)
*)
let describeOption opt = 
    match opt with
    | Some x -> sprintf "The value is %d" x
    | None -> "No value"

printfn "%s" (describeOption (Some 42))
    //result --> "The value is 42"
printfn "%s" (describeOption None)
    //result --> "No value"







(*
    Pattern Matching on Discriminatd Unions
*)
type Shape = 
    | Circle of radius: float
    | Rectangle of width: float * height: float

let area shape =
    match shape with
    | Circle radius -> System.Math.PI * radius * radius
    | Rectangle (width, height) -> width * height

let circle = Circle 5.0
let rectangle = Rectangle (4.0, 6.0)

printfn "Circle area: %f" (area circle)
    //result --> "Circle area: 78.54"
printfn "Rectangle area: %f" (area rectangle)
    //result --> "Rectangle area: 24.00"


(*
    Pattern Matching on Types (Types Tests)
*)
let describeType x =
    match x with
    | :? int -> "It's an int"
    | :? string -> "It's a string"
    | _ -> "Unknown type"

printfn "%s" (describeType 123)
    //result --> "It's an int"
printfn "%s" (describeType "abc")
    //result --> "It's a string"



(*
    Guards in Pattern Matching
*)
let classifyNumber x = 
    match x with
    | x when x < 0 -> "Negative"
    | 0 -> "Zero"
    | x when x > 0 -> "Positive"

printfn "%s" (classifyNumber -5)
    //result --> "Negative"
printfn "%s" (classifyNumber 0)
    //result --> "Zero"
printfn "%s" (classifyNumber 10)
    //result --> "Positive"



