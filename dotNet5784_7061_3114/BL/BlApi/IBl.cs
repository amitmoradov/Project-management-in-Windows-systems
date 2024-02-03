
namespace BlApi;

public interface IBl
{
    public IEngineer Engineer { get; }
    public ITask Task { get; }
    public BO.ProjectScheduled StatusProject {  get; }
    public static void Scedule() { }

}
