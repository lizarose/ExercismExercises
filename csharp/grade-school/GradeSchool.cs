using System;
using System.Collections.Generic;
using System.Linq;

public class GradeSchool
{
    private readonly HashSet<string> students = new();
    private readonly Dictionary<string, int> grades = new();
    
    public bool Add(string student, int grade)
    {
        if(grades.ContainsKey(student))
            return false;
    
        students.Add(student);
        grades[student] = grade;
        return true;
    }

    public IEnumerable<string> Roster() => grades
        .OrderBy(kvp => kvp.Value) //Sort by grade
        .ThenBy(kvp => kvp.Key)    //Sort by student
        .Select(kvp => kvp.Key)    //Select student
        .ToList();
    
    public IEnumerable<string> Grade(int grade) => grades
        .Where(kvp => kvp.Value == grade)   //Filter students by grade
        .Select(kvp => kvp.Key)             //Select student name
        .OrderBy(name => name)              //Sort student names
        .ToList();
    
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