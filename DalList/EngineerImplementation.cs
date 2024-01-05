namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        DataSource.Engineers.Add(item);
        //throw new NotImplementedException();
        return item.Id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
