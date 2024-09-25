module ValentinesDay

// TODO: please define the 'Approval' discriminated union type

// TODO: please define the 'Cuisine' discriminated union type

// TODO: please define the 'Genre' discriminated union type

// TODO: please define the 'Activity' discriminated union type

let rateActivity (activity: Activity): Approval = failwith "Please implement the 'rateActivity' function"


















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