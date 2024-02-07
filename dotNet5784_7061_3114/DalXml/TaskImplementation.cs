namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

internal class TaskImplementation : ITask
{
    readonly string e_task_xml = "tasks";
    readonly string NextTaskId = "NextTaskId";
    readonly string data_config_xml = "data-config";

    public int Create(Task item)
    {
        // Load XML file to the list 
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(e_task_xml);

        //Chack if he exist
        Task? task = Read(item._id);

        if (task == null)
        {
            // Get the run number id .
            int new_id = XMLTools.GetAndIncreaseNextId(data_config_xml, NextTaskId);

            // Create new task with the new id .
            Task copy_item = item with { _id = new_id };
            // Add the update to new list
            tasks.Add(copy_item);

            // Update the xml file after change of list engineer.
            XMLTools.SaveListToXMLSerializer<Task>(tasks, e_task_xml);
            return new_id;
        }
        // If the object is exist
        throw new DalDoesExistException($"Task with ID = {item._id} is exists");
    }

    public void Delete(int id)
    {
        // Get the file content .
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(e_task_xml);
        Task? task_to_delete = Read(id);

        // The object can to remove
        if (task_to_delete is not null && task_to_delete._canToRemove)
        {
            tasks.Remove(task_to_delete);

            // Update the xml file after change of list engineer.
            XMLTools.SaveListToXMLSerializer<Task>(tasks, e_task_xml);
            return;
        }
        if (task_to_delete is not null && !task_to_delete._canToRemove)
        {
            throw new DalCannotDeleted($"Task with ID = {id} cannot be deleted");
        }
        throw new DalDoesNotExistException($"Task with ID = {id} is Not exists");
    }

    public Task? Read(int id)
    {
        // Get the file content .
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(e_task_xml);

        if (tasks is not null)
        {
            // Pass all DataBase and find the task that answer the requirement
            Task? requested_Task = tasks.Find(item => item._id == id);
            return requested_Task;
        }
        return null;
    }

    public Task? Read(Func<Task, bool> filter)
    {
        // Get the file content .
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(e_task_xml);

        if (tasks is not null)
        {
            // Pass all DataBase and find the engineer that answer the requirement
            return tasks.FirstOrDefault(filter);
        }
        return null;
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        // Get the file content .
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(e_task_xml);

        if (filter != null)
        {
            return from item in tasks
                   where filter(item)
                   select item;
        }

        //Filter == null - return all elements.
        return from item in tasks
               select item;
    }

    public void Update(Task item)
    {
        List<Task> tasks = XMLTools.LoadListFromXMLSerializer<Task>(e_task_xml);

        if (item is not null)
        {
            //Delete the old engineer with same id
            Delete(item._id);
            tasks = XMLTools.LoadListFromXMLSerializer<Task>(e_task_xml);
            // Add the update to new list
            tasks.Add(item);

            // Update the xml file after change of list engineer.
            XMLTools.SaveListToXMLSerializer<Task>(tasks, e_task_xml);
            return;
        }
        throw new DalDoesNotExistException($"Task with ID = {item!._id} is Not exists");
    }

    public void reset()
    {
        XElement root = XMLTools.LoadListFromXMLElement(e_task_xml);
        root.RemoveAll();
        XMLTools.SaveListToXMLElement(root, e_task_xml);

        //Insert to data config file
        XElement data_config = XMLTools.LoadListFromXMLElement(data_config_xml);

        //Reset value
        XElement? reset_next_id = data_config.Element("NextTaskId");
        // Change the value to 1 .
        reset_next_id?.SetValue(1);
        // Save the change .
        XMLTools.SaveListToXMLElement(data_config, data_config_xml);

    }
}


