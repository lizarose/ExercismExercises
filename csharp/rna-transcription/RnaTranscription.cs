using System;
using System.Linq;

public static class RnaTranscription
{
     public static string ToRna(string strand) => string.Join("", strand.Select(n => n switch
        { 'A' => 'U', 
          'C' => 'G', 
          'G' => 'C', 
          'T' => 'A' }
    ));
}



// public static class RnaTranscription
// {
//     public static string ToRna(string strand)
//     {
//         string rna = "";

//         foreach(char character in strand)
//         {
//             switch (character)
//             {
//                case 'A':
//                    rna += 'U';
//                    break;
//                case 'C':
//                    rna += 'G';
//                    break;
//                case 'G':
//                    rna += 'C';
//                    break;
//                case 'T':
//                    rna += 'A';
//                    break;
//                default:
//                    throw new ArgumentException("Invalid Sequence");
//             }
//         }
//         return rna;
//     }
// }