namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        // chack if the item is exist
        Task? task = Read(item.Id);

        if (task == null)
        {
            // Get the current run nummber .
            int newId = DataSource.Config.NextTeskId;
            // Copy of item and change Id .
            Task copyItem = item with { Id = newId };

            DataSource.Tasks.Add(copyItem);
            return newId;
        }

        // If the object is exist
        throw new Exception($"Task with ID={item.Id} is exists");
    }

    public void Delete(int id)
    {
        Task? task = Read(id);
        // The object can to remove
        if (task is not null && task.canToRemove)
        {
            DataSource.Tasks.Remove(task);
            return;
        }
        if (task is not null && !task.canToRemove)
        {
            throw new Exception($"Task with ID={id} cannot be deleted");
        }
        throw new Exception($"Task with ID={id} is Not exists");
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
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    { 
        // chack if the task is exists
        Task? task = Read(item.Id);
      
        if(task is not null)
        {
            Delete(task.Id);
            Create(item);
            return;
        }
        throw new Exception($"Engineer with ID={item.Id} is not exists");
    }
}
