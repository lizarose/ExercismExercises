module PizzaPricing

// TODO: please define the 'Pizza' discriminated union type

let pizzaPrice (pizza: Pizza): int = failwith "Please implement the 'pizzaPrice' function"

let orderPrice(pizzas: Pizza list): int = failwith "Please implement the 'orderPrice' function"

























(* Recursion - Pizza Pricing exercise*)

(*
    Rescursion --> the ability for something to be defined in terms 
        of itself
        -most commonly found in recursive functions --> functions
            that call themselves
        -recursive functions need to have at least one base case 
            and at least one recursive case
            -base case --> returns a value without calling the 
                fucntion again
            -recursive case --> calls the function again,
                modifying the input so that it will at some point 
                match the base case
        -recursive functions are defined like regular functions, but 
            with the "rec" modifier 
            -without this modifier, the function will not be able to 
                call itself and any attempt to do so will result in 
                compilation error
*)
let rec factorial x =
    //Base case
    if x = 1 then 
        1
    //Recursive case
    else 
        x * factorial (x - 1)


(*
    -also supports recursive types through discriminated unions
        -this has one or more of its cases refer to the DU type 
            itself in their data
        -recursive types must have a base case but don't need "rec"
            keyword
*)
type RussianDoll = 
    | Doll                      //Base case
    | Layer of RussianDoll      //Recursive case