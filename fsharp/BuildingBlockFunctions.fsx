(*Using Functions as Building Blocks to Make Code More Readable*)

(*
    A well-known principle of good design is to create a set of basic operations and then combine these building blocks in various ways
    to build up more complex behaviors. This is done via function
    composition.
*)

// building blocks
let add2 x = x + 2
let mult3 x = x * 3
let square x = x * x

// test
[1..10] |> List.map add2 |> printfn "%A"
[1..10] |> List.map mult3 |> printfn "%A"
[1..10] |> List.map square |> printfn "%A"

// new composed functions
let add2ThenMult3 = add2 >> mult3
let mult3ThenSquare = mult3 >> square

(*
    The ">>" operator is the composition operator. It means: do the first
    function, and then do the second.
*)



(*
    Extending Existing Functions:
*)
/ helper functions;
let logMsg msg x = printf "%s%i" msg x; x     //without linefeed
let logMsgN msg x = printfn "%s%i" msg x; x   //with linefeed

// new composed function with new improved logging!
let mult3ThenSquareLogged =
   logMsg "before="
   >> mult3
   >> logMsg " after mult3="
   >> square
   >> logMsgN " result="

// test
mult3ThenSquareLogged 5
[1..10] |> List.map mult3ThenSquareLogged //apply to a whole list







(* Here is an example of using the composition operator to collapse a list of functions into a single operation.
*)
let listOfFunctions = [
   mult3;
   square;
   add2;
   logMsgN "result=";
   ]

// compose all functions in the list into a single one
let allFunctions = List.reduce (>>) listOfFunctions

//test
allFunctions 5



(*
    Mini Languages:

    Domain-specific languages(DSLs) are well recognized as a technique 
    to create more readable and concise code. Stick with using verbs 
    and nouns to encapsulate the behavior.
*)

// set up the vocabulary
type DateScale = Hour | Hours | Day | Days | Week | Weeks
type DateDirection = Ago | Hence

// define a function that matches on the vocabulary
let getDate interval scale direction =
    let absHours = match scale with
                   | Hour | Hours -> 1 * interval
                   | Day | Days -> 24 * interval
                   | Week | Weeks -> 24 * 7 * interval
    let signedHours = match direction with
                      | Ago -> -1 * absHours
                      | Hence ->  absHours
    System.DateTime.Now.AddHours(float signedHours)

// test some examples
let example1 = getDate 5 Days Ago
let example2 = getDate 1 Hour Hence

// the C# equivalent would probably be more like this:
// getDate().Interval(5).Days().Ago()
// getDate().Interval(1).Hour().Hence()



(*
    For "method chaining" to work, every function should return an object 
    that can be used next in the chain. So you see that the "display"
    function returns the shape, rather than nothing.

    Next we create some helper functions which we expose as the 
    "mini-language", and will be used as building blocks by the users
    of the language.
*)


// create an underlying type
type FluentShape = {
    label : string;
    color : string;
    onClick : FluentShape->FluentShape // a function type
    }
let defaultShape =
    {label=""; color=""; onClick=fun shape->shape}

let click shape =
    shape.onClick shape

let display shape =
    printfn "My label=%s and my color=%s" shape.label shape.color
    shape   //return same shape

(*
    appendClickAction --> takes a function as a parameter and composes
    it with the existing click action
*)

let setLabel label shape =
   {shape with FluentShape.label = label}

let setColor color shape =
   {shape with FluentShape.color = color}

//add a click action to what is already there
let appendClickAction action shape =
   {shape with FluentShape.onClick = shape.onClick >> action}








(*
    2 functions are passed to appendClickAction but then are composed 
    into one
*)



//setup some test values
let redBox = defaultShape |> setRedBox
let blueBox = defaultShape |> setBlueBox

// create a shape that changes color when clicked
redBox
    |> display
    |> changeColorOnClick "green"
    |> click
    |> display  // new version after the click

// create a shape that changes label and color when clicked
blueBox
    |> display
    |> appendClickAction (setLabel "box2" >> setColor "green")
    |> click
    |> display  // new version after the click








(*
    The "showRainbow" function does take a shape as a paraemter, but 
    it is not explicitly shown. This elision of parameters is called
    "point-free" style.
*)

let rainbow =
    ["red";"orange";"yellow";"green";"blue";"indigo";"violet"]

let showRainbow =
    let setColorAndDisplay color = setColor color >> display
    rainbow
    |> List.map setColorAndDisplay
    |> List.reduce (>>)

// test the showRainbow function
defaultShape |> showRainbow