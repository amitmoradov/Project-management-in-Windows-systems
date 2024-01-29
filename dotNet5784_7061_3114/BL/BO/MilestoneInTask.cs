
namespace BO;
/// <summary>
/// 
/// </summary>
public class MilestoneInTask
{
    public int Id { get; set; }
    public string? Alias { get; set; }
    public override string ToString() => this.ToStringProperty();
    private string ToStringProperty() => $"Milestone In Task - Id: {Id}, Alias: {Alias}";
}
