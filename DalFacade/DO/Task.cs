
using System.Data;

namespace DO;

/// <summary>
/// The tesk of engineer .
/// </summary>
/// <param name="Id"> Id of the task .</param>
/// <param name="Alias"> nickname of the task .</param>
/// <param name="Description"> What is the task now .</param>
/// <param name="CreatedAtDate">CreatedAtDate of the task .</param>
/// <param name="RequiredEffortTime">RequiredEffortTime odf task .</param>
/// <param name="IsMilestone">Milistion in the project .</param>
/// <param name="Copmliexity"> The level of difficulty .</param>
/// <param name="StartDate">The time to start the task .</param>
/// <param name="ScheduledDate"> Date possible to finish</param>
/// <param name="DeadLineDate"> Date of the finish .</param>
/// <param name="Deliverables">Product .</param>
/// <param name="Remarks"> Remark to the task .</param>
/// <param name="EngineerId">The id of the charge engineer .</param>
public record Task
(
    DateTime? CreatedAtDate,
    TimeSpan? RequiredEffortTime,//זמן מאמץ נדרש
    DO.EngineerExperience? Copmliexity,
    DateTime? StartDate,//תאריך תחילת הפרוייקט
    DateTime? ScheduledDate,
    DateTime? CompleteDate,//תאריך סיום הפרוייקט
    DateTime? DeadLineDate,
    string? Alias = null,
    string? Description = null,
    string? Deliverables = null,
    string? Remarks = null,
    int Id = 0,
    int EngineerId = 0,
    bool active = true,
    bool IsMilestone = false,
    bool canToRemove = true

)
{
    public Task () : this(null, null, null,null ,null, null, null) { }
}