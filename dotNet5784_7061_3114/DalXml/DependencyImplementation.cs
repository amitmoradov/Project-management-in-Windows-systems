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

    public int Create(Dependency item)
    {
        // chack if the item is exist
        Dependency? dependency = Read(item._id);
        if (dependency == null)
        {
            // Get the current run nummber .
            int newId = Config.NextDependencyId;

            // Copy of item and change Id .
            Dependency copyItem = item with { _id = newId };
            
            XElement dependsId = new XElement("dependency id ", dependency?._id);

            // A function that saves the content of the XELEMENT into an xml file
            XMLTools.SaveListToXMLElement(dependsId, e_dependncy_xml);
         
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
        XElement? search;
        search = XMLTools.LoadListFromXMLElement(e_dependncy_xml);
        return from p in search.Elements()
               select new Dependency();           
        //return FirstOrDefault(search => search._id == id);

        //foreach (var item in DataSource.Dependencies)
        //{
        //    if (item._id == id)
        //    {
        //        return item;
        //    }
        //}
        //return null;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        // Returns the first member that meets the condition if there is none, in which case it returns null
        return DataSource.Dependencies.FirstOrDefault(filter);
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;
        //return new List<Dependency>(DataSource.Dependencies);
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
