namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Implementation of interface method .
/// </summary>
internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        // chack if the item is exist
        Dependency? dependency = Read(item._id);
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
        //return new List<Dependency>(DataSource.Dependencies);
    }

    public void Update(Dependency item)
    {
        Dependency? dependency = Read(item._id);
        if(dependency is not null)
        {
            Delete(dependency._id);
            Create(item);
            return;
        }
        throw new DalDoesNotExistException($"Dependency with ID={item._id} is not exists");
    }
}
