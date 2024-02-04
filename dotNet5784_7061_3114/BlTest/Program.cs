
using BlApi;
using BO;
using DalApi;
using DalTest;
using DO;

internal class Program
{
    static readonly BlApi.IBl e_bl = BlApi.Factory.Get();
    static bool _exit = false;
    static ProjectScheduled status = ProjectScheduled.planning;

    static void Main(string[] args)
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
            DalTest.Initialization.Do();

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
                    case '1'://set tasks
                        if (status != ProjectScheduled.planning)
                        {
                            throw new BlAlreadyPalnedException("The status of project already in this level");
                        }
                        enterTask();
                        break;
                    case '2':
                        EngineerSubMenu("Engineer"); // Entity Engineer

                        break;
                    case '3':
                        TaskSubMenu("Task"); // Entity Task
                        break;
                    case '4':
                        if (status != ProjectScheduled.planning)
                        {
                            throw new BlAlreadyPalnedException("The status of project already in this level");
                        }
                        status = ProjectScheduled.ScheduleDetermination;
                        createDate();
                        break;
                    case '5':
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
                case '1':
                    // Perform Create operation                       
                    //Console.WriteLine($"Enter the {menuEntityName} ditals: id, cost, level, email, name");

                    BO.Engineer newEngineer = InputValueEngineer();
                    // Send the item to methods of create and insert to list of engineer
                    try
                    {
                        e_bl!.Engineer.Create(newEngineer);
                    }
                    // If the engineer is exist
                    catch (DalDoesExistException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case '2':
                    // Perform Read operation       
                    Console.Write($"Enter the {menuEntityName} id: ");
                    int searchId = int.Parse(Console.ReadLine()!);
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
                    // If the engineer is exist
                    catch (DalDoesNotExistException ex)
                    {
                        Console.WriteLine(ex.Message);
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
                    catch (DalDoesNotExistException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (DalCannotDeleted ex)
                    {
                        Console.WriteLine(ex.Message);
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
        Console.WriteLine($"Enter the Engineer ditals:  UPDATE - same id / CREATE - id, cost, level, email, name,Task(enter Id of task and Alias)");
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
        if (status != ProjectScheduled.planning)
        {
            int idOfTask = int.Parse(Console.ReadLine()!);
            string? alias = Console.ReadLine();
            BO.TaskInEngineer taskInEngineer = new BO.TaskInEngineer()
            {
                Id = idOfTask,
                Alias = alias,
            };

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

        //To save all parameters in item without parameter oftask
        BO.Engineer item = new()
        { 
            Id= id,
            Cost= cost,
            Level= level,
            CanToRemove = true,
            Task = null,
            Active = true,
            Email= email,
            Name= name,
        };

        return item;
    }






/*
    static BO.Task InputValueTask()
    {
        Console.WriteLine($"Enter the Task ditals: engineer id,  UPDATE - same id / CREATE - id, level(int), alias, description, remarks");
        int engineerId = int.Parse(Console.ReadLine()!);
        int id = int.Parse(Console.ReadLine()!);
        DO.EngineerExperience? taskLevel = (DO.EngineerExperience)int.Parse(Console.ReadLine()!);
        string? alias = Console.ReadLine();
        string? description = Console.ReadLine();
        string? remarks = Console.ReadLine();

        BO.Task task = new()
        {

        };
        return task;
    }


    static Dependency InputValueDependency()
    {
        Console.WriteLine($"Enter the Dependency ditals: DependentTask , DependsOnTas, UPDATE - same id / CREATE - id");
        int dependentTask = int.Parse(Console.ReadLine()!);
        int dependsOnTask = int.Parse(Console.ReadLine()!);
        int id = int.Parse(Console.ReadLine()!);
        Dependency item = new(dependentTask, dependsOnTask, id);
        return item;

    }
*/
    /// <summary>
    /// Main menu.
    /// </summary>
    static void DisplayMainMenu()
    {
        Console.WriteLine("Main Menu:");
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. set tasks");
        Console.WriteLine("2. Entity Engineer");
        Console.WriteLine("3. Entity Task");
        Console.WriteLine("4. Create a schedule/create Dates for Tasks");
        Console.WriteLine("5. Initialization");

        // Add more entities or options as needed
        Console.Write("Enter your choice: ");
    }
}