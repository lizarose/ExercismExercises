module GradeSchool

type School = Map<int, string list>

let empty: School = Map.empty
//Initialize empty roster for School

//Check if student is already in any grade --> ignore grade and focus just on name
let studentAlreadyInGrade (student: string) (school: School): bool = 
    school |> Map.exists (fun _ students -> List.contains student students)

//If student is not in a grade --> add to grade
let add (student: string) (grade: int) (school: School): School = 
    if studentAlreadyInGrade student school then
        school
    else 
        match Map.tryFind grade school with 
        | Some studentsName ->  Map.add grade (student :: studentsName) school
        | None -> Map.add grade [student] school

//Roster for all students --> convert to kvp, collect and sort, remove dupes, back to list
let roster (school: School): string list = 
    school
    |> Map.toSeq
    |> Seq.collect (snd >> List.sort)
    |> Seq.distinct
    |> Seq.toList

//Roster for specific grade --> find and sort
let grade (number: int) (school: School): string list = 
    match Map.tryFind number school with
    | Some students -> List.sort students
    | None -> []








(*
    >> --> this takes two functions and combines them together and allows you to pipe essentially the result of the first to the next

    List.sortBy --> allows you to control what you sort your list by
            ex: 
                -Length
                -names
                -grades



    List.collect --> applies function to each element of a list and then concatenates all the results and returns the combined list

example:
[1..4] |> List.collect (fun x -> [1..x])
     //result --> [1; 1; 2; 1; 2; 3; 1; 2; 3; 4] 

        --> [1]
        --> [1; 2]
        --> [1; 2; 3]
        --> [1; 2; 3; 4]




*)