using FluentValidation;

namespace DotNet.TeachersApi.Features;

public record AddTeacherCommand(Guid Id, string Nome);
public class AddTeacherValidator : AbstractValidator<AddTeacherCommand>
{
    public AddTeacherValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
    }
}
public class AddTeacherHandler : IAddTeacherHandler
{
    private readonly IValidator<AddTeacherCommand> _validator;
    private readonly IClientEvent<AddTeacherCreatedEvent> _clientEvent;
    private readonly ITeacherRepository _repository;

    public AddTeacherHandler(IValidator<AddTeacherCommand> validator, ITeacherRepository repository, IClientEvent<AddTeacherCreatedEvent> clientEvent)
    {
        _validator = validator;
        _repository = repository;
        _clientEvent = clientEvent;
    }

    public void Handle(AddTeacherCommand command, CancellationToken cancellationToken)
    {
        var validate = _validator.Validate(command);
        if (!validate.IsValid)
            throw new NotImplementedException();
        var professores = new List<Teacher>() {
            new Teacher(),
            new Teacher()
        };
        _repository.InsertMany(professores);
        _clientEvent.Send(new AddTeacherCreatedEvent());
    }
}
public interface IAddTeacherHandler
{
    void Handle(AddTeacherCommand command, CancellationToken cancellationToken);
}
public interface ITeacherRepository
{
    Teacher? Get(Guid id);
    void InsertMany(IEnumerable<Teacher> orders);
}
public class TeacherRepository : ITeacherRepository
{
    private readonly DatabaseContext context;
    public TeacherRepository(DatabaseContext context)
    {
        this.context = context;
    }
    public Teacher? Get(Guid id)
    {
        return context.Teachers.FirstOrDefault(x => x.Id == id);
    }

    public void InsertMany(IEnumerable<Teacher> orders)
    {
        context.Teachers.AddRange(orders);
    }
}
public class DatabaseContext
{
    public List<Teacher> Teachers { get { return new List<Teacher>() { }; } }
}