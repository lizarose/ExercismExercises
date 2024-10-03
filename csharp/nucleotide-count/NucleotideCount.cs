using System;
using System.Collections.Generic;
using System.Linq;

public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {     
        var validNucleotides = new HashSet<char> {'A', 'C', 'G', 'T'};   
        var nucleotidesCount = validNucleotides.ToDictionary(n => n, n => 0);
        
        if (sequence.Any(n => !validNucleotides.Contains(n))) 
            throw new ArgumentException("Invalid Sequence");
        
        sequence.ToList().ForEach(n => nucleotidesCount[n]++);
        return nucleotidesCount;
    }
}




// public static class NucleotideCount
// {
//     public static IDictionary<char, int> Count(string sequence)
//     {
//         Dictionary<char, int> charCount =
//             new Dictionary<char, int>();

//         charCount.Add('A', 0);
//         charCount.Add('C', 0);
//         charCount.Add('G', 0);
//         charCount.Add('T', 0);


//         foreach(char character in sequence)
//         {
//            if(charCount.ContainsKey(character))
//            {
//                 charCount[character]++;
//            }
//             else
//             {
//                 throw new ArgumentException("Invalid Sequence");
//             }
//         }
//         return charCount;
//     }
// }