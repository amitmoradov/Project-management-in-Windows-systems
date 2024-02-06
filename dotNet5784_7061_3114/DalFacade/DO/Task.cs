
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
/// <param name="ScheduledDate"> </param>
/// <param name="DeadLineDate"> Date of the finish .</param>
/// <param name="Deliverables">Product .</param>
/// <param name="Remarks"> Remark to the task .</param>
/// <param name="EngineerId">The id of the charge engineer .</param>
public record Task
( 
    DateTime? _createdAtDate,
    TimeSpan? _requiredEffortTime,//זמן מאמץ נדרש
    DO.EngineerExperience? _copmliexity,
    DateTime? _startDate,//תאריך תחילת הפרוייקט
    DateTime? _scheduledDate,
    DateTime? _completeDate,//תאריך סיום הפרוייקט
    DateTime? _deadLineDate,
    string? _alias = null,
    string? _description = null,
    string? _deliverables = null,
    string? _remarks = null,
    int _id = 0,
    int _engineerId = 0,
    bool _active = true,
    bool _isMilestone = false,
    bool _canToRemove = true


)
{
    public Task () : this(null, null, null,null ,null, null, null) { }

    public bool ShouldSerialize_scheduledDate() { return _scheduledDate.HasValue; }
    public bool ShouldSerialize_requiredEffortTime() { return _requiredEffortTime.HasValue; }
    public bool ShouldSerialize_deadLineDate() { return _deadLineDate.HasValue; }
    public bool ShouldSerialize_deliverables() { return !string.IsNullOrEmpty(_deliverables); }
    public bool ShouldSerialize_startDate() { return _startDate.HasValue; }
    public bool ShouldSerialize_completeDate() { return _completeDate.HasValue; }
    //public bool ShouldSerializeEngineerId() { return _engineerId.HasValue; }
}