namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        // chack if the item is exist
        Task? task = Read(item._id);

        if (task == null)
        {
            // Get the current run nummber .
            int newId = DataSource.Config.NextTeskId;
            // Copy of item and change Id .
            Task copyItem = item with { _id = newId };

            DataSource.Tasks.Add(copyItem);
            return newId;
        }

        // If the object is exist
        throw new Exception($"Task with ID={item._id} is exists");
    }

    public void Delete(int id)
    {
        Task? task = Read(id);
        // The object can to remove
        if (task is not null && task._canToRemove)
        {
            DataSource.Tasks.Remove(task);
            return;
        }
        if (task is not null && !task._canToRemove)
        {
            throw new Exception($"Task with ID={id} cannot be deleted");
        }
        throw new Exception($"Task with ID={id} is Not exists");
    }

    public Task? Read(int id)
    {
        return DataSource.Tasks.FirstOrDefault(task => task._id == id);

        //foreach (var task in DataSource.Tasks)
        //{
        //    if (task._id == id)
        //    {
        //        return task;
        //    }
        //}
        //return null;
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }

    public void Update(Task item)
    { 
        // chack if the task is exists
        Task? task = Read(item._id);
      
        if(task is not null)
        {
            Delete(task._id);
            Create(item);
            return;
        }
        throw new Exception($"Engineer with ID={item._id} is not exists");
    }
}
