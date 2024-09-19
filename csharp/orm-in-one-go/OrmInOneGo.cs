using System;
public class Orm
{
    private Database database;
    public Orm(Database database)
    {
        this.database = database;
    }
    public void Write(string data)
    {
        using (var database = new Database())
        try
        {
            database.BeginTransaction();
            database.Write(data);
            database.EndTransaction();
            
        }
        catch
        {
            throw new InvalidOperationException("Invalid Operation");
            
        }
    }
    public bool WriteSafely(string data)
    {
        using (var database = new Database())
        try
        {
            database.BeginTransaction();
            database.Write(data);
            database.EndTransaction();
            return true;
        }
        catch
        {
            return false;
        }
    }
}