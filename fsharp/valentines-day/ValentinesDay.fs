module ValentinesDay
// TODO: please define the 'Approval' discriminated union type
type Approval =
    | Yes
    | No 
    | Maybe 
// TODO: please define the 'Cuisine' discriminated union type
type Cuisine = 
    | Korean
    | Turkish 
// TODO: please define the 'Genre' discriminated union type
type Genre = 
    | Crime
    | Horror 
    | Romance 
    | Thriller
// TODO: please define the 'Activity' discriminated union type
type Activity =
    | BoardGame 
    | Chill
    | Movie of Genre
    | Restaurant of Cuisine
    | Walk of int 
let rateActivity (activity: Activity): Approval =  
    match activity with 
    | Movie Romance | Restaurant Korean -> Yes
    | Walk kilometers when kilometers < 3 -> Yes
    | Walk kilometers when kilometers < 5 -> Maybe
    | Restaurant Turkish -> Maybe
    | _ -> No



(*
    -instead of listing out all of the "No" choices, just group them together with the everything else choice
        _ -> No

    -multiple "Yes" options can be grouped together, but the "Walk" option can't due to being an int type vs the others are strings
*)












(* Discriminated Unions - Valentines Day exercise*)

(*
    -discriminated union type represents a fixed number of named cases
    -each value corresponds to exactly one of the named cases
    -defined using teh "type" keyword, with cases separated by pipes |
*)
type Season =
    | Spring
    | Summer
    | Autumn
    | Winter

(*
    -each case can optionally have data associated with it, and different
    cases can have different types of data
    -if none of the cases have data associated with them --> enum
*)
type Number = 
    | Integer of int
    | Float of float 
    | Invalid 

(*
    -creating a value for a specific case can be done by referring its name
        (Success)
    -case names are just constructor functions, associated data can be passed as a regualr function argument
    -if another disciminated union has defined a case with the same name,
    then must use its full name
        (Result.Success)
*)
let byName = Integer 2
let byFullName = Number.Invalid

(*
    -DU has structural equality --> 2 values for the same case w/same data
        are equivalent
    
    -can use if/elif/else expressions to work with DUs but it's best to
        use Match..With
*)