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
            //תשמור את המשתנה בתוך הקובץ
            startProjectDate.SetValue(startProject.ToString());
        }
        else
        {
            config.Add(new XElement("StartProjectDate", startProject));
        }
        XMLTools.SaveListToXMLElement(config, "data-config");
    }

    /// <summary>
    /// Save the change of status project . to what I will give him
    /// </summary>
    /// <param name="status"></param>
    public void SaveChangeOfStatus(string status)
    {
        XElement config = XMLTools.LoadListFromXMLElement("data-config");
        XElement? statusOfProject = config.Element("StatusOfProject");
        //I want to replace the value of xelement here with the value I received in the function
        if (statusOfProject != null)
        {
            statusOfProject.SetValue(status);
        }
        XMLTools.SaveListToXMLElement(config, "data-config");
    }
   public DateTime ReturnStartProjectDate()
    {
        XElement config = XMLTools.LoadListFromXMLElement("data-config");
        XElement? startProjectDate = config.Element("StartProjectDate");
        return DateTime.Parse(startProjectDate.Value);
    }


}
