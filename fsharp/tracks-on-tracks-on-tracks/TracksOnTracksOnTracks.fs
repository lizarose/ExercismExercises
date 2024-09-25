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