(* Active Patterns - Dynamic Patterns for Matching*)

(*
    -active patterns --> the pattern can be parsed or detech dynamically
*)

//Parse a string into an int or bool:

// create an active pattern
let (|Int|_|) str =
   match System.Int32.TryParse(str:string) with
   | (true,int) -> Some(int)
   | _ -> None

// create an active pattern
let (|Bool|_|) str =
   match System.Boolean.TryParse(str:string) with
   | (true,bool) -> Some(bool)
   | _ -> None


//can be used with match..with expressions
// create a function to call the patterns
let testParse str =
    match str with
    | Int i -> printfn "The value is an int '%i'" i
    | Bool b -> printfn "The value is a bool '%b'" b
    | _ -> printfn "The value '%s' is something else" str

// test
testParse "12"
testParse "true"
testParse "abc"




//active pattern with regular expression:

// create an active pattern
open System.Text.RegularExpressions
let (|FirstRegexGroup|_|) pattern input =
   let m = Regex.Match(input,pattern)
   if (m.Success) then Some m.Groups.[1].Value else None


//can be used with match..with expressions

// create a function to call the pattern
let testRegex str =
    match str with
    | FirstRegexGroup "http://(.*?)/(.*)" host ->
           printfn "The value is a url and the host is %s" host
    | FirstRegexGroup ".*?@(.*)" host ->
           printfn "The value is an email and the host is %s" host
    | _ -> printfn "The value '%s' is something else" str

// test
testRegex "http://google.com/test"
testRegex "alice@hotmail.com"



//FizzBuzz Challenge written using active patterns:

// setup the active patterns
let (|MultOf3|_|) i = if i % 3 = 0 then Some MultOf3 else None
let (|MultOf5|_|) i = if i % 5 = 0 then Some MultOf5 else None

// the main function
let fizzBuzz i =
  match i with
  | MultOf3 & MultOf5 -> printf "FizzBuzz, "
  | MultOf3 -> printf "Fizz, "
  | MultOf5 -> printf "Buzz, "
  | _ -> printf "%i, " i

// test
[1..20] |> List.iter fizzBuzz


(*
    Active Patterns:
        --> provide a way to extend pattern matching by allowing more
            complex, customizable matching logic
        --> decompose data in ways that are not tied to its structure
        --> break down a value into custom forms for matching in pattern
            matching expressions
        --> can be used for complex data types, partial matches, abstract

        Two Main Types:
            --> Single-case active patterns (|Pattern|)
                - matching single case
                - only want to return one possible result or none
                - best for abstract matching
            --> Multi-case active patterns (|Pattern1| Pattern2|...|)
                - matching multiple cases
                - return Some value if they match or none if they don't
                - similar to option types 
                - can be used to handle cases where pattern may fail  
*)
