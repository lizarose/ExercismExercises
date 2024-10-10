module TwoFer

let twoFer (input: string option): string = 
    match input with 
    | Some n -> sprintf "One for %s, one for me." n 
    | None -> "One for you, one for me."