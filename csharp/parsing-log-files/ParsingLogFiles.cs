using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
public class LogParser
{
    string pattern = @"^\[(TRC|DBG|INF|WRN|ERR|FTL)\]+";
    
    
    public bool IsValidLine(string text)
    {
        Regex regex = new Regex(pattern);
        return regex.IsMatch(text);
      
    }
    
    public string[] SplitLogLine(string text)
    {
    
        string pattern = "<[-=*^]+>";
        Regex regex = new Regex(pattern);
        string[] result = Regex.Split(text, pattern);
        return result;
    }
    public int CountQuotedPasswords(string lines)
    {
        string convertLines = new string(lines);
        string pattern = @"""[^""][password][^""]*""";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
      
        int count = 0;
        foreach(Match match in Regex.Matches(lines, pattern))
        {
                count++;
        }
        return count;
    }
    public string RemoveEndOfLineText(string line)
    {
        string pattern = @"end-of-line\d+";
        string removed = Regex.Replace(line, pattern, "");
        return removed;
       
    }
    public string[] ListLinesWithPasswords(string[] lines)
    {
        string pattern = @"(password\w+)";
        var result = new List<string>();
        foreach (string line in lines)
        {
            if (Regex.IsMatch(line, pattern, RegexOptions.IgnoreCase))
            {
                Match match = Regex.Match(line, pattern, RegexOptions.IgnoreCase);
                result.Add($"{match.Value}: {line}");
            }
            else{
                result.Add($"--------: {line}");
            }
            
        }
        return result.ToArray();
  
    }
}
