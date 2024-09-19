using System;
public static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string operation)
    {
         try
        {            
            switch (operation)
            {
                case null:
                    throw new ArgumentNullException("Null Operation");
                case "":
                    throw new ArgumentException("No Value Entered for Operation");
                case "+": 
                    return $"{operand1} + {operand2} = {operand1 + operand2}";
                case "*":
                    return $"{operand1} * {operand2} = {operand1 * operand2}";
                case "/":
                    return $"{operand1} / {operand2} = {operand1 / operand2}";
                default:
                    throw new ArgumentOutOfRangeException("Non Valid Operation");
            }
        }
        catch (DivideByZeroException)
        {
            return "Division by zero is not allowed.";
        }
    
    }
}