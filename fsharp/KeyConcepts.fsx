(*Four Key Concepts + any extra I find*)

(*
    Theme: 
        conciseness, convenience, correctness, concurrency, completeness

    -Function-oriented rather than object-oriented
    -Expressions rather than statements
    -Algebraic types for creating domain models
    -Pattern matching for flow of control
*)

(* 
    Function-Oriented Rather Than Object-Oriented:
    As you might expect from the term “functional programming”, functions are everywhere in F#.
    
    Of course, functions are first class entities, and can be passed around like any other value:
*)
let square x = x * x

// functions as values
let squareclone = square
let result = [1..10] |> List.map squareclone

// functions taking other functions as parameters
let execFunction aFunc aParam = aFunc aParam
let result2 = execFunction square 12

(*
    Benefits:
        -building with composition:
            -Composition is the ‘glue’ that allows us build larger systems 
            from smaller ones. Composition is used to build basic functions,
             and then functions that use those functions, and so on. And
              the composition principle doesn’t just apply to functions,
               but also to types(the product and sum types discussed below).
        -factoring and refactoring:
            - The ability to factor a problem into parts depends how easily
             the parts can be glued back together. 
        -good design:
            - Many of the principles of good design, such as “separation of
             concerns”, “single responsibility principle”, “program to an 
             interface, not an implementation”, arise naturally as a result
              of a functional approach. And functional code tends to be 
              high level and declarative in general.
*)

(*
    Expressions Rather Than Statements:
    In functional languages, there are no statements, only expressions. 

    F# is similar to SQL as every function definition is a sinlge expression
    not a set of statements. It's more compact and safer than using statements. Statements don't return values so you have to use temporary variables that are assigned to from within statement bodies.
*)

(*
    Algebraic Types:
    The type system in F# is based on the concept of algebraic types. That is, new compound types are built by combining existing types in two different ways:
        -First, a combination of values, each picked from a set of types. These are called “product” types.
        -Or, alternately, as a disjoint union representing a choice between a set of types. These are called “sum” types.
*)
/declare it
type IntOrBool =
  | IntChoice of int
  | BoolChoice of bool

//use it
let y = IntChoice 42
let z = BoolChoice true

(*
    These “choice” types are not available in C#, but are incredibly useful 
    for modeling many real-world cases, such as states in a state machine 
    (which is a surprisingly common theme in many domains).
    And by combining “product” and “sum” types in this way, it is easy to 
    create a rich set of types that accurately models any business domain. 
*)

(*
    Pattern Matching For Flow of Control:
    Most imperative languages offer a variety of control flow statements for branching and looping:
        - if-then-else (and the ternary version bool ? if-true : if-false
        - case or switch statements
        - for and foreach loops, with break and continue
        - while and until loops
        -and even the dreaded goto

    F# does support some of these, but F# also supports the most general 
    form of conditional expression, which is pattern-matching.
    
A typical matching expression that replaces if-then-else looks like this:
*)
match booleanExpression with
| true -> // true branch
| false -> // false branch

//And the replacement of switch might look like this:
match aDigit with
| 1 -> // Case when digit=1
| 2 -> // Case when digit=2
| _ -> // Case otherwise

//Finally, loops are generally done using recursion, and typically look something like this:
match aList with
| [] ->
     // Empty case
| first::rest ->
     // Case with at least one element.
     // Process first element, and then call
     // recursively with the rest of the list

//Although the match expression seems unnecessarily complicated at first, you’ll see that in practice it is both elegant and powerful.

(*
    Pattern Matching With Union Types:
    We mentioned above that F# supports a “union” or “choice” type. This is 
    used instead of inheritance to work with different variants of an 
    underlying type. Pattern matching works seamlessly with these types to 
    create a flow of control for each choice.
    
    In the following example, we create a Shape type representing four 
    different shapes and then define a draw function with different 
    behavior for each kind of shape. This is similar to polymorphism in an 
    object oriented language, but based on functions.
*)
type Shape =        // define a "union" of alternative structures
    | Circle of radius:int
    | Rectangle of height:int * width:int
    | Point of x:int * y:int
    | Polygon of pointList:(int * int) list

let draw shape =    // define a function "draw" with a shape param
  match shape with
  | Circle radius ->
      printfn "The circle has a radius of %d" radius
  | Rectangle (height,width) ->
      printfn "The rectangle is %d high by %d wide" height width
  | Polygon points ->
      printfn "The polygon is made of these points %A" points
  | _ -> printfn "I don't recognize this shape"

let circle = Circle(10)
let rect = Rectangle(4,5)
let point = Point(2,3)
let polygon = Polygon( [(1,1); (2,2); (3,3)])

[circle; rect; polygon; point] |> List.iter draw

(*
    "Of" Keyword --> used to declare a discriminated union Shape,
    which can have values of int. Can have multiple types like 
                    float * length : float

                --> can be used to delegate 
                --> can be used for exception types
*)


(*
    Behavior-Oriented Design vs Data-Oriented Design:
    But in a pure functional design there are no objects and no behavior. 
    Data types do not have any behavior associated with them, and 
    functions do not contain data – they just transform data types into 
    other data types.

    In this case, Circle and Rectangle are not actually types. The only 
    type is Shape – a choice, a discriminated union – and these are 
    various cases of that type.
    In order to work with the Shape type, a function needs to handle each 
    case of the Shape, which it does with pattern matching.
    
*)