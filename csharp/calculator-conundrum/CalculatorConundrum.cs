using System;

public static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string operation)
    {
         try
        {
             if (operation == null)
            {
                 throw new ArgumentNullException("Null Operation");
            }
            else if (operation == "")
            {
                  throw new ArgumentException("No Value Entered for Operation");
            }
            
            switch (operation)
            {
                case "+": 
                    return $"{operand1} + {operand2} = {operand1 + operand2}";
                case "*":
                    return $"{operand1} * {operand2} = {operand1 * operand2}";
                case "/":
                    return operand2 != 0 ? $"{operand1} / {operand2} = {operand1 / operand2}": "Division by zero is not allowed.";
                default:
                    throw new ArgumentOutOfRangeException("Non Valid Operation");
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new ArgumentOutOfRangeException("Non-Valid Operation");
        }
    
    }
}
