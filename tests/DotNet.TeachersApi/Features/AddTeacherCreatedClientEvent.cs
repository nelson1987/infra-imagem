namespace DotNet.TeachersApi.Features;

public interface IClientEvent<TRequest>
{
    void Send(TRequest request);
    TRequest Consume();
}
public record AddTeacherCreatedEvent();
public class AddTeacherCreatedClientEvent : IClientEvent<AddTeacherCreatedEvent>
{
    private readonly Broker _broker;
    public AddTeacherCreatedClientEvent(Broker context)
    {
        this._broker = context;
    }
    public AddTeacherCreatedEvent Consume()
    {
        return _broker.Expurge();
    }

    public void Send(AddTeacherCreatedEvent request)
    {
        _broker.Include(request);
    }
}
public class Broker
{
    private List<AddTeacherCreatedEvent>? myVar;

    public List<AddTeacherCreatedEvent> Events
    {
        get { return myVar; }
        set { myVar = value; }
    }
    //public Broker()
    //{
    //    if (!myVar.Any()) myVar = new List<AddTeacherCreatedEvent>() { };
    //}
    internal void Include(AddTeacherCreatedEvent request)
    {
        //if (!myVar.Any()) myVar = new List<AddTeacherCreatedEvent>() { };
        if (myVar == null) myVar = new List<AddTeacherCreatedEvent>() { };
        myVar.Add(request);
    }
    internal AddTeacherCreatedEvent Expurge()
    {
        var primeiro = myVar.FirstOrDefault();
        myVar.Remove(primeiro);
        return primeiro;
    }
}