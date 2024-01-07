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
        Dependency? dependency = Read(item.Id);
        if (dependency == null)
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
        throw new Exception($"Dependency with ID={item.Id} is exists");
    }

    public void Delete(int id)
    {
        Dependency? dependency = Read(id);
        // The object can to remove
        if (dependency is not null && dependency.canToRemove) 
        {
            DataSource.Dependencies.Remove(dependency);
            return;
        }

        if (dependency is not null && !dependency.canToRemove)
        {
            throw new Exception($"Dependency with ID={id} cannot be deleted");
        }
        
        // If the object is not exist
        throw new Exception($"Dependency with ID={id} is Not exists");
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
    }

    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        Dependency? dependency = Read(item.Id);
        if(dependency is not null)
        {
            Delete(dependency.Id);
            Create(item);
            return;
        }
        throw new Exception($"Dependency with ID={item.Id} is not exists");
    }
}
