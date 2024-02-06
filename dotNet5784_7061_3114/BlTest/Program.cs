
using BlApi;
using BO;
using DalApi;
using DalTest;
using DO;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    static bool _exit = false;
    static ProjectScheduled statusProject = e_bl.StatusProject;

    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
        {
            DalTest.Initialization.Do();
        }

        try
        {


            while (!_exit)
            {
                //Console.WriteLine();
                DisplayMainMenu();
                // Input char and convert to char type .
                char? userInput = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (userInput)
                {
                    case '0':
                        _exit = true;
                        break;
                    case '1':
                        try
                        {
                            EngineerSubMenu("Engineer"); // Entity Engineer
                        }
                        catch (BlAlreadyPalnedException ex)
                        {
                            Console.WriteLine("Exception message: " + ex.Message);
                            Console.WriteLine("Exception type: " + ex.GetType().FullName);
                        }

                        break;
                    case '2':
                        try
                        {
                            TaskSubMenu("Task"); // Entity Task
                        }
                        catch (BlAlreadyPalnedException ex)
                        {
                            Console.WriteLine("Exception message: " + ex.Message);
                            Console.WriteLine("Exception type: " + ex.GetType().FullName);
                        }
                        break;
                    case '3':// Create a schedule/create Dates for Tasks
                        if (statusProject != ProjectScheduled.planning && statusProject != ProjectScheduled.scheduleWasPalnned)
                        {
                            throw new BlAlreadyPalnedException("The schedule has already been initialized");
                        }
                        statusProject = ProjectScheduled.ScheduleDetermination;

                        DateTime startProject = ProjectStartDate();
                        ScheduledDateForTasks();

                        break;
                    case '4':
                        // If we want to initialization the data base .
                        Console.Write("Would you like to create Initial data? (Y/N)"); //stage 3
                        ans = Console.ReadLine() ?? throw new FormatException("Wrong input"); //stage 3

                        if (ans == "Y")
                        {
                            //Initialization.Do(e_dal); // stage 2
                            DalTest.Initialization.Do(); // stage 4
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }

        }
        catch (Exception ex)
        {
            // Handle any exceptions that might occur during initialization or subsequent code execution
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

    }





    static void EngineerSubMenu(string menuEntityName)
    {
        bool returnMainMenu = false;
        while (!returnMainMenu)
        {
            Console.WriteLine();
            // Call to the sub menu of the entity .
            DisplaySubEntityMenu(menuEntityName);

            // Input char and convert to char type .
            char? userInput = Console.ReadKey().KeyChar;
            Console.WriteLine();
            switch (userInput)
            {
                case '0':
                    // We go out from all menus , and finish the running .
                    _exit = true;
                    returnMainMenu = true;
                    break;
                case '1'://Create
                         // Perform Create operation                       
                         //Console.WriteLine($"Enter the {menuEntityName} ditals: id, cost, level, email, name");
                    try
                    {
                        BO.Engineer newEngineer = InputValueEngineer();
                        // Send the item to methods of create and insert to list of engineer                    
                        e_bl!.Engineer.Create(newEngineer);
                    }
                    // If the engineer is exist
                    catch (BlAlreadyExistsException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }             
                    break;

                case '2':// Read
                    // Perform Read operation       
                    Console.Write($"Enter the {menuEntityName} id: ");
                    int searchId = int.Parse(Console.ReadLine()!);
                    try
                    {
                        // Search the engineer inside detebase and bring him
                        BO.Engineer? engineer = e_bl!.Engineer.Read(searchId);
                        // If is exist
                        if (engineer != null)
                        {
                            Console.WriteLine("The engineer is ");
                            Console.WriteLine(engineer);
                            break;
                        }
                        Console.WriteLine("The engineer is not exist");
                    }
                    catch(BlReadNotFoundException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    break;

                case '3':
                    // Perform ReadAll operation
                    Console.WriteLine("Perform ReadAll operation for Entity " + menuEntityName);
                    IEnumerable<BO.Engineer?> engineers = e_bl!.Engineer.ReadAll();
                    if (engineers != null)
                    {
                        foreach (var i_engineer in engineers)
                        {
                            // Print the name of all engineer
                            Console.WriteLine(i_engineer);
                        }
                        break;
                    }
                    Console.WriteLine("The DataBase is empty");
                    break;

                case '4':
                    // Perform Update operation

                    try
                    {
                        Console.WriteLine($"Enter the {menuEntityName} id: ");
                        int readId = int.Parse(Console.ReadLine()!);

                        BO.Engineer? previousEngineer = e_bl!.Engineer.Read(readId);
                        if (previousEngineer != null)
                        {
                            // Print the previous engineer .
                            Console.WriteLine(previousEngineer);
                        }
                        Console.WriteLine();

                        BO.Engineer updateEngineer = InputValueEngineer();
                        // Send the item to methods of create and insert to list of engineer
                        e_bl!.Engineer.Update(updateEngineer);
                    }
                    
                    catch (BlIncorrectDatailException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    catch (BlReadNotFoundException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }

                    break;

                case '5':
                    // Perform Delete operation
                    Console.WriteLine("Enter Id to remove frome the DataBase ");
                    int id = int.Parse(Console.ReadLine()!);
                    try
                    {
                        e_bl!.Engineer.Delete(id);
                    }
                    // If the engineer is exist
                    catch (BlEntityCanNotRemoveException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    catch (DalCannotDeleted ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    // when delete funcion read to read funcion and the engineer is not found.
                    catch(BlReadNotFoundException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    catch(BlDoesNotExistException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    catch(BlCannotDeletedException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    break;

                case '6':
                    returnMainMenu = true;
                    break;
                // Add more cases for additional operations if needed
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }






    static void TaskSubMenu(string menuEntityName)
    {
        bool returnMainMenu = false;
        while (!returnMainMenu)
        {
            Console.WriteLine();
            // Call to the sub menu of the entity .
            DisplaySubEntityMenu(menuEntityName);
            Console.WriteLine();
            // Input char and convert to char type .
            char? userInput = Console.ReadKey().KeyChar;

            Console.WriteLine();

            switch (userInput)
            {
                case '0':
                    // We go out from all menus , and finish the running .
                    _exit = true;
                    returnMainMenu = true;
                    break;

                case '1':// Perform Create operation 

                    //Console.WriteLine($"Enter the {menuEntityName} ditals: engineer id, level, alias, description, remarks");
                    if (statusProject != ProjectScheduled.planning)
                    {
                        throw new BlAlreadyPalnedException("The schedule has already been initialized");
                    }
                    BO.Task newTask = InputValueTaskForPlanning();
                    // Send the item to methods of create and insert to list of task
                    try
                    {
                        e_bl!.Task.Create(newTask);
                    }
                    // If the engineer is not exist
                    catch (DalDoesExistException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }

                    break;

                case '2':
                    // Perform Read operation       
                    Console.Write($"Enter the {menuEntityName} id: ");
                    int searchId = int.Parse(Console.ReadLine()!);

                    // Search the task inside detebase and bring him
                    try
                    {
                        BO.Task? searchTask = e_bl!.Task.Read(searchId);
                        // If is exist
                        if (searchTask != null)
                        {
                            Console.WriteLine("The Task is " + searchTask);
                            break;
                        }
                        Console.WriteLine("The Task is not exist");                       
                    }
                    catch (BlReadNotFoundException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    break;

                case '3':
                    // Perform ReadAll operation
                    Console.WriteLine("Perform ReadAll operation for Entity " + menuEntityName);
                    try
                    {
                        IEnumerable<BO.TaskInList?> tasks = e_bl!.Task.ReadAll();

                        if (tasks != null)
                        {
                            foreach (var e_task in tasks)
                            {
                                // Print the name of all task
                                Console.WriteLine(e_task);
                            }
                            break;
                        }
                        Console.WriteLine("The DataBase is empty");
                    }
                    catch(BlReadNotFoundException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    break;

                case '4':  // Perform Update operation

                    try
                    {
                        Console.WriteLine($"Enter the {menuEntityName} id: ");
                        int readId = int.Parse(Console.ReadLine()!);

                        BO.Task? previousTask = e_bl!.Task.Read(readId);

                        if (previousTask != null)
                        {
                            // Print the previous Task .
                            Console.WriteLine(previousTask);
                        }
                        Console.WriteLine();
                        BO.Task updateTask = new();
                        if (statusProject == ProjectScheduled.planning)
                        {
                            updateTask = InputValueTaskForPlanning();                                                     
                        }

                        if (statusProject == ProjectScheduled.scheduleWasPalnned)
                        {
                            updateTask = InputValueTaskForPlanned();                       
                            if(previousTask != null)
                            {
                                // If he try to change the StartDate .
                                if (previousTask.StartDate != null && (updateTask.StartDate != previousTask.StartDate))
                                {
                                    throw new BlStartDateAlreadyExistsException("The start date value already exists");
                                }
                                // If he try to change the CompleteDate .
                                if (previousTask.CompleteDate != null && (updateTask.CompleteDate != previousTask.CompleteDate))
                                {
                                    throw new BlCompleteDateAlreadyExistsException("The complete date value already exists");
                                }
                            }
                        }

                        // Send the item to methods of create and insert to list of task
                        e_bl!.Task.Update(updateTask);
                      

                    }
                    // If the engineer is exist
                    catch (BlReadNotFoundException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    catch(BlDoesNotExistException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    break;

                case '5': // Perform Delete operation

                    //Tasks cannot be deleted after the project schedule has been created
                    if (statusProject == ProjectScheduled.scheduleWasPalnned)
                    {
                        throw new BlAlreadyPalnedException("The schedule has already been initialized");
                    }

                    Console.WriteLine("Enter Id to remove frome the DataBase ");
                    int id = int.Parse(Console.ReadLine()!);
                    try
                    {
                        e_bl!.Task.Delete(id);
                    }
                    
                    catch (BlReadNotFoundException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }

                    catch (BlEntityCanNotRemoveException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    catch(BlDoesNotExistException ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    catch(DalCannotDeleted ex)
                    {
                        Console.WriteLine("Exception message: " + ex.Message);
                        Console.WriteLine("Exception type: " + ex.GetType().FullName);
                    }
                    break;

                case '6':
                    returnMainMenu = true;
                    break;

                // Add more cases for additional operations if needed
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }




        }


    }















        static void DisplaySubEntityMenu(string entityName)
        {
            Console.WriteLine("Entity " + entityName + " Menu:");
            Console.WriteLine("0. Exit Main Menu");
            Console.WriteLine("1. Create");
            Console.WriteLine("2. Read");
            Console.WriteLine("3. ReadAll");
            Console.WriteLine("4. Update");
            Console.WriteLine("5. Delete");
            Console.WriteLine("6. Back to Main Menu");
            // Add more options specific to the entity if needed
            Console.Write("Enter your choice: ");
        }

        static BO.Engineer InputValueEngineer()
        {
            Console.WriteLine($"Enter the Engineer ditals:  UPDATE - same id / CREATE - id, cost, level, email, name");
            Console.WriteLine("For Level:" +
                " 0 - Beginner," +
                " 1 - AdvancedBeginner," +
                " 2 - Intermediate," +
                " 3 - Advanced," +
                " 4 - Expert");
            int id = int.Parse(Console.ReadLine()!);
            double cost = double.Parse(Console.ReadLine()!);
            DO.EngineerExperience level = (DO.EngineerExperience)int.Parse(Console.ReadLine()!);
            string? email = Console.ReadLine();
            string name = Console.ReadLine()!;
            BO.TaskInEngineer taskInEngineer = new BO.TaskInEngineer()
            {
                Id = 0,
                Alias = "",
            };

            if (statusProject == ProjectScheduled.scheduleWasPalnned)
            {
                Console.WriteLine("Enter also ,Task(enter Id of task and Alias)");
                int idOfTask = int.Parse(Console.ReadLine()!);
                string? alias = Console.ReadLine();
                taskInEngineer.Alias = alias;
                taskInEngineer.Id = idOfTask;
            }

            //To save all parameters in engineer 
            BO.Engineer engineer = new()
            {
                Id = id,
                Cost = cost,
                Level = level,
                CanToRemove = true,
                Task = taskInEngineer,
                Active = true,
                Email = email,
                Name = name,
            };
            return engineer;
        }

        /// <summary>
        /// Date of start the project
        /// </summary>
        /// <returns></returns>
        static DateTime ProjectStartDate()
        {
            DateTime dateTime = new DateTime(2024, 2, 5);
            return dateTime;
        }


        /// <summary>
        /// Initializes all tasks in the scheduledDate field.
        /// </summary>
        static void ScheduledDateForTasks()
        {
            IEnumerable<BO.Task> boTasks = BringAllFieldTaskList();

            foreach (BO.Task task in boTasks)
            {
                //All the tasks that task depends on
                IEnumerable<BO.Task?> taskDependOn = e_bl.Task.BringTasksDependsOn(task);

                //If this task has no previous tasks, we will return the scheduled project start date
                if (taskDependOn == null)
                {
                    task.ScheduledDate = ProjectStartDate();
                }
                else
                {
                    //If all previous tasks have the field scheduled
                    // - We will return the latest estimated finish date from all previous tasks            
                    if (ScheduledDateHasValue(taskDependOn))
                    {
                        task.ScheduledDate = GetMaxScheduledDate(taskDependOn);
                    }
                    else
                    {
                        throw new BO.BlNullPropertyException("$You did not add value for : ScheduledDate");
                    }

                }
                task.Status = Status.Scheduled;

            }

        }

        /// <summary>
        /// Return max Scheduled Date
        /// </summary>
        /// <param name="taskDependOn"></param>
        /// <returns></returns>
        static DateTime GetMaxScheduledDate(IEnumerable<BO.Task?> taskDependOn)
        {
            //We just gave a date to the max variable.
            DateTime maxDate = new DateTime(1948, 5, 11);
            foreach (var task in taskDependOn)
            {
                //Checks whether task is bigger in terms of years.
                if (task.ScheduledDate > maxDate)
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
        static bool ScheduledDateHasValue(IEnumerable<BO.Task?> taskDependOn)
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
        static IEnumerable<BO.Task> BringAllFieldTaskList()
        {
            var tasksInList = e_bl.Task.ReadAll();
            var allFieldTaskList = from task in tasksInList
                                   let fullTask = e_bl.Task.Read(task.Id)
                                   select fullTask;

            return allFieldTaskList;
        }

        static BO.Task InputValueTaskForPlanning()
        {
            Console.WriteLine("Enter task details for creation:");
            Console.WriteLine("ID: ");
            int id = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Level (0-3): ");
            DO.EngineerExperience level = (DO.EngineerExperience)int.Parse(Console.ReadLine()!);
            Console.WriteLine("Alias: ");
            string alias = Console.ReadLine();
            Console.WriteLine("Description: ");
            string description = Console.ReadLine();
            Console.WriteLine("Remarks: ");
            string remarks = Console.ReadLine();

            DateTime createDate = DateTime.Now;

            List<TaskInList> dependencies = new List<TaskInList>();
            if (statusProject == ProjectScheduled.planning)
            {
                string input;
                do
                {
                    Console.WriteLine("Enter Dependency details (ID, Description, Alias): ");
                    TaskInList dependency = new TaskInList()
                    {
                        Id = int.Parse(Console.ReadLine()!),
                        Description = Console.ReadLine(),
                        Alias = Console.ReadLine()
                    };
                    dependencies.Add(dependency);
                    Console.WriteLine("To add another dependency, type 'y'. Otherwise, press any other key.");
                    input = Console.ReadLine();
                } while (input.ToLower() == "y");

                Console.WriteLine("Enter required effort time (e.g., 03:30:00): ");              
            }
             TimeSpan requiredEffortTime = TimeSpan.Parse(Console.ReadLine()!);
        BO.Task task = new BO.Task()
            {
                Id = id,
                CreatedAtDate = createDate,
                Alias = alias,
                Description = description,
                StartDate = null,
                Remarks = remarks,
                Dependencies = dependencies,
                Copmliexity = level,
                CanToRemove = true,
                Active = true,
                RequiredEffortTime = requiredEffortTime,               
                Deliverables = null,
                Milestone = null,
                Engineer = null,
                DeadLineDate = null,
                Status = Status.Unscheduled,
                CompleteDate = null,
                ScheduledDate = null,
            };

            return task;
        }

        static BO.Task InputValueTaskForPlanned()
        {
            Console.WriteLine("Enter task details for update:");
            Console.WriteLine("ID: ");
            int id = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Level (0-3): ");
            DO.EngineerExperience level = (DO.EngineerExperience)int.Parse(Console.ReadLine()!);
            Console.WriteLine("Alias: ");
            string alias = Console.ReadLine();
            Console.WriteLine("Description: ");
            string description = Console.ReadLine();
            Console.WriteLine("Remarks: ");
            string remarks = Console.ReadLine();

            EngineerInTask assignedEngineer = new EngineerInTask();
            Console.WriteLine("Enter assigned engineer details (ID, Name): ");
            assignedEngineer.Id = int.Parse(Console.ReadLine()!);
            assignedEngineer.Name = Console.ReadLine();

            Console.WriteLine("Enter start date (e.g., 2024-02-03): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter completion date (e.g., 2024-02-03): ");
            DateTime completionDate = DateTime.Parse(Console.ReadLine()!);

            Console.WriteLine("Enter task status (0-3): ");
            Status status = (Status)int.Parse(Console.ReadLine()!);

            BO.Task task = new BO.Task()
            {
                Id = id,
                Alias = alias,
                Description = description,
                CreatedAtDate = null,
                Remarks = remarks,
                Engineer = assignedEngineer,
                Copmliexity = level,
                CanToRemove = true,
                Active = true,
                DeadLineDate = null,
                RequiredEffortTime = null,
                Deliverables = null,
                ScheduledDate = startDate,
                Milestone = null,
                Status = status,
                CompleteDate = completionDate
            };

            return task;
        }



    /// <summary>
    /// Main menu.
    /// </summary>
    static void DisplayMainMenu()
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Entity Engineer");
            Console.WriteLine("2. Entity Task");
            Console.WriteLine("3. Create a schedule/create Dates for Tasks");
            Console.WriteLine("4. Initialization");

            // Add more entities or options as needed
            Console.Write("Enter your choice: ");
        }
    
}