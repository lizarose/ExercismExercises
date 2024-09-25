module TisburyTreasureHunt

let getCoordinate (line: string * string): string =
    failwith "Please implement the 'getCoordinate' function"

let convertCoordinate (coordinate: string): int * char = 
    failwith "Please implement the 'convertCoordinate' function"

let compareRecords (azarasData: string * string) (ruisData: string * (int * char) * string) : bool = 
    failwith "Please implement the 'compareRecords' function"

let createRecord (azarasData: string * string) (ruisData: string * (int * char) * string) : (string * string * string * string) =
    failwith "Please implement the 'createRecord' function"






















(* Tuples - Tisbury Treasure Hunt exercise*)

(*
    -Tuple --> an immutable grouping of unnamed but ordered values
        -can hold any(or multiple) data types - including other tuples
        -defined as comma-separated values between (    ) and characters
*)
("one", 2)              //Tuple pair (2 values)
("one", 2, true)       //Tuple triplet (3 values)



(*
    -only tuples with the same length and the same types (in the same order)
    can be compared
    -they hae structural equality --> they are equal only if all their values 
    are equal
*)


(1, 2) = (1, 2)        //same length, same types, same values, same order
    //result --> true

(1, 2) = (2, 1)        //same length, same types, same values, different order
    //result --> false

(1, 2) = (1, "2")       //same length, different types
    //compiler error

(1, 2) = (1, 2, 3)      //different length
    //compiler error



(*
    Ways to extract values from a tuple:
        -the "fst" and "snd" functions
        -tuple deconstruction
        -pattern matching
*)


let person = ("Jordan", 170)

//Option 1: fst/snd
let name1 = fst person
let length2 = snd person

//Option 2: deconstruction
let (name2, length2) = person
    // --> name2 = "Jordan"
    // --> length2 = 170

//Option 3: pattern matching
match person with 
| name3, length3 -> printf "%s: %d" name3 length3