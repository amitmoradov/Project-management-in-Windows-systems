
using BlApi;
using BO;

namespace BlImplementation;


public class TaskImplementation : ITask
{
    // Access to Dl layer .
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void Create(BO.Task boTask)
    {
        //Chack the details of Task
        ChackDetails(boTask);

        //Convert the details to Data Base Layer
        DO.Task doTask = TurnTaskToDo(boTask);

        //Try to Save the details of new Task in Data Base.
        try
        {
            _dal.Task.Create(doTask);
        }

        //Catch the exception from DL and add more exception from this Layer.
        catch(DO.DalDoesExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boTask.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Task? Read(int id)
    {

        throw new NotImplementedException();
    }

    public BO.Task? Read(Func<BO.Task, bool> filter)
    {

        throw new NotImplementedException();
    }

    public IEnumerable<BO.Task?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }


    private void ChackDetails(BO.Task boTask)
    {

       if (boTask.Id < 0) 
        {
            throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boTask.Id}");
        }
        if (boTask.Alias == "")
        {
            throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boTask.Alias}");
        }
    }


    private BO.Task TurnTaskToBo(DO.Task doTask)
    {
        //Return from the list of tasks the one task that the engineer is working on
        var resulte = (from DO.Task task in _dal.Task.ReadAll()
                       where doEngineer._id == task._engineerId
                       select task).FirstOrDefault();

        //Create an object of type TaskInEngineer to initialize the fields in the Engineer class
        BO.TaskInEngineer taskInEngineer = new()
        {
            Id = resulte._id,
            Alias = resulte._alias
        };

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

    //////////////////////////////////Help function////////////////////////////////////


    /// <summary>
    /// Convert Bo to Do .
    /// </summary>
    /// <param name="boTask"></param>
    /// <returns></returns>
    private DO.Task TurnTaskToDo(BO.Task boTask)
    {
        // Get the Task that engineer work about .
        var task_result = (from DO.Task task in _dal.Task.ReadAll()
                           where task._id == boTask.Id
                           select task).FirstOrDefault();

        // Do details from Bo .
        DO.Task doTask = new(boTask.CreatedAtDate, boTask.RequiredEffortTime, boTask.Copmliexity, boTask.StartDate, boTask.ScheduledDate,
            boTask.CompleteDate, boTask.DeadLineDate, boTask.Alias, boTask.Description, boTask.Deliverables, boTask.Remarks,
            boTask.Id, task_result._engineerId, boTask.Active, _isMilestone: false, boTask.CanToRemove);
        return doTask;
    }

    private int BringStatus(DateTime StartDate, DateTime? ScheduledDate, DateTime? CompleteDate)
    {
        if (ScheduledDate == null)//Unscheduled אין תאריך התחלה מתוכנן
        {
            return 0;
        }
        else
        if (ScheduledDate < StartDate)//Scheduled לוח הזמנים תוכנן אך היא עוד לא התחילה להתבצע
        {
            return 1;
        }
        else
        if (ScheduledDate < CompleteDate && ScheduledDate > StartDate)//OnTrack  המטלה התחילה להתבצע אך עוד לא הסתיימה
        {
            return 2;
        }
        else if(ScheduledDate == CompleteDate)// Done ביצוע המטלה הסתיים 
        {
            return 3;
        }
    }
}


