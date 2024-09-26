module GuessingGame

let reply (guess: int): string = 
    match guess with
    | 41 | 43 -> "So close"
    | _ when guess < 41 -> "Too low"
    | _ when guess > 43 -> "Too high"
    | _ -> "Correct"


(*
    -need to ensure that all possible cases are covered
        
        
        _ -> "Correct"
        this is the default case as the other patterns check to see
        if they match, but if they don't then the answer must be correct
*)









(* Pattern Matching - Guessing Game exercise*)

(*
    -a value can be tested against one or more patterns
    -done through "match" keyword
*)

let describe number = 
    match number with 
    | 0 -> "Zero"
    | 1 -> "One"

(*
    -this looks like switch/case statement



    -varibale pattern --> allows you to capture a value
*)
match number with 
| 0 -> "Zero"
| i -> sprintf "Non zero: %d" i 



(*
    -add additional condition to a pattern --> Guard (clause) using "when" keyword
*)
match number with 
| 0 -> "Zero"
| i when i < 0 -> "Negative number"




(*
    not all possible input will have a matching pattern
        -compiler will detect this and output warning --> Exhaustive Checking
            -to solve, handle all cases
                -Wildcard Pattern --> pattern taht matches on any value
*)
match number with 
| i when i < 0 -> "Negative number"
| _ -> "Positive number"


(*
    pattern matching will test a value against each patter nfrom top to 
    bottom, until it finds a matching pattern and executes the logic
    associated with that pattern
        the order of the patterns matters!
*)