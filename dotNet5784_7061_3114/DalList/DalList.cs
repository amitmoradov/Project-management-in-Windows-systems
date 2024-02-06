namespace Dal;
using DalApi;
using System.ComponentModel;

/// <summary>
/// sealed : The class cannot be inherited
/// </summary>
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }

    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer =>  new EngineerImplementation();

    public ITask Task => new TaskImplementation();
    public void SaveStartProjectDate(DateTime startProject) { }

    void IDal.SaveChangeOfStatus(string status)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Return start projectDate .
    /// </summary>
    /// <returns></returns>
    DateTime IDal.ReturnStartProjectDate()
    {
        throw new NotImplementedException();
    }

    public string ReturnStatusProject()
    {
        throw new NotImplementedException();
    }
}

