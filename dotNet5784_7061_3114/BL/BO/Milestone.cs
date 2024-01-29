
namespace BO;

public class Milestone
{
    public int Id { get; set; }
    public string? Descrition { get; set; }
    public string? Alias { get; set; }
    public DateTime? CreatedAtDate { get; init; } // תאריך תחילת המשימה
    public BO.Status Status { get; set; }
    public DateTime? ForcastDate { get; set; } // תאריך משוער לסיום = המקסימום מבין תאריך ההתחלה המתוכנן ותאריך ההתחלה בפועל + משך המטלה
    public DateTime? DeadLineDate { get; set; }
    public DateTime? CompleteDate { get; set; }//תאריך סיום הפרוייקט
    public double? CompletionPrecentage { get; set; }//אחוז התקדמות - אחוז המטלות שהסתיימו מתוך המטלות שאבן הדרך תלויה בהן.

    public string? Remarks { get; set; }
    public List<BO.TaskInList>? Dependencies { get; set; }

    public override string ToString() => this.ToStringProperty();
    private string ToStringProperty() => $"{nameof(Id)}: {Id}, {nameof(Descrition)}: " +
        $"{Descrition}, {nameof(Alias)}: {Alias}, {nameof(CreatedAtDate)}: {CreatedAtDate}, {nameof(Status)}: {Status}," +
        $" {nameof(ForcastDate)}: {ForcastDate}, {nameof(DeadLineDate)}: {DeadLineDate}, {nameof(CompleteDate)}: {CompleteDate}," +
        $" {nameof(CompletionPrecentage)}: {CompletionPrecentage}%, {nameof(Remarks)}: {Remarks}, {nameof(Dependencies)}: {Dependencies}";

}
