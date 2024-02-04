
namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
   // public DateTime StartDate { get; init; }
   // public DateTime CompleteDate { get; init; }
    public BO.ProjectScheduled StatusProject { get; }
    public static void Scedule() { }

}
