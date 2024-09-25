module GuessingGame

let reply (guess: int): string = 
    match guess with
    | 42 -> "Correct"
    | 41 | 43 -> "So close"
    | _ when guess < 41 -> "Too low"
    | _ -> "Too high"


(*
    -need to ensure that all possible cases are covered
        
        
        _ -> "Too high"
        this is like an else statement as it will catch anything that
        doesn't match the other patterns
*)