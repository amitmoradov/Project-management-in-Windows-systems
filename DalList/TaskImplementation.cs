namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        // chack if the item is exist
        if (Read(item.Id) == null)
        {
            // Get the current run nummber .
            int newId = DataSource.Config.NextTeskId;
            // Copy of item and change Id .
            Task copyItem = item with { Id = newId };

            DataSource.Tasks.Add(copyItem);
            //throw new NotImplementedException();
            return newId;
        }
        // if the object is exist
        throw new Exception($"Task with ID={item.Id} is exists");
    }

    public void Delete(int id)
    {
        Task? task = Read(id);
        // The object can to remove
        if (task is not null && task.canToRemove)
        {
            DataSource.Tasks.Remove(task);
        }
        if (task is not null && !task.canToRemove)
        {
            throw new Exception($"Task with ID={id} cannot be deleted");
        }
        throw new Exception($"Task with ID={id} is Not exists");
        //throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        foreach (var task in DataSource.Tasks)
        {
            if (task.Id == id)
            {
                return task;
            }
        }
        return null;
        //throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
        //throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        if (Read(item.Id) == null) 
        {
        // if the object is not exist
           throw new Exception($"Engineer with ID={item.Id} is not exists");
        }
        foreach(var task in DataSource.Tasks)
        {
            if(task.Id == item.Id)
            {
                Delete(task.Id);
                Create(item);
            }
        }
        //throw new NotImplementedException();
    }
}
