namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        // Get the current run nummber .
        int newId = DataSource.Config.NextTeskId;
        // Copy of item and change Id .
        Task copyItem = item with { Id = newId };

        DataSource.Tasks.Add(copyItem);
        //throw new NotImplementedException();
        return newId;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }
}
