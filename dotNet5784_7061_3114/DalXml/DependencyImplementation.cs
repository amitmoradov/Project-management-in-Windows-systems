namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

/// <summary>
/// Implementation of interface method
/// </summary>
internal class DependencyImplementation : IDependency
{
    /// <summary>
    /// Name of DataBase XML
    /// </summary>
    readonly string e_dependncy_xml = "dependencies";
    readonly string  data_config_xml = "data-config";

    public int Create(Dependency item)
    {
        // chack if the item is exist
        Dependency? dependency = Read(item._id);
        if (dependency == null)
        {
            // Get the current run nummber from the data-config.xml 
            int newId = XMLTools.GetAndIncreaseNextId(data_config_xml, e_dependncy_xml);

            // Copy of item and change Id .
            Dependency copyItem = item with { _id = newId };

            // Sets the details of the dependency into the xml file
            XElement dependsId = new XElement("id", copyItem?._id);
            XElement dependOnTask = new XElement("dependOnTask", copyItem?._dependsOnTask);
            XElement dependentTask = new XElement("dependentTask", copyItem?._dependentTask);
            XElement active = new XElement("active", copyItem?._active);
            XElement canToRemove = new XElement("canToRemove", copyItem?._canToRemove);


            //Consolidates all previously configured settings and then saves it to an xml file
            XElement create_dependncy = new XElement("Dependncy", dependsId, dependOnTask, dependentTask,active,canToRemove);
            
            // A function that saves the content of the XELEMENT into an xml file
            XMLTools.SaveListToXMLElement(create_dependncy, e_dependncy_xml);
         
            return newId;
        }
        // if the object is exist
        throw new DalDoesExistException($"Dependency with ID={item._id} is exists");
    }

    public void Delete(int id)
    {
        Dependency? dependency = Read(id);
        // The object can to remove
        if (dependency is not null && dependency._canToRemove)
        {
            Dependencies.Remove(dependency);
            return;
        }

        if (dependency is not null && !dependency._canToRemove)
        {
            throw new DalCannotDeleted($"Dependency with ID={id} cannot be deleted");
        }

        // If the object is not exist
        throw new DalDoesNotExistException($"Dependency with ID={id} is Not exists");
    }

    public Dependency? Read(int id)
    {
        XElement? root = XMLTools.LoadListFromXMLElement(e_dependncy_xml);

        return (from item in root.Elements()
                where (int.Parse(item.Element("id")!.Value) == id)
                select new Dependency()
                {
                    _id = int.Parse(item.Element("id")!.Value),
                    _dependsOnTask = int.Parse(item.Element("dependOnTask")!.Value),
                    _dependentTask = int.Parse(item.Element("dependentTask")!.Value),
                    _active = bool.Parse(item.Element("active")!.Value),
                    _canToRemove = bool.Parse(item.Element("canToRemove")!.Value),

                }).FirstOrDefault();

    }

    public Dependency? Read(Func<Dependency, bool>? filter = null)
    {
        // Checking if we received a condition
        if (filter != null)
        {
            XElement? root = XMLTools.LoadListFromXMLElement(e_dependncy_xml);
            // Pass on all the elements in xml file
            foreach (var item in root.Elements())
            {
                // insert the element to temp variable and chack if filter work.
                Dependency dep = new Dependency(int.Parse(item.Element("id")!.Value),
                    int.Parse(item.Element("dependOnTask")!.Value),
                    int.Parse(item.Element("dependentTask")!.Value),
                    bool.Parse(item.Element("active")!.Value),
                    bool.Parse(item.Element("canToRemove")!.Value));

                if (filter(dep))
                {
                    return dep;
                }
            }
        }
        return null;

    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement? root = XMLTools.LoadListFromXMLElement(e_dependncy_xml);
        // מחזיר העתק של הרשימה העונה על התנאי
        List<DO.Dependency?> dep = new();

        if (filter != null)
        {
            // Add to list all elements that exist the condition.
            foreach(var item in root.Elements())
            {
               dep.Add(Read(filter));
            }
            return dep;
        }

        // if filter == null call to basic function Read with id.
        foreach (var item in root.Elements())
        {
           //
            dep!.Add(Read(int.Parse(item.Element("id")!.Value)));
        }
        return dep!;
    }

    public void Update(Dependency item)
    {
        Dependency? dependency = Read(item._id);
        if (dependency is not null)
        {
            Delete(dependency._id);
            Create(item);
            return;
        }
        throw new DalDoesNotExistException($"Dependency with ID={item._id} is not exists");
    }
}
