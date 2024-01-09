using Dal;
using DalApi;
using DO;

namespace DalTest;

internal class Program
{

    private static IDependency? e_dalDependency = new DependencyImplementation();
    private static ITask? e_dalTask = new TaskImplementation();
    private static IEngineer? e_dalEngineer = new EngineerImplementation();

    // Global to go out from all menus .
    static bool _exit = false;
    static void Main(string[] args)
    {
        try
        {
            Initialization.Do(e_dalDependency, e_dalTask, e_dalEngineer);


            while (!_exit)
            {

                DisplayMainMenu();
                // Input char and convert to char type .
                char? userInput = Console.ReadKey().KeyChar;

                switch (userInput)
                {
                    case '0':
                        _exit = true;
                        break;
                    case '1':
                        EngineerSubMenu("Engineer"); // Entity 1

                        break;
                    case '2':
                        TaskSubMenu("Task"); // Entity 2
                        break;
                    case '3':
                        DependencySubMenu("Dependency"); // Entity 3
                        break;
                    // Add more cases for other entities if needed
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
            // Call to the sub menu of the entity .
            DisplaySubEntityMenu(menuEntityName);

            // Input char and convert to char type .
            char? userInput = Console.ReadKey().KeyChar;

            switch (userInput)
            {
                case '0':
                    // We go out from all menus , and finish the running .
                    _exit = true;
                    returnMainMenu = true;
                    break;
                case '1':
                    // Perform Create operation                       
                    Console.Write($"Enter the {menuEntityName} ditals: id, cost, level, email, name");

                    Engineer newEngineer = InputValueEngineer();
                    // Send the item to methods of create and insert to list of engineer
                    try
                    {
                        e_dalEngineer?.Create(newEngineer);
                    }
                    // If the engineer is exist
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case '2':
                    // Perform Read operation       
                    Console.Write($"Enter the {menuEntityName} id: ");
                    int searchId = int.Parse(Console.ReadLine());
                    // Search the engineer inside detebase and bring him
                    Engineer? engineer = e_dalEngineer?.Read(searchId);
                    // If is exist
                    if (engineer != null)
                    {
                        Console.WriteLine("The engineer is " + engineer.Name);
                        break;
                    }
                    Console.WriteLine("The engineer is not exist");
                    break;

                case '3':
                    // Perform ReadAll operation
                    Console.WriteLine("Perform ReadAll operation for Entity " + menuEntityName);
                    List<DO.Engineer>? engineers = e_dalEngineer?.ReadAll();
                    if (engineers != null)
                    {
                        foreach (var i_engineer in engineers)
                        {
                            // Print the name of all engineer
                            Console.Write(i_engineer.Name + ' ');
                        }
                        Console.WriteLine();
                        break;
                    }
                    Console.WriteLine("The DataBase is empty");
                    break;

                case '4':
                    // Perform Update operation
                    Engineer updateEngineer = InputValueEngineer();
                    // Send the item to methods of create and insert to list of engineer
                    try
                    {
                        e_dalEngineer?.Update(updateEngineer);
                    }
                    // If the engineer is exist
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case '5':
                    // Perform Delete operation
                    Console.WriteLine("Enter Id to remove frome the DataBase ");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        e_dalEngineer?.Delete(id);
                    }
                    // If the engineer is exist
                    catch (Exception ex)
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
    /// <summary>
    /// Input the task values for case (create, uptade)
    /// </summary>
    /// <summary>
    /// Sub Menu for each entity .
    /// </summary>
    /// <param name="entityName"></param>

    static void TaskSubMenu(string menuEntityName)
    {
        bool returnMainMenu = false;
        while (!returnMainMenu)
        {
            // Call to the sub menu of the entity .
            DisplaySubEntityMenu(menuEntityName);

            // Input char and convert to char type .
            char? userInput = Console.ReadKey().KeyChar;

            switch (userInput)
            {
                case '0':
                    // We go out from all menus , and finish the running .
                    _exit = true;
                    returnMainMenu = true;
                    break;

                case '1':
                    // Perform Create operation                       
                    Console.Write($"Enter the {menuEntityName} ditals: engineer id, level, alias, description, remarks");

                    DO.Task newTask = InputValueTask();
                    // Send the item to methods of create and insert to list of task
                    try
                    {
                        e_dalTask?.Create(newTask);
                    }
                    // If the engineer is exist
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case '2':
                    // Perform Read operation       
                    Console.Write($"Enter the {menuEntityName} id: ");
                    int searchId = int.Parse(Console.ReadLine());

                    // Search the task inside detebase and bring him
                    DO.Task? searchTask = e_dalTask?.Read(searchId);
                    // If is exist
                    if (searchTask != null)
                    {
                        Console.WriteLine("The Task is " + searchTask.Id);
                        break;
                    }
                    Console.WriteLine("The Task is not exist");
                    break;

                case '3':
                    // Perform ReadAll operation
                    Console.WriteLine("Perform ReadAll operation for Entity " + menuEntityName);
                    List<DO.Task>? tasks = e_dalTask?.ReadAll();
                    if (tasks != null)
                    {
                        Console.WriteLine(tasks);
                        //foreach (var i_task in tasks)
                        //{
                        //    // Print the name of all task
                        //    Console.Write(i_task.);
                        //}
                        break;
                    }
                    Console.WriteLine("The DataBase is empty");
                    break;

                case '4':
                    // Perform Update operation
                    DO.Task updateTask = InputValueTask();
                    // Send the item to methods of create and insert to list of task
                    try
                    {
                        e_dalTask?.Update(updateTask);
                    }
                    // If the engineer is exist
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case '5':
                    // Perform Delete operation
                    Console.WriteLine("Enter Id to remove frome the DataBase ");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        e_dalTask?.Delete(id);
                    }
                    // If the engineer is exist
                    catch (Exception ex)
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



    static void DependncySubMenu(string menuEntityName)
    {
        bool returnMainMenu = false;
        while (!returnMainMenu)
        {
            // Call to the sub menu of the entity .
            DisplaySubEntityMenu(menuEntityName);

            // Input char and convert to char type .
            char? userInput = Console.ReadKey().KeyChar;

            switch (userInput)
            {
                case '0':
                    // We go out from all menus , and finish the running .
                    _exit = true;
                    returnMainMenu = true;
                    break;

                case '1':
                    // Perform Create operation                       
                    Console.Write($"Enter the {menuEntityName} ditals: engineer id, level, alias, description, remarks");

                    Dependency newDependency = InputValueDependency();
                    // Send the item to methods of create and insert to list of task
                    try
                    {
                        e_dalDependency?.Create(newDependency);
                    }
                    // If the engineer is exist
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;

                case '2':
                    // Perform Read operation       
                    Console.Write($"Enter the {menuEntityName} id: ");
                    int searchId = int.Parse(Console.ReadLine());

                    // Search the task inside detebase and bring him
                    DO.Task? searchTask = e_dalTask?.Read(searchId);
                    // If is exist
                    if (searchTask != null)
                    {
                        Console.WriteLine("The Task is " + searchTask.Id);
                        break;
                    }
                    Console.WriteLine("The Task is not exist");
                    break;


                case '3':
                    // Perform ReadAll operation
                    Console.WriteLine("Perform ReadAll operation for Entity " + menuEntityName);
                    List<DO.Task>? tasks = e_dalTask?.ReadAll();
                    if (tasks != null)
                    {
                        Console.WriteLine(tasks);
                        //foreach (var i_task in tasks)
                        //{
                        //    // Print the name of all task
                        //    Console.Write(i_task.);
                        //}
                        break;
                    }
                    Console.WriteLine("The DataBase is empty");
                    break;

                case '4':
                    // Perform Update operation
                    DO.Task updateTask = InputValueTask();
                    // Send the item to methods of create and insert to list of task
                    try
                    {
                        e_dalTask?.Update(updateTask);
                    }
                    // If the engineer is exist
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case '5':
                    // Perform Delete operation
                    Console.WriteLine("Enter Id to remove frome the DataBase ");
                    int id = int.Parse(Console.ReadLine());
                    try
                    {
                        e_dalTask?.Delete(id);
                    }
                    // If the engineer is exist
                    catch (Exception ex)
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

    static Engineer InputValueEngineer()
    {
        int id = int.Parse(Console.ReadLine());
        double cost = double.Parse(Console.ReadLine());
        string? email = Console.ReadLine();
        DO.EngineerExperience level = (DO.EngineerExperience)int.Parse(Console.ReadLine());
        string name = Console.ReadLine();

        //To save all paramers in item
        Engineer item = new(id, cost, level, email, name);
        return item;
    }


    static DO.Task InputValueTask()
    {
        int engineerId = int.Parse(Console.ReadLine());
        DO.EngineerExperience? taskLevel = (DO.EngineerExperience)int.Parse(Console.ReadLine());
        string? alias = Console.ReadLine();
        string? description = Console.ReadLine();
        string? remarks = Console.ReadLine();

        DO.Task task = new(null, null, taskLevel, null, null, null, null, alias, description, null, remarks,0,engineerId);
        return task;
    }


    static Dependency InputValueDependency()
    {
        /*
            int DependentTask, // מספר משימה 
    int DependsOnTask, // תלויה במשימה
    int Id = 0 , // מספר רץ
        */
        return
    }

    /// <summary>
    /// Main menu .
    /// </summary>
    static void DisplayMainMenu()
    {
        Console.WriteLine("Main Menu:");
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Entity Engineer");
        Console.WriteLine("2. Entity Task");
        Console.WriteLine("3. Entity Dependency");

        // Add more entities or options as needed
        Console.Write("Enter your choice: ");
    }

}
