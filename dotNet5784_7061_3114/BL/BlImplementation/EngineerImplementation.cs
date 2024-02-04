
using BlApi;
using BO;
using DO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace BlImplementation;
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {
        DO.Engineer doEngineer = TurnEngineerToDo(boEngineer);

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
        // Read throw exception if engineer is not found 
         BO.Engineer? boEngineer = Read(id);

         // Checks that the engineer is not performing a task
         if (boEngineer?.Task != null)
         {
            throw new BO.BlEntityCanNotRemoveException("Can not remove this antity");
         }

        //If the test was successful - you will make an attempt to request deletion from the Data layer
        try
        {
            _dal.Engineer.Delete(id);
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException("The antity is not exist", ex);
        }
        catch(DO.DalCannotDeleted ex)
        {
            throw new BO.BlCannotDeletedException("Can not delete this antity", ex);
        }
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer != null)
        {
            BO.Engineer boEngineer = TurnEngineerToBo(doEngineer);

            return boEngineer;
        }
         throw new BO.BlReadNotFoundException($"Engineer with ID={doEngineer?._id} is not exist");
    }

    public BO.Engineer? Read(Func<BO.Engineer, bool> filter)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(x=>filter(TurnEngineerToBo(x)));
        if (doEngineer != null)
        {
            BO.Engineer boEngineer = TurnEngineerToBo(doEngineer);
            return boEngineer;
        }
        throw new BO.BlReadNotFoundException("Engineer is not exist");
    }

    public IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        if (filter != null)
        {
            //Get all engineers from DL Layer that up in condition
            var listEngineers = from DO.Engineer doEngineer in _dal.Engineer.ReadAll(x => filter(TurnEngineerToBo(x)))
                                   // Save dateils of Engineer in listEngineers
                               let engineer = Read(doEngineer._id)
                               select engineer;
            return listEngineers;
        }
        //Get all engineers from DL Layer without condition
        var listEngineer = from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
                               // Save dateils of Engineer in listEngineer
                           let engineer = Read(doEngineer._id)
                           select engineer;
        return listEngineer;
    }

    public void Update(BO.Engineer boEngineer)
    {
        // Get the previous details engineer 
        BO.Engineer previousEngineer = Read(boEngineer.Id);

        ChackDetails(boEngineer);

        //Checks if the engineer's level hasn't dropped
        if (boEngineer.Level < previousEngineer?.Level)
        {
            throw new BO.BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boEngineer.Level}");
        }

        if (previousEngineer?.Task?.Id != boEngineer?.Task?.Id)
        {
            //Return from the list of tasks the one task that the engineer is working on
            var resulte = (from DO.Task task in _dal.Task.ReadAll()
                           where previousEngineer?.Id == task._engineerId
                           select task).FirstOrDefault();

            //לבדוק אם לשנות לאחר שניצור את המחקלה שלTask BL
            // כולל שינוי שם המהנדס
            DO.Task newTask = new(resulte._createdAtDate, resulte._requiredEffortTime, resulte._copmliexity, resulte._startDate,resulte._scheduledDate,resulte._completeDate,resulte._deadLineDate,resulte._alias,resulte._description,resulte._deliverables,resulte._remarks,resulte._id,boEngineer.Id,resulte._active,resulte._isMilestone,resulte._canToRemove);
            _dal.Task.Update(newTask);
        }
        //Save the change un Data Base.
        DO.Engineer doEngineer = TurnEngineerToDo(boEngineer);
        _dal.Engineer.Update(doEngineer);
    }



   //////////////////////////////////////////////Help function//////////////////////////////////////////////////

    /// <summary>
    ///  Chack input vaildity
    /// </summary>
    /// <param name="boEngineer"></param>
    private void ChackDetails(BO.Engineer boEngineer) 
    {
        if (boEngineer.Cost == null)
        {
            throw new BlNullPropertyException($"You did not add value for : Cost");
        }

        if (boEngineer.Email == null)
        {
            throw new BlNullPropertyException($"You did not add value for : Email");
        }

        if (boEngineer.Name == "")
        {
            throw new BO.BlNullPropertyException($"You did not add value for : Name");
        }

        if(boEngineer.Level == null)
        {
            throw new BlNullPropertyException($"You did not add value for : Level");
        }

        if (boEngineer.Id < 200000000 || boEngineer.Id >  400000000)
        {
            throw new BO.BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boEngineer.Id}");
        }

        if(boEngineer.Cost < 0)
        {
            throw new BO.BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boEngineer.Cost}");
        }

        if (IsValidEmail(boEngineer.Email) == false)
        {
            throw new BO.BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boEngineer.Email}");
        }
    }

    /// <summary>
    /// Function that chack the email is correct.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    private bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.EndsWith(".com", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Turn the Engineer from Data Layer to Bussinus Layer
    /// </summary>
    /// <param name="doEngineer"></param>
    /// <returns></returns>
    private BO.Engineer TurnEngineerToBo(DO.Engineer doEngineer)
    {
        //Return from the list of tasks the one task that the engineer is working on
        var resulte = (from DO.Task task in _dal.Task.ReadAll()
                       where doEngineer._id == task._engineerId
                       select task).FirstOrDefault();

        //Create an object of type TaskInEngineer to initialize the fields in the Engineer class
        BO.TaskInEngineer taskInEngineer = new();
        if (resulte == null)
        {
            taskInEngineer.Id = 0;
            taskInEngineer.Alias = null;
        }
        else
        {
            taskInEngineer.Id = resulte._id;
            taskInEngineer.Alias = resulte._alias;
        }

        BO.Engineer? boEngineer = new BO.Engineer()
        {
            Id = doEngineer._id,
            Email = doEngineer._email,
            Cost = doEngineer._cost,
            CanToRemove = doEngineer._canToRemove,
            Level = doEngineer._level,
            Active = doEngineer._active,
            Name = doEngineer._name,
            Task = taskInEngineer,
        };

        return boEngineer;
    }

    /// <summary>
    /// Turn the Engineer from Bussines Layer to Data Layer.
    /// </summary>
    /// <param name="boEngineer"></param>
    /// <returns></returns>
    private DO.Engineer TurnEngineerToDo(BO.Engineer boEngineer)
    {

        DO.Engineer doEngineer = new DO.Engineer
       (boEngineer.Id, boEngineer.Cost, boEngineer.Level, boEngineer.Email, boEngineer.Name, boEngineer.Active, boEngineer.CanToRemove);

        return doEngineer;
    }
}
