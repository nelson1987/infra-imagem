namespace DotNet.TeachersApi.Features;

public class Broker
{
    private static List<AddTeacherCreatedEvent>? myVar;

    public List<AddTeacherCreatedEvent> Events
    {
        get { return myVar; }
        set { myVar = value; }
    }
    public Broker()
    {
        myVar = myVar ?? new List<AddTeacherCreatedEvent>();
    }
    internal void Include(AddTeacherCreatedEvent request)
    {
        myVar.Add(request);
    }
    internal AddTeacherCreatedEvent Expurge()
    {
        var primeiro = myVar.FirstOrDefault();
        myVar.Remove(primeiro);
        return primeiro;
    }
}