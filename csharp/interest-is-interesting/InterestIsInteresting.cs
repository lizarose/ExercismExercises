using System;

static class SavingsAccount
{
    public static float InterestRate(decimal balance)
    { 
        if (balance < 0) 
        {
            return 3.213f;
        }
        else if (balance < 1000)
        {
            return 0.5f;
        }
        else if (balance >= 1000 && balance < 5000)
        {
            return 1.621f;
        }
        else if (balance >= 5000)
        {
            return 2.475f;
        }
        else
        {
            return 0.00f;
        }
    }

    public static decimal Interest(decimal balance)
    {
        var rate = InterestRate(balance);
        var decRate = (decimal)rate;
        
            return balance * (decRate / 100);
    }

    public static decimal AnnualBalanceUpdate(decimal balance)
    {
        var interest = Interest(balance);

            return balance + interest;
    }

    public static int YearsBeforeDesiredBalance(decimal balance, decimal targetBalance)
    {
        var years = 0;

        while (balance < targetBalance)
        {
            balance = AnnualBalanceUpdate(balance);
            years++;
        }
        return years;
    }
}