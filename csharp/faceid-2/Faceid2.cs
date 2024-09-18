using System;
using System.Collections.Generic;
public class FacialFeatures
{
    public string EyeColor { get; }
    public decimal PhiltrumWidth { get; }
    public FacialFeatures(string eyeColor, decimal philtrumWidth)
    {
        EyeColor = eyeColor;
        PhiltrumWidth = philtrumWidth;
    }
    // TODO: implement equality and GetHashCode() methods
    public override bool Equals(object obj) => obj is FacialFeatures compared && EyeColor.Equals(compared.EyeColor) && PhiltrumWidth.Equals(compared.PhiltrumWidth);
   
    public override int GetHashCode() => (EyeColor, PhiltrumWidth).GetHashCode();
}
public class Identity
{
    public string Email { get; }
    public FacialFeatures FacialFeatures { get; }
    public Identity(string email, FacialFeatures facialFeatures)
    {
        Email = email;
        FacialFeatures = facialFeatures;
    }
    // TODO: implement equality and GetHashCode() methods
    public override bool Equals(object obj) => obj is Identity compared && Email.Equals(compared.Email) && FacialFeatures.Equals(compared.FacialFeatures);
    
    public override int GetHashCode() => (Email, FacialFeatures).GetHashCode();
}
public class Authenticator
{
    public HashSet<Identity> newUser = new HashSet<Identity>();
    
    public static bool AreSameFace(FacialFeatures faceA, FacialFeatures faceB)
    {        
        if (faceA == null || faceB == null)
        {
              return faceA == faceB;
        }
        return faceA.Equals(faceB);
    }
    public bool IsAdmin(Identity identity)
    {
        if (identity.Email == "admin@exerc.ism" && identity.FacialFeatures.EyeColor == "green" && identity.FacialFeatures.PhiltrumWidth == 0.9m) 
        {
            return true;
        }
        return false;
    }
    public bool Register(Identity identity)
    {
        if (newUser.Contains(identity))
        {
            return false;
        }
        else
        {
            newUser.Add(identity);
            return true;
        }
    }
    public bool IsRegistered(Identity identity)
    {
       if (Register(identity) == newUser.Contains(identity))
       {
           return false;
       }
        else
        {
            return true;
        }
    }
    public static bool AreSameObject(Identity identityA, Identity identityB)
    {
        if (identityA == identityB)
        {
            identityA.Equals(identityB);
            return true;
        }
        else
        {
            return false;
        }
    }
}
