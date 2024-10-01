module NucleotideCount

let nucleotideCounts (strand: string): Option<Map<char, int>> =  
    //initial map creation
    let initialCharCount = Map.ofList [('A', 0); ('C', 0); ('G', 0); ('T', 0)]
    //count nucleotides and check if valid nucleotides
    let countNucleotides nucleotideMap char = 
        match Map.tryFind char nucleotideMap with 
        | Some currentCount -> Map.add char (currentCount + 1) nucleotideMap
        | None -> Map.empty     //if not valid nucleotides, then return empty map

    //use Seq.fold to calculate the nucleotide count
    let result = strand |> Seq.fold countNucleotides initialCharCount

    //check to see if empty to know if valid nucleotides or not
    if Map.isEmpty result then    
        None
    else 
        Some result







(*
    List.ofSeq --> takes a sequence and turns to a list
    List.forall --> looks to see if a functions is true for all elements being tested
    Map.ofList --> converts a list to a map

    map --> similar to a dictionary in C#
    map.containsKey --> looks for key

    As the 'yield' keyword pushes a single value into a list, the keyword, 'yield!', pushes a collection of values into the list.











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