(*Conciseness and Why It Is Important*)

(*
    An important goal for most mainstream programming languages is a good 
    balance of readability and conciseness. Too much conciseness can 
    result in hard-to-understand or obfuscated code, while too much 
    verbosity can easily swamp the underlying meaning. Ideally, we want a 
    high signal-to-noise ratio, where every word and character in the code 
    contributes to the meaning of the code, and there is minimal 
    boilerplate.
*)

(*
    Why is conciseness important? Here are a few reasons:
    - a concise language tends to be more declarative, saying what the 
        code should do rather than how to do it
    - it is easier to reason about correctness if there are fewer lines 
        of code to reason about
    - you can see more code on the screen
*)

(*
    F# is more concise than C# due to this:
        -type inference and low overhead type definitions
            -easy to create new types and no visual clutter with definition
        -using functions to extract boilerplate code
            -DRY 
        -composing complex code from simple functions and creating mini-languages
            -easy to create a set of basic operations and then combine 
            these building blocks to build up more complex behaviors
        -pattern matching
            -can be a glorified switch statement
            -can compare expressions in a number of ways by matching on:
                -values
                -conditions
                -types
            
*)