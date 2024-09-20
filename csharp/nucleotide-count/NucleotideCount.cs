using System;
using System.Collections.Generic;

public static class NucleotideCount
{
    public static IDictionary<char, int> Count(string sequence)
    {
        Dictionary<char, int> charCount =
            new Dictionary<char, int>();

        charCount.Add('A', 0);
        charCount.Add('C', 0);
        charCount.Add('G', 0);
        charCount.Add('T', 0);


        foreach(char character in sequence)
        {
           switch (character)
           {
               case 'A':
                   charCount['A']++;
                   break;
               case 'C':
                   charCount['C']++;
                   break;
               case 'G':
                   charCount['G']++;
                   break;
               case 'T':
                   charCount['T']++;
                   break;
               default:
                   throw new ArgumentException("Invalid Sequence");
           }
        }
        return charCount;
    }
}