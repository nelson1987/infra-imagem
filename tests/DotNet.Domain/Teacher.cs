namespace DotNet.Domain;

public class Teacher
{
    public Guid Id { get; set; }
    public Guid AssetId { get; internal set; }
}