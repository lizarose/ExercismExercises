module RnaTranscription

let toRna (dna: string): string = 

    let convertChar char = 
        match char with 
        | 'A' -> 'U'
        | 'C' -> 'G'
        | 'G' -> 'C'
        | 'T' -> 'A'
        | _ -> 'X'
    
    let mutable rna = ""

    for char in dna do 
        rna <- rna + (convertChar char |> string) 
    
    rna