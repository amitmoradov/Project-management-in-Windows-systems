namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

/// <summary>
/// Implementation of interface method .
/// </summary>
internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        // chack if the item is exist
        Dependency? dependency = Read(item._id);
        // Check if the depency already exist .
        Dependency? checkDependency = Read(x => x._dependentTask == item._dependentTask && x._dependsOnTask == item._dependsOnTask);
        if(checkDependency != null)
        {
            return 0;
        }

        if (dependency == null)
        {
            // Get the current run nummber .
            int newId = DataSource.Config.NextDependencyId;

            // Copy of item and change Id .
            Dependency copyItem = item with { _id = newId };

            DataSource.Dependencies.Add(copyItem);
            //throw new NotImplementedException();
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
            DataSource.Dependencies.Remove(dependency);
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
        return DataSource.Dependencies.FirstOrDefault(dependency => dependency._id == id);

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

    }

    public void Update(Dependency item)
    {
        //ependency? dependency = Read(item._id);
        if(item is not null)
        {
            Delete(item._id);
            DataSource.Dependencies.Add(item);
            return;
        }
        throw new DalDoesNotExistException($"Dependency with ID={item._id} is not exists");
    }

    public void reset()
    {
        DataSource.Dependencies.Clear();
    }

}
