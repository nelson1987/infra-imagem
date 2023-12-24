namespace DotNet.TeachersApi.Features;

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