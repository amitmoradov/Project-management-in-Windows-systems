
namespace DalApi;

public interface IDal
{
    IDependency Dependency { get; }
    IEngineer Engineer { get; }
    ITask Task { get; }
    void SaveStartProjectDate(DateTime startProject);
    void SaveChangeOfStatus(string status);
    /// <summary>
    /// Return start projectDate .
    /// </summary>
    /// <returns></returns>
    DateTime ReturnStartProjectDate();
}
