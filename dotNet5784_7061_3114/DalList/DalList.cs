namespace Dal;
using DalApi;
using System.ComponentModel;

/// <summary>
/// sealed : The class cannot be inherited
/// </summary>
sealed internal class DalList : IDal
{
    // Singleton instance of the DalList class
    public static IDal Instance { get; } = new DalList();
    
    // Private constructor to prevent external instantiation
    private DalList() { }

    // Implementation of the Dependency property returning a DependencyImplementation instance
    public IDependency Dependency => new DependencyImplementation();

    // Implementation of the Engineer property returning an EngineerImplementation instance
    public IEngineer Engineer => new EngineerImplementation();

    // Implementation of the Task property returning a TaskImplementation instance
    public ITask Task => new TaskImplementation();

    // Implementation of the Project property returning a ProjectImplementation instance
    public IProject Project => new ProjectImplementation();
}

