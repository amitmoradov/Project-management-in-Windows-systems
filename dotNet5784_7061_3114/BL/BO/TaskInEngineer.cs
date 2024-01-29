
namespace BO;
/// <summary>
/// Details of the task the engineer is working on
/// </summary>
public class TaskInEngineer
{
    public int Id { get; set; }
    public string? Alias { get; set; }
    public override string ToString() => this.ToStringProperty();
    private  string ToStringProperty() => $"Task In Engineer - Id: {Id}, Alias: {Alias}";


}
