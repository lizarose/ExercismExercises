using System;
public static class PhoneNumber
{
    public static (bool IsNewYork, bool IsFake, string LocalNumber) Analyze(string phoneNumber)
    {
        if(phoneNumber.Length != 12)
       {
           throw new ArgumentException("Invalid Phone Number");
       }
        
        string areaCode = phoneNumber.Substring(0,3);
        string prefix = phoneNumber.Substring(4, 3);
        string lastFour = phoneNumber.Substring(8, 4);
        bool isNewYork = areaCode == "212";
        bool isFake = prefix == "555";
        string localNumber = lastFour;
        
        return (isNewYork, isFake, localNumber);
    }
    public static bool IsFake((bool IsNewYork, bool IsFake, string LocalNumber) phoneNumberInfo) => phoneNumberInfo.IsFake;
      
}
