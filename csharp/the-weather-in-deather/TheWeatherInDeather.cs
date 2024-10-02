using System;
using System.Collections.Generic;

public class WeatherStation
{
    private Reading reading;
    private List<DateTime> recordDates = new List<DateTime>();
    private List<decimal> temperatures = new List<decimal>();

    public void AcceptReading(Reading reading)
    {
        this.reading = reading;
        recordDates.Add(DateTime.Now);
        temperatures.Add(reading.Temperature);
    }

    public void ClearAll()
    {
        reading = new Reading();
        recordDates.Clear();
        temperatures.Clear();
    }

    public decimal LatestTemperature => reading.Temperature; 
    
    public decimal LatestPressure => reading.Pressure;
    
    public decimal LatestRainfall => reading.Rainfall;
    
    public bool HasHistory  => recordDates.Count > 1 ? true : false;
    
    public Outlook ShortTermOutlook => 
        reading.Equals(new Reading()) ? throw new ArgumentException() : 
        (reading.Pressure, reading.Temperature) switch
        {
            (< 10m, < 30m) => Outlook.Cool,
            (_, > 50) => Outlook.Good,
            _ => Outlook.Warm,
        };

    public Outlook LongTermOutlook =>
        reading.Equals(new Reading()) ? throw new ArgumentException() : 
        (reading.WindDirection, reading.Temperature) switch
        {
            (WindDirection.Southerly, _) => Outlook.Good,
            (WindDirection.Easterly, > 20) => Outlook.Good,
            (WindDirection.Northerly, _) => Outlook.Cool,
            (WindDirection.Easterly, <= 20) => Outlook.Warm,
            (WindDirection.Westerly, _) => Outlook.Rainy,
        };
  
    public State RunSelfTest() => reading.Equals(new Reading()) ? State.Bad : State.Good;
    
}

/*** Please do not modify this struct ***/
public struct Reading
{
    public decimal Temperature { get; }
    public decimal Pressure { get; }
    public decimal Rainfall { get; }
    public WindDirection WindDirection { get; }

    public Reading(decimal temperature, decimal pressure,
        decimal rainfall, WindDirection windDirection)
    {
        Temperature = temperature;
        Pressure = pressure;
        Rainfall = rainfall;
        WindDirection = windDirection;
    }
}

/*** Please do not modify this enum ***/
public enum State
{
    Good,
    Bad
}

/*** Please do not modify this enum ***/
public enum Outlook
{
    Cool,
    Rainy,
    Warm,
    Good
}

/*** Please do not modify this enum ***/
public enum WindDirection
{
    Unknown, // default
    Northerly,
    Easterly,
    Southerly,
    Westerly
}


















/*
    Expression Bodied Members:
        -many types of struct and class members (fields being exception) can use the 
        expression-bodied member syntax
            -this produces more consice and readable code
            -methods and read-only properties

        public int Times3(int input) => input * 3;

        public int Interesting => 1729;



    Switch Expressions:
        -can match a value to one case in a set of patterns and return the associated 
        value or take the associated action
            -the association is denoted by the => symbol
            -each pattern can have an optional case guard introduced with the 'when'
            keyword
            -case guard expression must evaluate to true for that "arm" of the switch
            to be selected
            -the cases(switch arms) are evaluated in text order and teh process is cut
            short and teh associated value is returned as soon as a match is found


        double xx = 42d;

        string interesting = xx switch
        {
            0 => "I suppose zero is interesting",
            3.14 when DateTime.Now.Day == 14 && DateTime.Now.Month == 3 => "Mmmm pie!",
            3.14 =>  "π",
            42 => "a bit of a cliché",
            1729 => "only if you are a pure mathematician",
            _ => "not interesting"
        };

        // --> interesting == "a bit of a cliché"
        




    Conditionals Ternary:
        -ternary operators allow if-conditions to be defined in expressions rather than
        statement blocks
            -makes code more expressive and less error-prone
        
        -they combine 3 expressions:
            1. a condition followed by an expression to be evaluated and returned if 
                the condition is true (the if part, introduced by ? )
            2. an expression to be evaluate and returned if the condition is false
                (the else part, introduced by : )


        int a = 3, b = 4;
        int max = a > b ? a : b;

        // --> 4


    Throw Expressions:
        -throw expressions are an alternative to throw statements and in particular can add to the power of ternary and other compound expressions

    string trimmed = str == null ? throw new ArgumentException(): str.Trim();

*/