(*Functions as Interfaces*)

(*
    -all functions are "interfaces" --> many of the roles that interfaces
        play in object-oriented design are implicit in the way that functions work
*)

let addingCalculator input = input + 1

let loggingCalculator innerCalculator input =
   printfn "input is %A" input
   let result = innerCalculator input
   printfn "result is %A" result
   result

(*
    Any function can be transparently swapped for any other function as long as the signatures are the same. In other words, the signature of the function is the interface.
*)





(*
    Generic Wrappers:
    -by default, F#, logging code can be made completely generic so that 
        it will work for any function
*)
let add1 input = input + 1
let times2 input = input * 2

let genericLogger anyFunc input =
   printfn "input is %A" input   //log the input
   let result = anyFunc input    //evaluate the function
   printfn "result is %A" result //log the result
   result                        //return the result

let add1WithLogging = genericLogger add1
let times2WithLogging = genericLogger times2

(*
    The new "wrapped" functions can be used anywhere the original functions could be used
*)
// test
add1WithLogging 3
times2WithLogging 3

[1..5] |> List.map add1WithLogging



//Same generic wrapper can be used for other things too
let genericTimer anyFunc input =
   let stopwatch = System.Diagnostics.Stopwatch()
   stopwatch.Start()
   let result = anyFunc input  //evaluate the function
   printfn "elapsed ms is %A" stopwatch.ElapsedMilliseconds
   result

let add1WithTimer = genericTimer add1WithLogging

// test
add1WithTimer 3

(*
    The ability to do this kind of generic wrapping is one of the great conveniences of the function-oriented approach. You can take any function and create a similar function based on it. As long as the new function has exactly the same inputs and outputs as the original function, the new can be substituted for the original anywhere.
*)






(*
    The Strategy Pattern
    -no subclasses so function passed in with constructor
*)
type Animal(noiseMakingStrategy) =
   member this.MakeNoise =
      noiseMakingStrategy() |> printfn "Making noise %s"

// now create a cat
let meowing() = "Meow"
let cat = Animal(meowing)
cat.MakeNoise

// .. and a dog
let woofOrBark() = if (System.DateTime.Now.Second % 2 = 0)
                   then "Woof" else "Bark"
let dog = Animal(woofOrBark)
dog.MakeNoise
dog.MakeNoise  //try again a second later