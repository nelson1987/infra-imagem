namespace DotNet.TeachersApi.Features;

public class DatabaseContext
{
    private static List<Teacher>? myVar;

    public List<Teacher> Teachers
    {
        get { return myVar; }
        set { myVar = value; }
    }
    public DatabaseContext()
    {
        myVar = myVar ?? new List<Teacher>();
    }
}
