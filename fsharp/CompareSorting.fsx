(*Comparing F# with C#: Sorting*)

//Implement a quicksort-like algorithm for sorting lists
(*Here is the logic:
    If the list is empty, there is nothing to do.
    Otherwise:
        1. Take the first element of the list
        2. Find all elements in the rest of the list that 
            are less than the first element, and sort them.
        3. Find all elements inthe rest of the list that are 
            >= than the first element, and sort them.
        4. Combine the three parts together to get the final result:
            (sorted smaller elements + firstElement + sorted larger elements)
*)
let rec quicksort list =            //rec --> makes it recursive
    match list with                 //match with --> like switch/case
    | [] ->                 //If the list is empty, 
        []                  //return an empty list.
    | firstElem :: otherElements ->              //If the list is not empty,
        
        let smallerElements =                    //extract the smaller ones.
            otherElements
            |> List.filter (fun e -> e < firstElem)
            |> quicksort                        //then sort them
        
        let largerElements =                    //extract large ones
            otherElements
            |> List.filter (fun e -> e >= firstElem)
            |> quicksort                        //sort them
        //Now combine the 3 parts into a new list and return it
        List.concat [smallerElements; [firstElem]; largerElements]

printfn "%A" (quicksort [1;5;23;18;9;1;3])      //test it
    (*result --> [1; 1; 3; 5; 9; 18; 23]
        val quicksort: list: 'a list -> 'a list when 'a: comparison
        val it: unit = ()
    *)
(*This is a recursive function quicksort: 
    -it takes a list as input and returns a sorted version of that list
Pattern matching on the list:
    - [] -> []     --> this says that if the list is empty, return empty list
    - firstElem :: otherElements    --> this says that if the list is not 
            empty, pattern match to extract the first element(firstElem)
            and the rest of the elements(otherElements)
Splitting the list:
    - smallerElements   --> this extracts elemetns from otherElements that
            are smaller than firstElem
                -uses List.filter to filter out elements smaller than
                firstElem, then applies quicksort recursively to sort this sublist.
    - largerElements    --> extracts elements from otherElements that are 
            greater than or equal to firstElem and applies quicksort
            recursively to sort this sublist
Combine the results:
    -after sorting the smallerElements and largerElements, the function 
    combines the three parts: 
        smallerElements, [firstElem] (the pivot), and largerElements
        -it odes this using List.concat, which concatenates the lists
        in order.
Printing the result:
    -the function is test with the list [1; 5; 23; 18; 9; 1; 3]
    -after applying quicksort, it prints the sorted result*)


(*Match..With:
    -this is similar to switch/case*)
match x with
| caseA -> something
| caseB -> somethingElse
(*
    -"match" with [] matches an empty list, and returns an empty list
    -"match" with firstElem :: otherElemtns does two things:
        1. matches only a non-empty list
        2. creates two new values automatically
            - one for the first element(firstElem)
            - one for the rest of the list(otherElements)
*)

(*
    - The -> is sort of like a lambda (=>) in C#. The equivalent C# lambda 
    would look something like (firstElem, otherElements) => do something.

    - The “smallerElements” section takes the rest of the list, filters it
     against the first element using an inline lambda expression with the 
     “<” operator and then pipes the result into the quicksort function 
     recursively.

    - The “largerElements” line does the same thing, except using the “>=” 
        operator.

    - Finally the resulting list is constructed using the list 
    concatenation function “List.concat”. For this to work, the first 
    element needs to be put into a list, which is what the square brackets 
    are for.

    - Again note there is no “return” keyword; the last value will be 
    returned. In the “[]” branch the return value is the empty list, and in 
    the main branch, it is the newly constructed list.
*)

(*More Concise Version: *)
let rec quicksort2 = function
   | [] -> []
   | first::rest ->
        let smaller,larger = List.partition ((>=) first) rest
        List.concat [quicksort2 smaller; [first]; quicksort2 larger]

// test code
printfn "%A" (quicksort2 [1;5;23;18;9;1;3])