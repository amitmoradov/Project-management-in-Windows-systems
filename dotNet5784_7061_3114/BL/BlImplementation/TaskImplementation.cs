
using BlApi;
using BO;
using DO;
using System.Security.Cryptography;

namespace BlImplementation;


public class TaskImplementation : ITask
{
    // Access to Dl layer .
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public void Create(BO.Task boTask)
    {

        //Chack the details of Task
        ChackDetails(boTask);

        // Create dpendent
        // לברר לגבי בלי לולאת foreach
        if (boTask.Dependencies != null)
        {
            foreach (var task in boTask.Dependencies)
            {
                DO.Dependency newDependency = new(boTask.Id, task.Id);
                _dal.Dependency.Create(newDependency);

            }
        }
        //var resulte = from task in boTask.Dependencies
        //              let currentTaskId = task.Id
        //              select new DO.Dependency
        //              {
        //                  _dependentTask = boTask.Id,
        //                  _dependsOnTask = currentTaskId
        //              }into _dal.Dependency.Create();


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
        //
        var resulte = (from DO.Engineer engineer in _dal.Engineer.ReadAll()
                       where engineer._id == doTask._engineerId
                       select engineer).FirstOrDefault();

        //Create an object of type EngineerInTask to initialize
        BO.EngineerInTask engineerInTask = new()
        {
            Id = resulte._id,
            Name = resulte._name,
        };

        BO.Task? boTask = new BO.Task()
        {
            // TODO: להכניס את השדות של התאריכים עם החישוב (כלומר לאחר החישוב) ש
            Id = doTask._id,
            Alias = doTask._alias,
            Description = doTask._description,
            CanToRemove = doTask._canToRemove,
            Remarks = doTask._remarks,
            //StartDate = doTask._startDate,
            Active = doTask._active,
            Engineer = engineerInTask,
            RequiredEffortTime = doTask._requiredEffortTime,
            Copmliexity = doTask._copmliexity,

            //CompleteDate = doTask._completeDate,    
            Status = (Status)(BringStatus(doTask._startDate, doTask._scheduledDate, doTask._completeDate)),
        };

        return boTask;
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

    private int BringStatus(DateTime? StartDate, DateTime? ScheduledDate, DateTime? CompleteDate)
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
        return 0; 
    }
}


