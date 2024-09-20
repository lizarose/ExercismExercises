using System;
using System.Collections.Generic;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        var multiplesSet = new HashSet<int>();
        
        foreach(var multiple in multiples)
        {
            if(multiple != 0){
                int currentNum = multiple;
                while(currentNum < max)
                {
                    multiplesSet.Add(currentNum);
                    currentNum += multiple;
                }
            }
        }

        int total = 0;
        foreach(var value in multiplesSet)
        {
            total += value;
        }
        return total;        
    }
}