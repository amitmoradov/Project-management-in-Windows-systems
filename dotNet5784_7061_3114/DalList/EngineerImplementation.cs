namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        
        Engineer? engineer = Read(item._id);
        if (engineer == null)
        {
            DataSource.Engineers.Add(item);
            return item._id;
        }
        // if the object is exist
        throw new DalDoesExistException($"Engineer with ID={item._id} is exists");
    }

    public void Delete(int id)
    {
        Engineer? engineer = Read(id);
        // The object can to remove
        if (engineer is not null && engineer._canToRemove)
        {
            DataSource.Engineers.Remove(engineer);
            return;
        }
        if (engineer is not null && !engineer._canToRemove)
        {
            throw new DalCannotDeleted($"Engineer with ID={id} cannot be deleted");
        }
        throw new DalDoesNotExistException($"Engineer with ID={id} is Not exists");
    }
    

    public Engineer? Read(int id)
    {
        return DataSource.Engineers.FirstOrDefault(engineer => engineer._id == id);

        //foreach (var engineer in DataSource.Engineers)
        //{
        //    if (engineer._id == id)
        //    {
        //        return engineer;
        //    }
        //}
        //return null;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter != null)
        {
            return from item in DataSource.Engineers
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Engineers
               select item;

        //return new List<Engineer>(DataSource.Engineers);
        //throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        if (item is not null)
        {
            //Delete the old engineer with same id
            Delete(item._id);
            Create(item);
            
            return;
        }
        throw new DalDoesNotExistException($"Engineer with ID={item!._id} is Not exists");
    }
}
