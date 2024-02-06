
using BlApi;
using BO;
using DO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace BlImplementation;


public class TaskImplementation : ITask
{
    // Access to Dl layer .
    private DalApi.IDal _dal = DalApi.Factory.Get;
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    static ProjectScheduled statusProject = e_bl.StatusProject;

    public void Create(BO.Task boTask)
    {
        //Chack the details of Task
        ChackDetails(boTask);

        // Create dpendent
        if (boTask.Dependencies != null)
        {
            foreach (var task in boTask.Dependencies)
            {
                DO.Dependency newDependency = new(task.Id, boTask.Id);
                try
                {
                    // Chack if the dependecy is not exist
                    _dal.Dependency.Create(newDependency);
                }
                catch (DalDoesExistException ex)
                {
                    throw new BO.BlAlreadyExistsException($"Dependency with ID={newDependency._id} already exist", ex);
                }

            }
        }
        
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
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        // Read throw exception if Task is not found 
        BO.Task? boTask = Read(id);

        //if boTask is not empty
        if (boTask != null)
        {
            // Check that the Task is not dependecy for other task.
            var chack = from dependency in _dal.Dependency.ReadAll()
                        where dependency._dependsOnTask == boTask.Id
                        select dependency;

            // This task cannot be deleted because it is a dependency of another task
            if (chack is not null)
            {
                throw new BO.BlEntityCanNotRemoveException("Can not remove this antity");
            }

            //If the test was successful - you will make an attempt to request deletion from the Data layer
            try
            {
                _dal.Task.Delete(id);
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException("The antity is not exist", ex);
            }
            catch (DO.DalCannotDeleted ex)
            {
                throw new BO.BlCannotDeletedException("Can not delete this antity", ex);
            }
        }
        
    }

    public BO.Task? Read(int id)
    {
        //Get from DL the Task that I search
        DO.Task? doTask = _dal.Task.Read(id);

        if (doTask != null)
        {
            // Convert him to BO and return him.
            BO.Task boTask = TurnTaskToBo(doTask);
            return boTask;
        }
        //If is not exist
        throw new BO.BlReadNotFoundException($"Task with ID={doTask?._id} is not exist");
    }

    public BO.Task? Read(Func<BO.Task, bool> filter)
    {
        //Get from DL the Task that I search
        DO.Task? doTask = _dal.Task.Read(x => filter(TurnTaskToBo(x)));

        if (doTask != null)
        {
            // Convert him to BO and return him.
            BO.Task boTask = TurnTaskToBo(doTask);
            return boTask;
        }
        //If is not exist
        throw new BO.BlReadNotFoundException("Engineer is not exist");
    }

    public IEnumerable<BO.TaskInList?> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        //Create object of TaskInList from every Task in DataBase, and return him
        if (filter != null)
        { 
            var result = from task in _dal.Task.ReadAll(x => filter(TurnTaskToBo(x)))
                         let statusOfTask = (Status)BringStatus(task._startDate, task._scheduledDate, task._completeDate)
                         select new TaskInList
                         {
                             Alias = task._alias,
                             Description = task._description,
                             Id = task._id,
                             Status = statusOfTask
                         };

            return result;
        }

        var result1 = from task in _dal.Task.ReadAll()
                     let statusOfTask = (Status)BringStatus(task._startDate, task._scheduledDate, task._completeDate)
                     select new TaskInList
                     {
                         Alias = task._alias,
                         Description = task._description,
                         Id = task._id,
                         Status = statusOfTask
                     };
        return result1;

    }


    public void Update(BO.Task boTask)
    { 
        // Chack the details Task 
        ChackDetails(boTask);

        if (statusProject == ProjectScheduled.ScheduleDetermination)
        {
            boTask = ChackUpdate(boTask);
        }

        try
        {
            _dal.Task.Update(TurnTaskToDo(boTask));
        }
        catch(DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException("The Task is not exist",ex);
        }
    }

    

    private BO.Task TurnTaskToBo(DO.Task doTask)
    {
        // Doing linq to fill the files of EngineerInTask
        var resulte = (from DO.Engineer engineer in _dal.Engineer.ReadAll()
                       where engineer._id == doTask._engineerId
                       select engineer).FirstOrDefault();

        //Create an object of type EngineerInTask to initialize
        BO.EngineerInTask engineerInTask = new();
         if (resulte == null)
        {
           engineerInTask.Name= "";
            engineerInTask.Id = 0;
        }
        else
        {
            engineerInTask.Id= resulte._id;
            engineerInTask.Name = resulte._name;
        }

        BO.Task? boTask = new BO.Task()
        {
            // TODO: להכניס את השדות של התאריכים עם החישוב (כלומר לאחר החישוב) ש
            Id = doTask._id,
            Alias = doTask._alias,
            Description = doTask._description,
            CanToRemove = doTask._canToRemove,
            Remarks = doTask._remarks,
            Active = doTask._active,
            RequiredEffortTime = doTask._requiredEffortTime,
            Copmliexity = doTask._copmliexity,
            DeadLineDate = doTask._deadLineDate,
            ScheduledDate = doTask._scheduledDate,
            Deliverables = doTask._deliverables,


            StartDate = doTask._startDate,// think that we gave the start date in 
            Engineer = engineerInTask,
            CompleteDate = doTask._completeDate,
            CreatedAtDate = doTask._createdAtDate,
            Milestone = null,
            Status = (Status)(BringStatus(doTask._startDate, doTask._scheduledDate, doTask._completeDate)),
            Dependencies = BringDendencies(doTask),

            //ForcastDate = Max(StartDate ,ScheduledDate) + RequiredEffortTime .
            ForcastDate = CalculateForcastDate(MaxDate(doTask._startDate, doTask._scheduledDate), doTask._requiredEffortTime),
        };
       
        return boTask;
    }

    //////////////////////////////////Help function////////////////////////////////////

    private void ChackDetails(BO.Task boTask)
    {

        if (boTask.Id < 0)
        {
            throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boTask.Id}");
        }

        if (boTask.CreatedAtDate == null)
        {
            throw new BlNullPropertyException($"You did not add value for : CreatedAtDate");
        }

        if (boTask.ScheduledDate == null && statusProject != ProjectScheduled.planning)
        {
            throw new BlNullPropertyException($"You did not add value for : ScheduledDate");
        }

        if (boTask.Alias == "")
        {
            throw new BlNullPropertyException($"You did not add value for : Alias");
        }

        if (boTask.RequiredEffortTime == null)
        {
            throw new BlNullPropertyException($"You did not add value for : RequiredEffortTime");
        }

        if (boTask.StartDate == null && statusProject != ProjectScheduled.planning)
        {
            throw new BlNullPropertyException($"You did not add value for : StartDate");
        }

        if (boTask.Description == "")
        {
            throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: Description");
        }

        if (boTask.CompleteDate == null && statusProject != ProjectScheduled.planning)
        {
            throw new BlNullPropertyException($"You did not add value for : CompleteDate");
        }
        if (statusProject != ProjectScheduled.planning)
        {

            if (boTask.StartDate < boTask.CreatedAtDate)
            {
                throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boTask.StartDate}");
            }

            if (boTask.CompleteDate < boTask.CreatedAtDate || boTask.CompleteDate < boTask.StartDate || boTask.CompleteDate < boTask.ScheduledDate)
            {
                throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boTask.StartDate}");
            }
        }

        //if(boTask.RequiredEffortTime.Value.Days <= 0)
        //{
        //    throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boTask.RequiredEffortTime}");
        //}


    }

    /// <summary>
    /// Convert Bo to Do .
    /// </summary>
    /// <param name="boTask"></param>
    /// <returns></returns>
    private DO.Task TurnTaskToDo(BO.Task boTask)
    {
        // Get the Task that engineer work about .
        //var task_result = (from DO.Task task in _dal.Task.ReadAll()
        //                   where task._id == boTask.Id
        //                   select task).FirstOrDefault();
        EngineerInTask engineerInTask = new EngineerInTask();
        if (boTask.Engineer == null)
        {           
                engineerInTask.Id = 0;
                engineerInTask.Name = "";
        }
        else
        {
            engineerInTask.Id = boTask.Engineer.Id;
            engineerInTask.Name = boTask.Engineer.Name;
        }
        // Do details from Bo .
        DO.Task doTask = new(boTask.CreatedAtDate, boTask.RequiredEffortTime, boTask.Copmliexity, boTask.StartDate, boTask.ScheduledDate,
            boTask.CompleteDate, boTask.DeadLineDate, boTask.Alias, boTask.Description, boTask.Deliverables, boTask.Remarks,
            boTask.Id,engineerInTask.Id, boTask.Active, _isMilestone: false, boTask.CanToRemove);
        return doTask;
    }

    private int BringStatus(DateTime? StartDate, DateTime? ScheduledDate, DateTime? CompleteDate)
    { 
        
        if (ScheduledDate == null)//Unscheduled 
        {
            return 0;
        }
        else
        if (ScheduledDate < StartDate)//Scheduled 
        {
            return 1;
        }
        else
        if (ScheduledDate < CompleteDate && ScheduledDate > StartDate)//OnTrack  
        {
            return 2;
        }
        else if(ScheduledDate == CompleteDate)// Done 
        {
            return 3;
        }
        return 0; 
    }

    /// <summary>
    /// Return List of all tasks that boTask dependent in them
    /// </summary>
    /// <param name="boTask"></param>
    /// <returns></returns>
    private List<BO.TaskInList>? BringDendencies(DO.Task doTask)
    {
        // Get all tasks that boTask dependen on .
        var listOfDependencies = from dependency in _dal.Dependency.ReadAll(x => x._dependsOnTask == doTask._id)
                               // Search the task that boTask dependent on her in Dal every time .
                           let dependentTasks = _dal.Task.Read(dependency._dependentTask)
                           // Create new TaskInList and adds it to the list .
                           select new TaskInList
                           {
                               Id = dependentTasks._id,
                               Description = dependentTasks._description,
                               Alias = dependentTasks._alias,
                               Status = (Status)BringStatus(dependentTasks._startDate, dependentTasks._scheduledDate, dependentTasks._completeDate)
                           };
        return listOfDependencies.ToList();
    }


    /// <summary>
    /// Check the update after the input value .
    /// </summary>
    /// <param name="boTask"></param>
    /// <exception cref="NotImplementedException"></exception>
    private BO.Task ChackUpdate(BO.Task boTask)
    {
        // Have a Start Date
        if (boTask.CreatedAtDate != null)
        {
            boTask.Status = Status.OnTrack;
        }
        // Have a Complete Date
        if (boTask.CompleteDate != null)
        {
            boTask.Status = Status.Done;
        }
        return boTask;
    }


    /// <summary>
    /// Return Max(startDat ,scheduledDate)
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="scheduledDate"></param>
    /// <returns></returns>
    private DateTime? MaxDate(DateTime? startDate , DateTime? scheduledDate)
    {
        if (startDate.HasValue && scheduledDate.HasValue)
        {
            if (startDate.Value.Month > scheduledDate.Value.Month)
            {
                return startDate.Value;
            }
            // Both in the same month .
            if (startDate.Value.Day > scheduledDate.Value.Day)
            {
                return startDate.Value;
            }
            return scheduledDate.Value;
        }
        return null;
    }

    /// <summary>
    /// Calculate the ForcastDate field .
    /// </summary>
    /// <param name="maxDate"></param>
    /// <param name="requiredEffortTime"></param>
    /// <returns></returns>
    private DateTime? CalculateForcastDate (DateTime? maxDate , TimeSpan? requiredEffortTime)
    {
        return maxDate + requiredEffortTime;
    }


    /// <summary>
    /// The function returns all the tasks that the same task we received as a parameter depends on
    /// </summary>
    /// <param name="boTask"></param>
    /// <returns></returns>
    public IEnumerable<BO.Task?> BringTasksDependsOn(BO.Task boTask)
    {
        //Bring all taks that BO.Task.id dependet on them.
        var listOfDependencies = from dependency in _dal.Dependency.ReadAll(x => x._dependentTask == TurnTaskToDo(boTask)._id)

                                 let dependentOnTasks = _dal.Task.Read(dependency._dependsOnTask)
                                 // Select new BO.Task and adds it to the list .
                                 select TurnTaskToBo(dependentOnTasks);

        return listOfDependencies;
    }

}


