using System;
public static class LogAnalysis 
{
    // TODO: define the 'SubstringAfter()' extension method on the `string` type
    public static string SubstringAfter(this string log, string delimiter)
    {
        int index = log.IndexOf(delimiter);
        return log.Substring(index + delimiter.Length);
    }
    // TODO: define the 'SubstringBetween()' extension method on the `string` type
    public static string SubstringBetween(this string log, string delimiter, string delimiter2)
    {
        int index = log.IndexOf(delimiter);
        index += delimiter.Length;
        int endIndex = log.IndexOf(delimiter2, index);
        return log.Substring(index, endIndex - index).Trim();
    }
    // TODO: define the 'Message()' extension method on the `string` type
        public static string Message(this string log)
    {
            var separate = log.Split(": ");
            return separate[1].Trim();
    }
    // TODO: define the 'LogLevel()' extension method on the `string` type
        public static string LogLevel(this string log) => log.SubstringBetween("[","]");
}
