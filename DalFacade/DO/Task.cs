
namespace DO;

/// <summary>
/// The tesk of engineer .
/// </summary>
/// <param name="Id"> Id of the task .</param>
/// <param name="Alise"> nickname of the task .</param>
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
    int Id,
    int EngineerId,
    DateTime? CreatedAtDate,
    TimeSpan? RequiredEffortTime,
    bool IsMilestone,
    DO.EngineerExperience? Copmliexity,
    DateTime? StartDate,
    DateTime? ScheduledDate,
    DateTime? DeadLineDate,
    string? Alias = null,
    string? Description = null,
    string? Deliverables = null,
    string? Remarks = null,
    bool active = true,
    bool canToRemove = true

)
{
    public Task () : this(0, 0, null, null, false, null, null, null, null) { }
}