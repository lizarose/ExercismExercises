using System;
using System.Collections.Generic;
using System.Linq;


public class GradeSchool
{
    private readonly Dictionary<int, List<string>> grades = new();
    
    //add student to specific grade
    public bool Add(string student, int grade)
    {
        if(grades.Values.Any(g => g.Contains(student)))     //check if in grades
            return false;
        
        if(!grades.ContainsKey(grade))                  //if grade doesn't exist --> create it
            grades[grade] = new List<string>();
        
        grades[grade].Add(student);                 //add student to list for specific grade
        return true;
    }

    //get entire roster
    public IEnumerable<string> Roster() => grades
         .OrderBy(kvp => kvp.Key)                               //sort by grade
         .SelectMany(kvp => kvp.Value.OrderBy(name => name))    //sort by name
         .ToList();
    
    //get specific grade list
    public IEnumerable<string> Grade(int grade) =>
        grades.ContainsKey(grade)                           //check if grade exists
        ? grades[grade].OrderBy(name => name).ToList()      //if yes, return list
        : new List<string>();                               //if no, create list
}

























// public class GradeSchool
// {
//     private readonly HashSet<string> students = new();
//     private readonly Dictionary<string, int> grades = new();
    
//     public bool Add(string student, int grade)
//     {
//         if(grades.ContainsKey(student))
//         {
//             return false;
//         }
//         students.Add(student);
//         grades[student] = grade;
//         return true;
//     }

//     public IEnumerable<string> Roster()
//     {      
//         var gradeList = new Dictionary<int, List<string>>();

//         foreach(var kvp in grades)
//         {
//             int grade = kvp.Value;
//             string student = kvp.Key;

//             if(!gradeList.ContainsKey(grade))
//             {
//                 gradeList[grade] = new List<string>();
//             }
            
//             gradeList[grade].Add(student);
//         }
        
//         var sortedRoster = new List<string>();
//         var sortedGrades = new List<int>(gradeList.Keys);
//         sortedGrades.Sort();

//         foreach(var grade in sortedGrades)
//         {
//             var studentsGrades = gradeList[grade];
//             studentsGrades.Sort();
        
//             foreach(var student in studentsGrades)
//             {
//                 sortedRoster.Add(student);
//             }
//         }
//         return sortedRoster;
//     }

//     public IEnumerable<string> Grade(int grade)
//     {
//         var result = new List<string>();
//         foreach(var kvp in grades)
//         {
//             if(kvp.Value == grade)
//             {
//                 result.Add(kvp.Key);
//             }
//         }
//         result.Sort();
//         return result;
//     }
// }