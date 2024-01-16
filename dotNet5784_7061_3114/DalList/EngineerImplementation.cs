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
        throw new Exception($"Engineer with ID={item._id} is exists");
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
            throw new Exception($"Engineer with ID={id} cannot be deleted");
        }
        throw new Exception($"Engineer with ID={id} is Not exists");
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

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
        //throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        Engineer? engineer = Read(item._id);
        if (engineer is not null)
        {
            Delete(engineer._id);
            Create(item);
            return;
        }
        throw new Exception($"Engineer with ID={item._id} is Not exists");
    }
}
