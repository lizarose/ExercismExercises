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
           if(charCount.ContainsKey(character))
           {
                charCount[character]++;
           }
            else
            {
                throw new ArgumentException("Invalid Sequence");
            }
        }
        return charCount;
    }
}