
using BlApi;
using BO;
using DO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = new DO.Engineer
       (boEngineer.Id, boEngineer.Cost, boEngineer.Level, boEngineer.Email, boEngineer.Name, boEngineer.Active, boEngineer.CanToRemove);
        try
        {
            // Checks the details and if they are incorrect, throws an error
            ChackDetails(boEngineer);
            
            // Create of DAL
            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }

        catch (DO.DalDoesExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }

    }

    public void Delete(int id)
    {
         BO.Engineer? engineer = Read(id);

         // Checks that the engineer is not performing a task
         if (engineer?.Task != null)
         {
            throw new BO.BlEntityCanNotRemoveException($"Can not remove this engineer - The engineer is on a task");
         }

         //If the test was successful - you will make an attempt to request deletion from the data layer
         _dal.Engineer.Delete(id);

        // צריך דעת אם המהנדס סיים משימה או שהוא באמצע משימה

    }

    public BO.Engineer? Read(int id)
    {
        try
        {
            DO.Engineer? doEngineer = _dal.Engineer.Read(id);
            if (doEngineer != null)
            {
                BO.Engineer? boEngineer = new (doEngineer._id, doEngineer._cost, doEngineer._level, null, doEngineer._email, doEngineer._name, doEngineer._active, doEngineer._canToRemove);
            }
        }
        catch(DO.DalDoesNotExistException ex) 
        {
            throw new BO.BlDoesNotExistException("  ",ex);
        }
    }

    public BO.Engineer? Read(Func<BO.Engineer, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        //Get all engineers from layer DL
        var listEngineer = (from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                select new BO.Engineer
                {
                    Id = doEngineer._id,
                    Name = doEngineer._name,
                    Email = doEngineer._email,
                    CanToRemove = doEngineer._canToRemove,
                    Active = doEngineer._active,                 
                });

        // Chack if filter work on engineer
        if (filter != null)
        {
            listEngineer = listEngineer.Where(engineer => filter(engineer));
        }

        return listEngineer;

    }

    public void Update(BO.Engineer item)
    {
        throw new NotImplementedException();
    }

    //Help function

    /// <summary>
    ///  Chack input vaildity
    /// </summary>
    /// <param name="boEngineer"></param>
    private void ChackDetails(BO.Engineer boEngineer) 
    {
        if(boEngineer.Id < 200000000 || boEngineer.Id >  400000000)
        {
            throw new BO.BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boEngineer.Id}");
        }

        if(boEngineer.Cost < 0)
        {
            throw new BO.BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boEngineer.Cost}");
        }

        if(boEngineer.Name == "")
        {
            throw new BO.BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boEngineer.Name}");
        }

        if (boEngineer.Email == "")
        {
            throw new BO.BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boEngineer.Email}");
        }

    }
}
