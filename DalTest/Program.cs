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
                Console.WriteLine();
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
                    Console.WriteLine($"Enter the {menuEntityName} ditals: id, cost, level, email, name");

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
                    int searchId = int.Parse(Console.ReadLine()!);
                    // Search the engineer inside detebase and bring him
                    Engineer? engineer = e_dalEngineer?.Read(searchId);
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
                    List<DO.Engineer>? engineers = e_dalEngineer?.ReadAll();
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

                        Engineer? previousEngineer = e_dalEngineer?.Read(readId);
                        if (previousEngineer != null)
                        {
                            // Print the previous engineer .
                            Console.WriteLine(previousEngineer);
                        }
                        Console.WriteLine();

                        Engineer updateEngineer = InputValueEngineer();
                        // Send the item to methods of create and insert to list of engineer
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
                    int id = int.Parse(Console.ReadLine()!);
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

                case '1':
                    // Perform Create operation                       
                    Console.WriteLine($"Enter the {menuEntityName} ditals: engineer id, level, alias, description, remarks");

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
                    int searchId = int.Parse(Console.ReadLine()!);

                    // Search the task inside detebase and bring him
                    DO.Task? searchTask = e_dalTask?.Read(searchId);
                    // If is exist
                    if (searchTask != null)
                    {
                        Console.WriteLine("The Task is " + searchTask);
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
                        foreach (var e_task in tasks)
                        {
                            // Print the name of all task
                            Console.WriteLine(e_task);
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

                        DO.Task? previousTask = e_dalTask?.Read(readId);

                        if (previousTask != null)
                        {
                            // Print the previous Task .
                            Console.WriteLine(previousTask);
                        }
                        
                        DO.Task updateTask = InputValueTask();
                        // Send the item to methods of create and insert to list of task
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
                    int id = int.Parse(Console.ReadLine()!);
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



    static void DependencySubMenu(string menuEntityName)
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

                case '1':
                    // Perform Create operation                       
                    Console.WriteLine($"Enter the {menuEntityName} ditals: DependentTask , DependsOnTas");

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
                    int searchId = int.Parse(Console.ReadLine()!);

                    // Search the dependency inside detebase and bring him
                    Dependency? searchDependency = e_dalDependency?.Read(searchId);

                    // If is exist
                    if (searchDependency != null)
                    {
                        Console.WriteLine($"The Dependency is :{ searchDependency}");
                        break;
                    }
                    Console.WriteLine("The Dependency is not exist");
                    break;


                case '3':
                    // Perform ReadAll operation
                    Console.WriteLine("Perform ReadAll operation for Entity " + menuEntityName);

                    List<DO.Dependency>? dependency = e_dalDependency?.ReadAll();
                    if (dependency != null)
                    {
                        foreach (var e_dependency in dependency)
                        {
                            // Print the name of all dependency .
                            Console.WriteLine(e_dependency);
                        }
                        break;
                    }
                    Console.WriteLine("The DataBase is empty");
                    break;

                case '4':
                    // Perform Update operation                   
                    try
                    {
                        Console.WriteLine($"Enter the {menuEntityName} id:");
                        int dependencyId = int.Parse(Console.ReadLine()!);
                        Dependency? previousDependency = e_dalDependency?.Read(dependencyId);
                        if (previousDependency != null)
                        {
                            Console.WriteLine(previousDependency);
                        }

                        // Send the item to methods of create and insert to list of dependcy
                        Dependency updateDependecy = InputValueDependency();

                        e_dalDependency?.Update(updateDependecy);
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
                    int id = int.Parse(Console.ReadLine()!);
                    try
                    {
                        e_dalDependency?.Delete(id);
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
        int id = int.Parse(Console.ReadLine()!);
        double cost = double.Parse(Console.ReadLine()!);
        DO.EngineerExperience level = (DO.EngineerExperience)int.Parse(Console.ReadLine()!);
        string? email = Console.ReadLine();
        string name = Console.ReadLine()!;

        //To save all paramers in item
        Engineer item = new(id, cost, level, email, name);
        return item;
    }


    static DO.Task InputValueTask()
    {
        int engineerId = int.Parse(Console.ReadLine()!);
        DO.EngineerExperience? taskLevel = (DO.EngineerExperience)int.Parse(Console.ReadLine()!);
        string? alias = Console.ReadLine();
        string? description = Console.ReadLine();
        string? remarks = Console.ReadLine();

        DO.Task task = new(null, null, taskLevel, null, null, null, null, alias, description, null, remarks,0,engineerId);
        return task;
    }


    static Dependency InputValueDependency()
    {
        int dependentTask = int.Parse(Console.ReadLine()!);
        int dependsOnTask = int.Parse(Console.ReadLine()!);
        Dependency item = new(dependentTask, dependsOnTask);
        return item;

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
