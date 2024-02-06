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
    
    public void SaveStartProjectDate(DateTime startProject)
    {
        XElement config = XMLTools.LoadListFromXMLElement("data-config");
        XElement? startProjectDate = config.Element("StartProjectDate");
        if (startProjectDate != null)
        {
            startProjectDate.Value = startProject.ToString();
        }
        else
        {
            config.Add(new XElement("StartProjectDate", startProject));
        }
        XMLTools.SaveListToXMLElement(config, "data-config");
    }
}
