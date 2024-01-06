namespace Dal;
using DalApi;
using DO;
using System.Collections;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        if (Read(item.Id) == null)
        {
            DataSource.Engineers.Add(item);
            //throw new NotImplementedException();
            return item.Id;
        }
        // if the object is exist
        throw new Exception($"Engineer with ID={item.Id} is exicts");
    }

    public void Delete(int id)
    {
        foreach (var engineer in DataSource.Engineers)
        {
            // The object can to remove
            if (engineer.Id == id && engineer.canToRemove)
            {
                DataSource.Engineers.Remove(engineer);
            }
            if (!engineer.canToRemove)
            {
                throw new Exception($"Engineer with ID={id} cannot be deleted");
            }

        }
        throw new Exception($"Engineer with ID={id} is Not exicts");
        //throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        foreach (var engineer in DataSource.Engineers)
        {
            if (engineer.Id == id)
            {
                return engineer;
            }
        }
        return null;
        //throw new NotImplementedException();
    }

    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
        //throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        foreach(var engineer in DataSource.Engineers)
        {
            if(engineer.Id == item.Id)
            {
                Delete(engineer.Id);
                Create(item);
                return;
            }
        }
        //throw new NotImplementedException(); ///// צריך לעשות פה זריקה
    }
}
