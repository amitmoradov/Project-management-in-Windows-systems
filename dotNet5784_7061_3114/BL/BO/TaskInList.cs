
namespace BO;
/// <summary>
///  רשימת התלויות במשימה/אבני דרך
/// </summary>
public class TaskInList
{
    public int Id { get; set; }
    public string? Description { get; init; }
    public string? Alias { get; init; }
    public BO.Status Status { get; set; }

    public override string ToString() => this.ToStringProperty();
    private string ToStringProperty()
    {
        return $"Task ID: {Id}, Description: {Description}, Alias: {Alias}, Status: {Status}";
    }

}
