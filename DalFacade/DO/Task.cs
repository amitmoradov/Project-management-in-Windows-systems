
namespace DO;


public record Task
(
    int Id,
    string? Alise,
    string? Description,
    DateTime? CreatedAtDate,
    TimeSpan? RequiredEffortTime,
    bool IsMilestone,
    DO.EngineerExperience Copmliexity,
    DateTime? StartDate,
    DateTime? ScheduledDate,
    DateTime? DeadLineDate,
    string? Deliverables,
    string? Remarks,
    int EngineerId
);