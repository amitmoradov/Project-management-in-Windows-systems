
using BlApi;
using BO;
using DO;
using BL;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using DalApi;
using System.Data;
using System.Threading.Tasks;
namespace BlImplementation;



internal class TaskImplementation : BlApi.ITask
{
    // Access to Dl layer .
    private DalApi.IDal _dal = DalApi.Factory.Get;
   
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();

    private readonly BL _bl;
    internal TaskImplementation(BL bl) => _bl = bl;



    public void Create(BO.Task boTask)
    {
        //Chack the details of Task
        ChackDetails(boTask);

        // Create dpendent
        if (boTask.Dependencies != null)
        {
            foreach (var task in boTask.Dependencies)
            {
                DO.Dependency newDependency = new(boTask.Id,task.Id);
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
        //Checks if I am in one step, if so, it is forbidden to assign movers to the task
        if (_dal.Project.ReturnStatusProject() == "planning")
        {
            boTask.Engineer = null;
        }
        //Convert the details to Data Base Layer
        DO.Task doTask = TurnTaskToDo(boTask);

        //Try to Save the details of new Task in Data Base.
        try
        {
            _dal.Task.Create(doTask);
        }

        //Catch the exception from DL and add more exception from this Layer.
        catch (DO.DalDoesExistException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={boTask.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        //Tasks can not deleted if I am in step 3
        if (_dal.Project.ReturnStatusProject() == "scheduleWasPalnned")
        {
            throw new BlAlreadyPalnedException("The schedule has already been initialized");
        }
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
        //throw new BO.BlReadNotFoundException($"Task with ID={doTask?._id} is not exist");
        return null;
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
        //throw new BO.BlReadNotFoundException("Engineer is not exist");
        return null;
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
        if (_dal.Project.ReturnStatusProject() != "scheduleWasPalnned")
            ChackDetails(boTask);
        else 
        {
            ChackDatailPlannedSatge(boTask);
        }
        if (_dal.Project.ReturnStatusProject() == "ScheduleDetermination")
        {
            boTask = UpdateStatus(boTask);
        }

        BO.Task privuseTask = Read(boTask.Id);
        if (privuseTask == null)
        {
            throw new BO.BlDoesNotExistException("The Task is not exist");
        }

        /*If I'm in step 3 then
        I can't fill in all the fields that's why
        I bring the unfilled fields from the data layer and there in the updated variable*/
        if (_dal.Project.ReturnStatusProject() == "scheduleWasPalnned")
        {
            //Checks if there is other engineer working on the task.
            if (privuseTask.Engineer.Id != 0 && privuseTask.Engineer.Id != boTask.Engineer.Id)
            {
                throw new BlEngineerWorkingOnTask("There is another engineer already working on the task");
            }

            boTask = UnifiyTasksForThePlannedStage(boTask);


            /*After we have merged the task attributes to the one that I want to update,
            we will check if he has changed the attribute of the engineer's ID*/
            //Check if update the field of Engineer id
            if (boTask.Engineer.Id != 0)
            {
                BO.Task? chackTask = new();
                // Serach if engineer working on another task .
                chackTask = e_bl.Task.Read(x => x.Engineer.Id == boTask.Engineer.Id);

                // If the engineer still working on another task .
                if (chackTask != null)
                {
                    if ((chackTask.Status.ToString() == "OnTrack") && chackTask.Id != boTask.Id)
                    {
                        throw new BO.BlEngineerWorkingOnAnotherTask("The engineer already working on another task");
                    }
                }

                //Checking whether the engineer is not at a low level to assign him the task
                BO.Engineer? checkLevelEngineer = e_bl.Engineer.Read(boTask.Engineer.Id);


                //Checks if a task has been assigned to an engineer - that is, the engineer is not null
                if (checkLevelEngineer != null)
                {
                    if (checkLevelEngineer.Level < boTask.Copmliexity)
                    {
                        boTask.Engineer = null;
                        throw new BO.BlEngineerIsNotTheAllowedLevel("The engineer level is low to choose this task");
                    }

                    //Updates the field TaskInEngineer of engineer to new task .
                    checkLevelEngineer.Task.Id = boTask.Id;
                    checkLevelEngineer.Task.Alias = boTask.Alias;
                    // Save the changes .
                    e_bl.Engineer.Update(checkLevelEngineer);
                    
                }
            }

        }
        try
        {
            _dal.Task.Update(TurnTaskToDo(boTask));          
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException("The Task is not exist", ex);
        }
    }

    public void ScheduleFieldsInitialization()
    {
        var allTask = BringAllFieldTaskList();
        Random rnd = new Random();

        // Define the start and end dates for the range

        DateTime startDateProject = _dal.Project.ReturnStartProjectDate();
        DateTime endDate = startDateProject.AddYears(1);
        int countDays = 0;
        foreach (var task in allTask.ToList())
        {
            countDays = countDays + 10;
            //int randomDays = rnd.Next(1, (endDate - startDateProject).Days);
            DateTime randomDate = startDateProject.AddDays(countDays);
            //A random variable that holds a date between 2024.04.11 and 2024.12.31
            Update(task.Id, randomDate);
        }
        // Status project change from 2 to 3 .
        _dal.Project.SaveChangeOfStatus("scheduleWasPalnned");
    }


    public void Update(int idTask, DateTime scheduledDate)
    {
        
        BO.Task boTask = Read(idTask);

        if (!(boTask.Dependencies.All(x => Read(x.Id).ScheduledDate != null)))
        {
           throw new BlCannotUpdateException("The previous tasks did not reach to scheduled");
        }
        //Then, the action will check that the date received as a parameter is not earlier than all the estimated end dates of all tasks preceding it.
        //Otherwise an exception will be thrown
        if (!(boTask.Dependencies.All(x => Read(x.Id).ForcastDate <= scheduledDate)))
        {
           
            throw new BlCannotUpdateException("The received date is not earlier than all the estimated end dates of all the tasks preceding it");
        }

        boTask.Status = (Status)BringStatus(boTask.StartDate, boTask.ScheduledDate, boTask.CompleteDate);
        boTask.ScheduledDate = scheduledDate;

        // Update the DateBase.
        _dal.Task.Update(TurnTaskToDo(boTask));

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
            engineerInTask.Name = "";
            engineerInTask.Id = 0;
        }
        else
        {
            engineerInTask.Id = resulte._id;
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
            Copmliexity = (BO.EngineerExperience)doTask._copmliexity,
            DeadLineDate = doTask._deadLineDate,
            ScheduledDate = doTask._scheduledDate,
            Deliverables = doTask._deliverables,


            StartDate = doTask._startDate,// think that we gave the start date in 
            Engineer = engineerInTask,
            CompleteDate = doTask._completeDate,
            CreatedAtDate = doTask._createdAtDate,
            Milestone = null,
            Status = (Status)BringStatus(doTask._startDate, doTask._scheduledDate, doTask._completeDate),
            Dependencies = BringDendencies(doTask),

            //ForcastDate = Max(StartDate ,ScheduledDate) + RequiredEffortTime .
            ForcastDate = CalculateForcastDate(MaxDate(doTask._startDate, doTask._scheduledDate), doTask._requiredEffortTime),
        };


        return boTask;
    }

    /////////////////////////////////////////////////////////////Help function//////////////////////////////////////////////////////////////////////////////////////

    private void ChackDetails(BO.Task boTask)
    {
        if (boTask.Id < 0)
        {
            throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boTask.Id}");
        }
        if (boTask.Alias == "")
        {
            throw new BlNullPropertyException($"You did not add value for : Alias");
        }
         if (boTask.Description == "")
         {
            throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: Description");
         }
        if (boTask.CreatedAtDate == null && _dal.Project.ReturnStatusProject() != "scheduleWasPalnned")
        {
            throw new BlNullPropertyException($"You did not add value for : CreatedAtDate");
        }
        if (boTask.RequiredEffortTime == null && _dal.Project.ReturnStatusProject() != "scheduleWasPalnned")
        {
            throw new BlNullPropertyException($"You did not add value for : RequiredEffortTime");
        }

    }
   
    private void ChackDatailPlannedSatge(BO.Task boTask)
    {

        if (boTask.Id < 0)
        {
            throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boTask.Id}");
        }

        if (boTask.Alias == "")
        {
            throw new BlNullPropertyException($"You did not add value for : Alias");
        }

        if (boTask.Description == "")
        {
            throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: Description");
        }

        //if (boTask.CreatedAtDate == null && _dal.Project.ReturnStatusProject() != "scheduleWasPalnned")
        //{
        //    throw new BlNullPropertyException($"You did not add value for : CreatedAtDate");
        //}

        //if (boTask.RequiredEffortTime == null && _dal.Project.ReturnStatusProject() != "scheduleWasPalnned")
        //{
        //    throw new BlNullPropertyException($"You did not add value for : RequiredEffortTime");
        //}

       
        if (_dal.Project.ReturnStatusProject() != "planning")
        {

            //if (boTask.ScheduledDate == null)
            //{
            //    throw new BlNullPropertyException($"You did not add value for : ScheduledDate");
            //}

            //if (boTask.StartDate == null)
            //{
            //    throw new BlNullPropertyException($"You did not add value for : StartDate");
            //}

            //if (boTask.CompleteDate == null)
            //{
            //    throw new BlNullPropertyException($"You did not add value for : CompleteDate");
            //}
            if (boTask.Engineer != null && boTask.Engineer.Id !=0)
            {
               if (boTask.StartDate == null)
               {
                    throw new BlNullPropertyException($"You did not add value for : StartDate , when you try to add task to engineer");
               }
            }
            if (boTask.StartDate != null)
            {
                if (boTask.StartDate < boTask.CreatedAtDate)
                {
                    throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boTask.StartDate}");
                }

                //Can not Update the start date without update the engineer that work on the task.
                if (boTask.Engineer.Id == 0)
                {
                    throw new BO.BlCannotUpdateException("Can not Update the start date without update the engineer that work on the task, please try again");
                }
            }
            if (boTask.CompleteDate != null && boTask.StartDate != null)
            {
                if (boTask.CompleteDate < boTask.CreatedAtDate || boTask.CompleteDate < boTask.StartDate || boTask.CompleteDate < boTask.ScheduledDate)
                {
                    throw new BlIncorrectDatailException($"You have entered an incorrect item. What is wrong is this: {boTask.StartDate}");
                }
            }
        }

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
        DO.Task doTask = new(boTask.CreatedAtDate, boTask.RequiredEffortTime, (DO.EngineerExperience)boTask.Copmliexity, boTask.StartDate, boTask.ScheduledDate,
            boTask.CompleteDate, boTask.DeadLineDate, boTask.Alias, boTask.Description, boTask.Deliverables, boTask.Remarks,
            boTask.Id, engineerInTask.Id, boTask.Active, _isMilestone: false, boTask.CanToRemove);
        return doTask;
    }

    private int BringStatus(DateTime? StartDate, DateTime? ScheduledDate, DateTime? CompleteDate)
    {

        if (ScheduledDate == null)//Unscheduled 
        {
            return 0;
        }
        if (ScheduledDate != null && StartDate == null)//Scheduled 
        {
            return 1;
        }
        if (StartDate != null && CompleteDate == null)//OnTrack  
        {
            return 2;
        }
        if (CompleteDate != null)// Done 
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
        var listOfDependencies = from dependency in _dal.Dependency.ReadAll(x => x._dependentTask == doTask._id)
                                     // Search the task that boTask dependent on her in Dal every time .
                                 let dependentOnTasks = _dal.Task.Read(dependency._dependsOnTask)
                                 // Create new TaskInList and adds it to the list .
                                 select new TaskInList
                                 {
                                     Id = dependentOnTasks._id,
                                     Description = dependentOnTasks._description,
                                     Alias = dependentOnTasks._alias,
                                     Status = (Status)BringStatus(dependentOnTasks._startDate, dependentOnTasks._scheduledDate, dependentOnTasks._completeDate)
                                 };
        return listOfDependencies.ToList();
    }


    /// <summary>
    /// Check the update after the input value .
    /// </summary>
    /// <param name="boTask"></param>
    /// <exception cref="NotImplementedException"></exception>
    private BO.Task UpdateStatus(BO.Task boTask)
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

    //-------------------------------------- Help functoin for UPDATE with two parameters (ScheduleDetermination of status project) --------------------------------------------------------------------------------
    /// <summary>
    /// Return Max(startDat ,scheduledDate)
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="scheduledDate"></param>
    /// <returns></returns>
    private DateTime? MaxDate(DateTime? startDate, DateTime? scheduledDate)
    {
        if (scheduledDate != null && startDate == null)
        {
            return scheduledDate;
        }
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
    private DateTime? CalculateForcastDate(DateTime? maxDate, TimeSpan? requiredEffortTime)
    {
        return maxDate + requiredEffortTime;
    }


    /// <summary>
    /// The function returns all the tasks that the same task we received as a parameter depends on
    /// </summary>
    /// <param name="boTask"></param>
    /// <returns></returns>
    private IEnumerable<BO.Task?> BringTasksDependsOn(BO.Task boTask)
    {
        //Bring all taks that BO.Task.id dependet on them.
        var listOfDependencies = from dependency in _dal.Dependency.ReadAll(x => x._dependentTask == TurnTaskToDo(boTask)._id)

                                 let dependentOnTasks = _dal.Task.Read(dependency._dependsOnTask)
                                 // Select new BO.Task and adds it to the list .
                                 select TurnTaskToBo(dependentOnTasks);

        return listOfDependencies;
    }


    /// <summary>
    /// Return max Scheduled Date
    /// </summary>
    /// <param name="taskDependOn"></param>
    /// <returns></returns>
    private DateTime GetMaxScheduledDate(IEnumerable<BO.Task?> taskDependOn)
    {
        //We just gave a date to the max variable.
        DateTime maxDate = _dal.Project.ReturnStartProjectDate();
        foreach (var task in taskDependOn)
        {
            //Checks whether task is bigger in terms of years.
            if (task.ScheduledDate + task.RequiredEffortTime > maxDate)
            {
                maxDate = task.ScheduledDate.Value;
            }
        }
        return maxDate;
    }


    /// <summary>
    /// Return if the previous tasks have a Scheduled date
    /// </summary>
    /// <param name="taskDependOn"></param>
    /// <returns></returns>
    private bool ScheduledDateHasValue(IEnumerable<BO.Task?> taskDependOn)
    {
        foreach (BO.Task task in taskDependOn)
        {
            if (task.ScheduledDate is null)
            {
                return false;
            }
        }
        return true;
    }


    /// <summary>
    /// Brings all the fields of BO.Task (Return List of BO.Task)
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.Task> BringAllFieldTaskList()
    {
        var tasksInList = ReadAll();
        var allFieldTaskList = from task in tasksInList
                               let fullTask = Read(task.Id)
                               select fullTask;

        return allFieldTaskList;
    }

    /// <summary>
    ///  I can't fill in all the fields that's why
    ///I bring the unfilled fields from the data layer and there in the updated variable
    ///
    ///The function only unites the fields of the task without any logic
    /// </summary>
    /// <param name="needUpdate"></param>
    /// <returns></returns>
    private BO.Task UnifiyTasksForThePlannedStage(BO.Task needUpdate)
    {
        /*If I'm in step 3 then
           I can't fill in all the fields that's why
           I bring the unfilled fields from the data layer and there in the updated variable*/

        BO.Task? privuseTask = Read(needUpdate.Id);
        BO.Task afterUpdateTask = new BO.Task()
        {
                RequiredEffortTime = privuseTask.RequiredEffortTime,
                CreatedAtDate = privuseTask.CreatedAtDate,              
                ScheduledDate = privuseTask.ScheduledDate,
                Dependencies = privuseTask.Dependencies,
                ForcastDate = privuseTask.ForcastDate, // Calculate every call to Read .


                Id = needUpdate.Id,
                Active = needUpdate.Active,
                CanToRemove = needUpdate.CanToRemove,
                Alias = needUpdate.Alias,
                Description = needUpdate.Description,
                Deliverables = needUpdate.Deliverables,
                Milestone = needUpdate.Milestone,
                Engineer = needUpdate.Engineer,

                CompleteDate = needUpdate.CompleteDate,
                Status = needUpdate.Status,
                StartDate = needUpdate.StartDate,
                DeadLineDate = needUpdate.DeadLineDate,
                Copmliexity = needUpdate.Copmliexity,
                Remarks = needUpdate.Remarks,

        };   

        return afterUpdateTask;
    }
    /// <summary>
    /// Return all Id of Tasks .
    /// </summary>
    /// <returns></returns>
    public IEnumerable<int> AllTaskSId()
    {
        var allId = from task in _dal.Task.ReadAll()
                    select task._id;
        return allId;
    }
    /// <summary>
    /// Crete and save in data base new dependency from PL . 
    /// </summary>
    /// <param name="dependencyTask"></param>
    /// <param name="dependencyOnTask"></param>
    public void AddDependency(int dependencyTask , int dependencyOnTask)
    {
      
            // If the dependencyTask and dependencyOnTask are the same task.
            if (dependencyTask == dependencyOnTask)
            {
                throw new BlCannotAddDependencyException("The dependency and dependent task are the same task");
            }

            // If dependency on task is depends on dependent task .
            if (_dal.Dependency.Read(x => x._dependentTask == dependencyOnTask && x._dependsOnTask == dependencyTask) is not null)
            {
                throw new BlCannotAddDependencyException($"The task {dependencyOnTask} already depends on {dependencyTask} , two way dependency is not enabled .");
            }

            // If at least one task in the dependencyOnTask.Dependency list depends on dependencyTask, circular dependency is not enabled.
            // Get the dependendies list of dependencyOnTask if have .
            var dependenciesList = e_bl.Task.Read(dependencyOnTask)?.Dependencies;
            if (dependenciesList != null)
            {
                var dependentTask = dependenciesList.FirstOrDefault(task => _dal.Dependency.Read(x => x._dependentTask == task.Id && x._dependsOnTask == dependencyTask)is not null);
                if (dependentTask != null)
                {
                    throw new BlCannotAddDependencyException($"The task {dependencyOnTask} depends on task {dependentTask.Id} , and task {dependentTask.Id} already depends on task {dependencyTask} .\nCircular dependency is not enabled.");
                }

            }

        // Can add a new dependency .
        DO.Dependency newDependency = new(dependencyTask,dependencyOnTask);
        try
        {
            _dal.Dependency.Create(newDependency);
        }
        catch (DO.DalDoesExistException ex)
        {
            throw new BlDependencyAlreadyExistException($"The dependency {dependencyTask} -> {dependencyOnTask} already exist", ex);
        } 
    }

    public void DeleteDependency(int dependencyTask, int dependencyOnTask)
    {
        // Return the dependency by dependencyTask -> dependencyOnTask.
        DO.Dependency? newDependency = _dal.Dependency.Read(x => x._dependentTask == dependencyTask && x._dependsOnTask == dependencyOnTask);
        if(newDependency == null)
        {
            throw new BlDoesNotExistException($"The task {dependencyTask} does not depend in {dependencyOnTask}");
        }
        int idDependency = newDependency._id;

        try
        {
            _dal.Dependency.Delete(idDependency);
        }
        catch (Exception ex) { } /// בלי נדר לתקן לחריגה מתאימה
    }

}


