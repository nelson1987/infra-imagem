namespace DotNet.TeachersApi.Features;

public class Teacher
{
    public Guid Id { get; set; }
    public Guid AssetId { get; internal set; }
}