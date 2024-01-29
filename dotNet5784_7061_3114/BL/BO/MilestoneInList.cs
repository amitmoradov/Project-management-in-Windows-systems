
namespace BO;

public class MilestoneInList
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Alias { get; set; }
    public BO.Status? Status { get; set; }
    public double? CompletionPrecentage { get; set; }////אחוז התקדמות - אחוז המטלות שהסתיימו מתוך המטלות שאבן הדרך תלויה בהן.
    public override string ToString() => this.ToStringProperty();
    private string ToStringProperty() => $"{nameof(Id)}: {Id}, {nameof(Description)}: {Description}, {nameof(Alias)}: {Alias}, {nameof(Status)}: {Status}, {nameof(CompletionPrecentage)}: {CompletionPrecentage}%";
}
