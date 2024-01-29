
namespace BO;
/// <summary>
/// The details of the engineer working on the task
/// </summary>
public class EngineerInTask
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public override string ToString() => this.ToStringProperty();
    private string ToStringProperty()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
    }
}
