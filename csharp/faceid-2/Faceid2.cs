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
    HashSet<Identity> adminUser = new HashSet<Identity>
    {
        new Identity("admin@exerc.ism", new FacialFeatures("green", 0.9m))
    };
    
    public static bool AreSameFace(FacialFeatures faceA, FacialFeatures faceB)
    {        
        if (faceA == null || faceB == null)
        {
              return faceA == faceB;
        }
        return faceA.Equals(faceB);
    }
    
    public bool IsAdmin(Identity identity) => adminUser.Contains(identity);
    
    public bool Register(Identity identity) => newUser.Add(identity);
    
    public bool IsRegistered(Identity identity) => newUser.Contains(identity);
    public static bool AreSameObject(Identity identityA, Identity identityB) => Object.ReferenceEquals(identityA, identityB);
    
}