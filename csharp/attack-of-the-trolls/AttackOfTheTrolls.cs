using System;

// TODO: define the 'AccountType' enum
[Flags]
public enum AccountType
{
    Guest,
    User,
    Moderator
}
// TODO: define the 'Permission' enum
[Flags]
public enum Permission : byte
{
    None = 0b00000000,
    Read = 0b00000001,
    Write = 0b00000010,
    Delete = 0b00000100,
    All = 0b00000111
}
static class Permissions
{
    public static Permission Default(AccountType accountType)
    {    
        switch (accountType)
        {
            case AccountType.Guest:
                return Permission.Read;
                
            case AccountType.User:
                return Permission.Read | Permission.Write;
            case AccountType.Moderator:
                return Permission.All;
            default:
                return Permission.None;
        }
        
        throw new NotImplementedException("Please implement the (static) Permissions.Default() method");
    }
    public static Permission Grant(Permission current, Permission grant)
    {
        return current | grant;
        
        throw new NotImplementedException("Please implement the (static) Permissions.Grant() method");
    }
    public static Permission Revoke(Permission current, Permission revoke)
    {
        return current & ~revoke;
        
        throw new NotImplementedException("Please implement the (static) Permissions.Revoke() method");
    }
    public static bool Check(Permission current, Permission check)
    {
        return (current | check) == current;
        
        throw new NotImplementedException("Please implement the (static) Permissions.Check() method");
    }
}