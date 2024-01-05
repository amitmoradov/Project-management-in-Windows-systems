namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
/// <summary>
/// Implementation of interface method .
/// </summary>
public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        // Get the current run nummber .
        int newId = DataSource.Config.NextDependencyId;
        // Copy of item and change Id .
        Dependency copyItem = item with {Id = newId };

        DataSource.Dependencies.Add(copyItem);
        //throw new NotImplementedException();
        return newId;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Dependency> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
