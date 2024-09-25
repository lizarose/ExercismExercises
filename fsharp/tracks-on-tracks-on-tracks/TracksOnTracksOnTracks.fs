module TracksOnTracksOnTracks

let newList: string list = []

let existingList: string list = ["F#"; "Clojure"; "Haskell"]

let addLanguage (language: string) (languages: string list): string list =
    language :: languages

let countLanguages (languages: string list): int = 
    languages |> List.length

let reverseList(languages: string list): string list = 
    languages |> List.rev

let excitingList (languages: string list): bool = 
    match languages with
    | "F#" :: _ -> true
    | _ :: "F#" :: [] -> true
    | _ :: "F#" :: [_] -> true
    | _ -> false




(*
    :: --> this is used to add to a list

    languages |> List.length --> this takes the languages string list and counts how many are in the list

    languages |> List.rev --> this takes the languages string list and reverses it

    excitingList is looking for F# to be the first in the list, or the second in the list of 2-3
        :: --> can be used for adding to list, but in this case, it is deconstructing bc it's being used with Match..With
            -splitting the list into head(1st element) and tail(everything else)
            -if checking to see if "F#" is second element in a list and don't care about the length
                | _ :: "F#" :: tail -> true
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