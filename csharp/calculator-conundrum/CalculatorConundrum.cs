using System;
public static class SimpleCalculator
{
    public static string Calculate(int operand1, int operand2, string operation)
    {
        if (operation == "+")
        {
            return $"{operand1} + {operand2} = {operand1 + operand2}";
        }
        else if (operation == "*")
        {
            return $"{operand1} * {operand2} = {operand1 * operand2}";
        }
        else if (operation == "/")
        {
            if (operand2 != 0)
            {
            return $"{operand1} / {operand2} = {operand1 / operand2}";
            }
            else
            {
                return "Division by zero is not allowed.";
            }
        }
        else if (operation == null)
        {
             throw new ArgumentNullException("Null Operation");
        }
        else if (operation == "")
        {
              throw new ArgumentException("No Value Entered for Operation");
        }
        else
        {
            throw new ArgumentOutOfRangeException("Non Valid Operation");
        }
    }
}