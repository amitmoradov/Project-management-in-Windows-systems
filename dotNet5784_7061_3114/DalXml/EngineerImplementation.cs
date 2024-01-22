namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Xml.Linq;


internal class EngineerImplementation : IEngineer
{

    readonly string e_engineer_xml = "engineers";
    public int Create(Engineer item)
    {
        // Load XML file to the list 
        List<Engineer> engineers = new();
        engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(e_engineer_xml);

        //chack if he exist
        Engineer? engineer = Read(item._id);

        if (engineer == null)
        {
            // Add the update to new list
            engineers.Add(item);

            // Update the xml file after change of list engineer.
            XMLTools.SaveListToXMLSerializer<Engineer>(engineers, e_engineer_xml);
            return item._id;
        }
        // if the object is exist
        throw new DalDoesExistException($"Engineer with ID={item._id} is exists");
    }

    public void Delete(int id)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(e_engineer_xml);
        Engineer? engineer = Read(id);

        // The object can to remove
        if (engineer is not null && engineer._canToRemove)
        {
            engineers.Remove(engineer);

            // Update the xml file after change of list engineer.
            XMLTools.SaveListToXMLSerializer<Engineer>(engineers, e_engineer_xml);
            return;
        }
        if (engineer is not null && !engineer._canToRemove)
        {
            throw new DalCannotDeleted($"Engineer with ID={id} cannot be deleted");
        }
        throw new DalDoesNotExistException($"Engineer with ID={id} is Not exists");
    }

    public Engineer? Read(int id)
    {
       List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(e_engineer_xml);

       if (engineers is not null)
       {
            // Pass all DataBase and find the engineer that answer the requirement
            Engineer? requested_engineer = engineers.Find(item => item._id == id);
            return requested_engineer;
       }
       return null;  
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(e_engineer_xml);
        if (engineers is not null)
        {
            // Pass all DataBase and find the engineer that answer the requirement
            return engineers.FirstOrDefault(filter);
        }
        return null;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(e_engineer_xml);

        if (filter != null)
        {
            return from item in engineers
                   where filter(item)
                   select item;
        }

        //Filter == null - return all elements.
        return from item in engineers
               select item;
    }

    public void Update(Engineer item)
    {
        List<Engineer> engineers = XMLTools.LoadListFromXMLSerializer<Engineer>(e_engineer_xml);

        if (item is not null)
        {   
            //Delete the old engineer with same id
            Delete(item._id);
            // Add the update to new list
            engineers.Add(item);

            // Update the xml file after change of list engineer.
            XMLTools.SaveListToXMLSerializer<Engineer>(engineers, e_engineer_xml);
            return;
        }
        throw new DalDoesNotExistException($"Engineer with ID={item!._id} is Not exists");
    }
}
