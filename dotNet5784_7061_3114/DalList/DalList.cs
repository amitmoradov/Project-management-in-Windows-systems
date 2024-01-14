namespace Dal;
using DalApi;

/// <summary>
/// sealed : The class cannot be inherited
/// </summary>
sealed public class DalList : IDal
{

    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task => new TaskImplementation();
}

