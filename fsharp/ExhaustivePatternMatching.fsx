(* Exhaustive Pattern Matching - Technique to Ensure Correctness*)

(*
    -nulls cannot exist accidentally
        -a string or object must always be assigned to something at creation, and is immutable therafter
    -In F# there is a similar but more powerful concept to convey the design intent: the generic wrapper type called Option, with two choices: Some or None. The Some choice wraps a valid value, and None represents a missing value.





    Here’s an example where Some is returned if a file exists, but a missing file returns None:
*)
let getFileInfo filePath =
   let fi = new System.IO.FileInfo(filePath)
   if fi.Exists then Some(fi) else None

let goodFileName = "good.txt"
let badFileName = "bad.txt"

let goodFileInfo = getFileInfo goodFileName // Some(fileinfo)
let badFileInfo = getFileInfo badFileName   // None

//If we want to do anything with these values, we must always handle both possible cases.

match goodFileInfo with
  | Some fileInfo ->
      printfn "the file %s exists" fileInfo.FullName
  | None ->
      printfn "the file doesn't exist"

match badFileInfo with
  | Some fileInfo ->
      printfn "the file %s exists" fileInfo.FullName
  | None ->
      printfn "the file doesn't exist"


(*
    We have no choice about this. Not handling a case is a compile-time 
    error, not a run-time error. By avoiding nulls and by using Option 
    types in this way, F# completely eliminates a large class of null 
    reference exceptions.
*)



(*
    Exhaustive Pattern Matching For Edge Cases:
    -As an additional benefit, the F# code is also much more self-documenting. It explicitly describes the consequences of each case. 

*)
let rec movingAverages list =
    match list with
    // if input is empty, return an empty list
    | [] -> []
    // otherwise process pairs of items from the input
    | x::y::rest ->
        let avg = (x+y)/2.0
        //build the result by recursing the rest of the list
        avg :: movingAverages (y::rest)
    // for one item, return an empty list
    | [_] -> []

// test
movingAverages [1.0]
movingAverages [1.0; 2.0]
movingAverages [1.0; 2.0; 3.0]





(*
    Exhaustive Pattern Matching as an Error Handling Technique:
    -In the functional world, a common technique is to create a new 
    structure to hold both the good and bad possibilities, rather than 
    throwing an exception if the file is missing.

*)

(*
    The code demonstrates how performActionOnFile returns a Result object 
    which has two alternatives: Success and Failure. The Failure 
    alternative in turn has two alternatives as well: FileNotFound and 
    UnauthorizedAccess.
*)

// define a "union" of two different alternatives
type Result<'a, 'b> =
    | Success of 'a  // 'a means generic type. The actual type
                     // will be determined when it is used.
    | Failure of 'b  // generic failure type as well

// define all possible errors
type FileErrorReason =
    | FileNotFound of string
    | UnauthorizedAccess of string * System.Exception

// define a low level function in the bottom layer
let performActionOnFile action filePath =
   try
      //open file, do the action and return the result
      use sr = new System.IO.StreamReader(filePath:string)
      let result = action sr  //do the action to the reader
      Success (result)        // return a Success
   with      // catch some exceptions and convert them to errors
      | :? System.IO.FileNotFoundException as ex
          -> Failure (FileNotFound filePath)
      | :? System.Security.SecurityException as ex
          -> Failure (UnauthorizedAccess (filePath,ex))
      // other exceptions are unhandled


//type inference makes it so middle and top layers don't need to specify types returned

// a function in the middle layer
let middleLayerDo action filePath =
    let fileResult = performActionOnFile action filePath
    // do some stuff
    fileResult //return

// a function in the top layer
let topLayerDo action filePath =
    let fileResult = middleLayerDo action filePath
    // do some stuff
    fileResult //return



(*
    Now it is not required that you always handle all possible cases 
    explicitly. In the example below, the function uses the underscore 
    wildcard to treat all the failure reasons as one. This can be 
    considered bad practice if we want to get the benefits of the 
    strictness, but at least it is clearly done.
*)





(*
    Exhaustive Pattern Matching as a Change Management Tool:
    -makes sure code stays correct as requirements change, or during
    refactoring

*)

type Result<'a, 'b> =
    | Success of 'a
    | Failure of 'b
    | Indeterminate

//now requirements change:
type Result<'a> =
    | Success of 'a