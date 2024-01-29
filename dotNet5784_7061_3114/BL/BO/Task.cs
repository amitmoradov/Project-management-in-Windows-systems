using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Task
    {
       public DateTime? CreatedAtDate {  get; init; } // תאריך תחילת המשימה
       public BO.Status Status {  get; set; } // מצב המשימה 
       public list <BO.TaskInList> Dependencies {  get; set; }
       public TimeSpan? RequiredEffortTime { get; set; }  //זמן מאמץ נדרש
        public  DO.EngineerExperience? Copmliexity {  get; set; }
        public  DateTime? StartDate { get; init; }  //תאריך תחילת הפרוייקט
        public DateTime? ScheduledDate { get; set; } // משך הזמן הדרוש למשימה 
        public  DateTime? CompleteDate { get; set; }//תאריך סיום הפרוייקט
        public DateTime? DeadLineDate {  get; set; }
        public DateTime? ForcastDate {  get; set; } // תאריך משוער לסיום = המקסימום מבין תאריך ההתחלה המתוכנן ותאריך ההתחלה בפועל + משך המטלה
        public string? Alias {  get; init; }
        public string? Description {  get; init; }
        public string? Deliverables {  get; set; } // תוצר
        public string? Remarks {  get; set; }
        public int Id {  get; init; }
        public BO.EngineerInTask Engineer { get; set; } // שם ות.ז מהנדס אחראי על המשימה 
        public BO.MilestoneInTask Milestone {  get; set; } //  אבן דרך קשורה
        public bool Active { get; set; } // פעיל
        public bool CanToRemove {  get; set; }

    }
}
