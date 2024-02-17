
namespace BO;
/// <summary>
///  רשימת התלויות במשימה/אבני דרך
/// </summary>
public class TaskInList
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public BO.Status Status { get; set; }

    public override string ToString() => this.ToStringProperty();
    private string ToStringProperty()
    {
        return $"Task ID: {Id}\n" +
            $" Description: {Description}\n" +
            $" Alias: {Alias}\n" +
            $" Status: {Status}\n" +
            "=======================" +
            "                           ";



    }

}
