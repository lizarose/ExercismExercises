using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max) =>
         multiples
            .Where(multiples => multiples != 0)
            .SelectMany (multiples => GetMultiples(multiples, max))
            .ToHashSet()
            .Sum();

    public static IEnumerable<int> GetMultiples(int multiple, int max)
    {
     for(int current = multiple; current < max; current += multiple)
         yield return current;
    }
}