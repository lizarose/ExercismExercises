module GradeSchool

type School = Map<int, string list>

//Initialize empty roster for School
let empty: School = Map.empty


//Roster for specific grade --> find and sort
let grade (number: int) (school: School): string list = 
    school |> Map.tryFind number |> Option.map List.sort |> Option.defaultValue []


//Check if student is already in any grade --> ignore grade and focus just on name
let studentAlreadyInGrade (student: string) (school: School): bool = 
    school |> Map.exists (fun _ students -> List.contains student students)

//If student is not in a grade --> add to grade
let add (student: string) (gradeNum: int) (school: School): School = 
    if studentAlreadyInGrade student school then
        school
    else 
        school |> Map.add gradeNum (student :: (grade gradeNum school))


//Roster for all students --> convert to kvp, collect and sort, remove dupes, back to list
let roster (school: School): string list = 
    school
    |> Map.toSeq
    |> Seq.collect (snd >> List.sort)
    |> Seq.distinct
    |> Seq.toList








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