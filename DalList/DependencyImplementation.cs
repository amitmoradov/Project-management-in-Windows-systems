namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Implementation of interface method .
/// </summary>
public class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        // chack if the item is exist
        if (Read(item.Id) == null)
        {
        // Get the current run nummber .
        int newId = DataSource.Config.NextDependencyId;
            // Copy of item and change Id .
            Dependency copyItem = item with { Id = newId };

            DataSource.Dependencies.Add(copyItem);
            //throw new NotImplementedException();
            return newId;
        }
        // if the object is exist
        throw new Exception($"Dependency with ID={item.Id} is exicts");
    }

    public void Delete(int id)
    {
        foreach (Dependency dependency in DataSource.Dependencies)
        {
            // The object can to remove
            if (dependency.Id == id && dependency.canToRemove) 
            {
                DataSource.Dependencies.Remove(dependency);
            }

            if (!dependency.canToRemove)
            {
                throw new Exception($"Dependency with ID={id} cannot be deleted");
            }
        }
        // if the object is not exist
        throw new Exception($"Dependency with ID={id} is Not exicts");
    }

    public Dependency? Read(int id)
    {
        foreach (var item in DataSource.Dependencies)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        return null;
       //throw new NotImplementedException();
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
        // throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
       foreach(var dependency in DataSource.Dependencies)
        {
           if(dependency.Id == item.Id)
            {
                Delete(dependency.Id);
                Create(item);
            }
        }
        throw new($"");
    }
}
