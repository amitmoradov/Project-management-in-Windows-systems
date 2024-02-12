
namespace BO;

public class Task
{
   public DateTime? CreatedAtDate {  get; init; } // תאריך יצירת המשימה
   public BO.Status Status {  get; set; } // מצב המשימה 
   public List <BO.TaskInList>? Dependencies {  get; set; }
   public TimeSpan? RequiredEffortTime { get; set; }  //זמן מאמץ נדרש
    public  DO.EngineerExperience? Copmliexity {  get; set; }
    public  DateTime? StartDate { get; init; }  //תאריך תחילת המשימה
    public DateTime? ScheduledDate { get; set; }
    public  DateTime? CompleteDate { get; set; }//תאריך סיום המשימה
    public DateTime? DeadLineDate {  get; set; }
    public DateTime? ForcastDate {  get; set; } // תאריך משוער לסיום = המקסימום מבין תאריך ההתחלה המתוכנן ותאריך ההתחלה בפועל + משך המטלה
    public string? Alias {  get; set; }
    public string? Description {  get; set; }
    public string? Deliverables {  get; set; } // תוצר
    public string? Remarks {  get; set; }
    public int Id {  get; init; }
    public BO.EngineerInTask? Engineer { get; set; } // שם ות.ז מהנדס אחראי על המשימה 
    public BO.MilestoneInTask? Milestone {  get; set; } //  אבן דרך קשורה
    public bool Active { get; set; } 
    public bool CanToRemove {  get; set; }

    public override string ToString() => this.ToStringProperty();

    private string ToStringProperty()
    {
        return $"The task details are:\n" +
        $"Id: {Id}\n" +
        $"Alias: {Alias}\n" +
        $"Description: {Description}\n" +
        $"CreatedAtDate: {CreatedAtDate}\n" +
        $"Status: {Status}\n" +
        $"Complexity: {Copmliexity}\n" +
        $"StartDate: {StartDate}\n" +
        $"ScheduledDate: {ScheduledDate}\n" +
        $"CompleteDate: {CompleteDate}\n" +
        $"DeadLineDate: {DeadLineDate}\n" +
        $"ForecastDate: {ForcastDate}\n" +
        $"RequiredEffortTime: {RequiredEffortTime}\n" +
        $"Deliverables: {Deliverables}\n" +
        $"Remarks: {Remarks}\n" +
        $"Dependencies: {string.Join(", ", Dependencies)}\n" +
        $"Engineer: {Engineer}\n" +
        $"Milestone: {Milestone}\n" +
        $"Active: {Active}\n" +
        $"CanToRemove: {CanToRemove}\n" +
        $"===========================\n"; 

    }
}
