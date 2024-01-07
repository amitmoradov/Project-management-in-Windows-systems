namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        
        Engineer? engineer = Read(item.Id);
        if (engineer == null)
        {
            DataSource.Engineers.Add(item);
            return item.Id;
        }
        // if the object is exist
        throw new Exception($"Engineer with ID={item.Id} is exists");
    }

    public void Delete(int id)
    {
        Engineer? engineer = Read(id);
        // The object can to remove
        if (engineer is not null && engineer.canToRemove)
        {
            DataSource.Engineers.Remove(engineer);
            return;
        }
        if (engineer is not null && !engineer.canToRemove)
        {
            throw new Exception($"Engineer with ID={id} cannot be deleted");
        }
        throw new Exception($"Engineer with ID={id} is Not exists");
    }
    

    public Engineer? Read(int id)
    {
       // DataSource.Engineers.Find(engineer => engineer.Id == id);
        foreach (var engineer in DataSource.Engineers)
        {
            if (engineer.Id == id)
            {
                return engineer;
            }
        }
        return null;
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
        //throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        Engineer? engineer = Read(item.Id);
        if (engineer is not null)
        {
            Delete(engineer.Id);
            Create(item);
            return;
        }
        throw new Exception($"Engineer with ID={item.Id} is Not exists");
        //throw new NotImplementedException(); ///// צריך לעשות פה זריקה
    }
}
