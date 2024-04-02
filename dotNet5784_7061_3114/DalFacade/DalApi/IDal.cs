namespace DalApi;

/// <summary>
/// Represents an interface for Data Access Layer (DAL).
/// </summary>
public interface IDal
{
    // Represents an interface for managing dependencies
    IDependency Dependency { get; }

    // Represents an interface for managing engineers
    IEngineer Engineer { get; }

    // Represents an interface for managing tasks
    ITask Task { get; }

    // Represents an interface for managing projects
    IProject Project { get; }

    //void SaveStartProjectDate(DateTime startProject);
    //void SaveChangeOfStatus(string status);
    ///// <summary>
    ///// Return start projectDate .
    ///// </summary>
    ///// <returns></returns>
    //DateTime ReturnStartProjectDate();

    //string ReturnStatusProject();
}