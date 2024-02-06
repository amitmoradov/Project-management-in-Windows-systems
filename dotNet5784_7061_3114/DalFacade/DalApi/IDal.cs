
namespace DalApi;

public interface IDal
{
    IDependency Dependency { get; }
    IEngineer Engineer { get; }
    ITask Task { get; }
    public void SaveStartProjectDate(DateTime startProject) { }
}
