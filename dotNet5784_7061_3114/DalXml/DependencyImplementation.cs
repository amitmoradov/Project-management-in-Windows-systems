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
        XElement? root = XMLTools.LoadListFromXMLElement(e_dependncy_xml);

        if (dependency == null)
        {
            // Get the current run nummber from the data-config.xml 
            int newId = XMLTools.GetAndIncreaseNextId(data_config_xml, e_dependncy_xml);

            // Copy of item and change Id .
            Dependency copyItem = item with { _id = newId };

            //Call to help fenction that convert to Xelemwent
            XElement create_dependncy = ConvertToXElement(item);

            // Add the new dependency to the root.
            root.Add(create_dependncy);
            // A function that saves the content of the XElemenr into an xml file
            XMLTools.SaveListToXMLElement(root, e_dependncy_xml);
         
            return newId;
        }
        // if the object is exist
        throw new DalDoesExistException($"Dependency with ID={item._id} is exists");
    }

    public void Delete(int id)
    {
        XElement? root = XMLTools.LoadListFromXMLElement(e_dependncy_xml);
        XElement? dependency_to_delete = SearchElementInXML(root,id);

        // The object can to remove
        if (dependency_to_delete is not null && bool.Parse(dependency_to_delete.Element("canToRemove")!.Value) == true)
        {
            dependency_to_delete.Remove();
            XMLTools.SaveListToXMLElement(root, e_dependncy_xml);
        }

        if (dependency_to_delete is not null && bool.Parse(dependency_to_delete.Element("canToRemove")!.Value) == false)
        {
            throw new DalCannotDeleted($"Dependency with ID={id} cannot be deleted");
        }

        // If the object is not exist
        throw new DalDoesNotExistException($"Dependency with ID={id} is Not exists");
    }

    public Dependency? Read(int id)
    {
        XElement? root = XMLTools.LoadListFromXMLElement(e_dependncy_xml);
        //Find the element with id that we want
        XElement item = SearchElementInXML(root, id);
        
        //conver him to dependency type
        Dependency? dependency = ConvertToDependency(item);
        return dependency;

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
                Dependency dep = ConvertToDependency(item);

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

        // The list of elements after the filter.
        List<DO.Dependency?> list_dependency = new();

        if (filter != null)
        {
           
            // Add to list all elements that exist the condition.
            foreach (var item in root.Elements())
            {
                //
                Dependency chack_depedency = ConvertToDependency(item);

                if (filter(chack_depedency))
                {
                    list_dependency.Add(chack_depedency);
                }
            }
            return list_dependency;
        }

        // if filter == null call to basic function Read with id.
        foreach (var item in root.Elements())
        {
           //Add new elemnet to the list 
            list_dependency!.Add(Read(int.Parse(item.Element("id")!.Value)));
        }
        return list_dependency!;
    }

    public void Update(Dependency item)
    {
        XElement? root = XMLTools.LoadListFromXMLElement(e_dependncy_xml);

        if (item is not null)
        {
            //Delete the old Dependency with same id
            Delete(item._id);
            XElement dependency_update = ConvertToXElement(item);

            //Add the update item to the file.
            root.Add(dependency_update);

            // Save the file after update.
            XMLTools.SaveListToXMLElement(root, e_dependncy_xml);
            return;
        }
        throw new DalDoesNotExistException($"Dependency with ID={item?._id} is not exists");
    }



    //Help function - to find elemnt and return him (retur Xelelment)
    private XElement SearchElementInXML(XElement root, int id)
    {
        if (root != null)
        {
            XElement? element = root.Elements().FirstOrDefault(item => int.Parse(item.Element("id")!.Value) == id);
            return element;
        }
        throw new XmlRootException("The root of file is not exist");
    }

    //Help function - Convert type XElement to Dependency type
    private Dependency ConvertToDependency(XElement item)
    {

        Dependency? dependency = new(

            int.Parse(item.Element("dependentTask")!.Value),
            int.Parse(item.Element("dependOnTask")!.Value),
            int.Parse(item.Element("id")!.Value),
            bool.Parse(item.Element("active")!.Value),
            bool.Parse(item.Element("canToRemove")!.Value));
        return dependency;
    }

    //Help function - Convert type Dependency  to XElement type
    private XElement ConvertToXElement(Dependency item)
    {

        // Sets the details of the dependency into the xml file
        XElement dependsId = new XElement("id", item?._id);
        XElement dependOnTask = new XElement("dependOnTask", item?._dependsOnTask);
        XElement dependentTask = new XElement("dependentTask", item?._dependentTask);
        XElement active = new XElement("active", item?._active);
        XElement canToRemove = new XElement("canToRemove", item?._canToRemove);


        //Consolidates all previously configured settings and then saves it to an xml file
        XElement convert_dependncy = new XElement("Dependncy", dependsId, dependOnTask, dependentTask, active, canToRemove);

        return convert_dependncy;
    }
}
