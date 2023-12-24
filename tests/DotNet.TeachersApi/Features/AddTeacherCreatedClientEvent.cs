namespace DotNet.TeachersApi.Features;
public class AddTeacherCreatedClientEvent : IMessageBroker
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
