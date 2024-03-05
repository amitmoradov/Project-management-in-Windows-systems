
using DalApi;
using System;
using System.Net.NetworkInformation;
using System.Xml.Linq;

namespace Dal;

internal class ProjectImplementation : IProject
{
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
    public string ReturnStatusProject()
    {
        XElement config = XMLTools.LoadListFromXMLElement("data-config");
        XElement? statusProject = config.Element("StatusOfProject");
        return statusProject.Value;
    }

    public void SaveVirtualTimeInDal(DateTime time)
    {
        XElement config = XMLTools.LoadListFromXMLElement("data-config");
        XElement? virualTime = config.Element("VirtualTimeInDal");
        //I want to replace the value of xelement here with the value I received in the function
        virualTime?.SetValue(time);
        
        XMLTools.SaveListToXMLElement(config, "data-config");
    }

    public DateTime ReturnVirtualTimeInDal()
    {
        XElement config = XMLTools.LoadListFromXMLElement("data-config");
        XElement? virtualTimeInDal = config.Element("VirtualTimeInDal");
        return DateTime.Parse(virtualTimeInDal.Value);
    }
}
