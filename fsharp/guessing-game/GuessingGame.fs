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