module SumOfMultiples

let sum (numbers: int list) (upperBound: int): int = 
    let allNums = [1..upperBound - 1]

    let multiples =
        allNums
        |> List.filter (fun number -> 
            List.exists (fun x -> x > 0 && number % x = 0) numbers)

    multiples
    |> List.distinct
    |> List.sum
    







(*
    List.distinct --> returns a list with no duplicates 
    List.filter --> filters out list 
    List.exists --> like List.contains but needs function




To Do:

let numbers = [3;5]
3 --> [3;6;9;12;15;18]
5 --> [5;10;15]
combine --> [3;5;6;9;10;12;15;18] --> sum --> 78

let upperBound = 20


-set list 
-break down list so each value gets filtered with their own list
-combine the lists, removing duplicates
-add all of the numbers in the list to get a total sum of the list

*)

