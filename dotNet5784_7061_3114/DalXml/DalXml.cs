using DalApi;
using System.Diagnostics;
using System.Xml.Linq;
namespace Dal;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }

    public IDependency Dependency => new DependencyImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public IProject Project => new ProjectImplementation();
    
}
