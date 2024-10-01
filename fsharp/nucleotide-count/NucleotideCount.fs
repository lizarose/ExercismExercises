module NucleotideCount

let nucleotideCounts (strand: string): Option<Map<char, int>> =  

    let initialCharCount = Map.ofList [('A', 0); ('C', 0); ('G', 0); ('T', 0)]

    let countNucleotides nucleotideMap char = 
        nucleotideMap |> Option.bind (fun count -> 
        match Map.tryFind char count with 
        | Some currentCount -> Some (Map.add char (currentCount + 1) count)
        | None -> None)

    strand |> Seq.fold countNucleotides (Some initialCharCount)






(*
    List.ofSeq --> takes a sequence and turns to a list
    List.forall --> looks to see if a functions is true for all elements being tested
    Map.ofList --> converts a list to a map

    map --> similar to a dictionary in C#
    map.containsKey --> looks for key

    As the 'yield' keyword pushes a single value into a list, the keyword, 'yield!', pushes a collection of values into the list.

    Option.bind --> chain operations on Option types 
                    -process a value inside an Option only if it contains a valid value(Some), and it skips the operation if the Option is None
                    -useful for performing sequences of operations that might fail, without having to explicitly check for None at each step

                    Input: an Option value and a function
                    Behavior: 
                            -if the Option is Some(x), it applies the function to x --> the function returns another Option
                            -if the Option is None, it skips applying the function and directly returns None

            example:
            let incrementIfEven x = 
                if x % 2 = 0 then 
                    Some (x + 1)
                else 
                    None

            let result = Option.bind incrementIfEven (Some 4)
                //Result --> Some 5                                     4 is even so the function was applied which then returns Some 5 as the result

            let result2 = Option.bind incrementIfEven (Some 3)
                //Result --> None

            let result3 = Option.bind incrementIfEven None
                //Result --> None











            Option.bind -->
                -it may or may not transform the data
                -it may return the input itself
                -it may transform the input and return
                -it may cause trajectory change; Some or None in this case

            

            let result = input |> f1 |> f2 |> f3 |> f4


                -this is a pipeline --> deals with sequential flow, one function call after the other
                                        and the output of one goes into the next as input


            let function num = 
                Some num
                |> Option.bind validation1
                |> Option.map action1
                |> Option.bind validation2
                |> function
                    | Some(x)
                    | None -> 0
















    this is basically tail recursion but problem is it doesn't handle long strands so it won't stackoverflow:
module NucleotideCount

let nucleotideCounts (strand: string): Option<Map<char, int>> =  
    //initial map creation
    let initialCharCount = Map.ofList [('A', 0); ('C', 0); ('G', 0); ('T', 0)]
    //recursive function that takes in a counter and the chars that still need to be iterated through
    let rec countNucleotides nucleotideMap remainingChars = 
        match remainingChars with 
        | [] -> Some nucleotideMap      //base case: returns final count of valid nucleotides
        | char :: remainingChars ->     //pattern matches chars 
            match Map.tryFind char nucleotideMap with 
            | Some currentCount -> countNucleotides (Map.add char (currentCount + 1) nucleotideMap) remainingChars  //this is valid chars so count is updated
            | None -> None              //invalid nucleotide

    strand |> Seq.toList |> countNucleotides initialCharCount       //converts strand to list then calls function with initial map to start recursion
  


*)