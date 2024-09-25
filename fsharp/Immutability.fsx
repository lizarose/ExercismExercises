(* Immutability - Making Code Predictable*)

(*
    Why is it important?
        -makes the code predictable
            -don't have to worry about what order to call functions in
        -easier to work with
            -easier to write, read, and maintain which means less testing
        -forces you to use a "transformational" approach
            -transform rather than modify original data
*)

// immutable list
let list = [1;2;3;4]

type PersonalName = {FirstName:string; LastName:string}
// immutable person
let john = {FirstName="John"; LastName="Doe"}

(*
    Can't modify data structure, so must copy it when you want to change it
*)

let alice = {john with FirstName="Alice"}



//complex data structures are implemented as linked lists so common parts are shared:

// create an immutable list
let list1 = [1;2;3;4]

// prepend to make a new list
let list2 = 0::list1

// get the last 4 of the second list
let list3 = list2.Tail

// the two lists are the identical object in memory!
System.Object.ReferenceEquals(list1,list3)


(*
    Mutable data:
    -does support mutable data with "mutable" keyword
    -only used for special cases like:
        -optimization
        -caching
        -dealing with .NET libraries
*)