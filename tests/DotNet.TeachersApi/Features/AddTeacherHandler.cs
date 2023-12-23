namespace DotNet.TeachersApi.Features;

public record AddTeacherCommand(Guid Id, string Nome);
public class AddTeacherHandler : IAddTeacherHandler
{
    public void Handle(AddTeacherCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
public interface IAddTeacherHandler
{
    void Handle(AddTeacherCommand command, CancellationToken cancellationToken);
}