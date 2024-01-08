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
                        EntitySubMenu("Engineer"); // Entity 1
                        break;
                    case '2':
                        EntitySubMenu("Task"); // Entity 2
                        break;
                    case '3':
                        EntitySubMenu("Dependency"); // Entity 3
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

    static void EntitySubMenu(string menuEntityName)
    {
        while (!_exit)
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
                    break;
                case '1':
                    // Perform Create operation
                    Console.WriteLine("Perform Create operation for Entity " + menuEntityName);
                    
                        break;
                case '2':
                    // Perform Read operation
                    Console.WriteLine("Perform Read operation for Entity " + menuEntityName);

                    break;
                case '3':
                    // Perform ReadAll operation
                    Console.WriteLine("Perform ReadAll operation for Entity " + menuEntityName);
                    break;
                case '4':
                    // Perform Update operation
                    Console.WriteLine("Perform Update operation for Entity " + menuEntityName);
                    break;
                case '5':
                    // Perform Delete operation
                    Console.WriteLine("Perform Delete operation for Entity " + menuEntityName);
                    break;
                // Add more cases for additional operations if needed
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
    /// <summary>
    /// Sub Menu for each entity .
    /// </summary>
    /// <param name="entityName"></param>
    static void DisplaySubEntityMenu(string entityName)
    {
        Console.WriteLine("Entity " + entityName + " Menu:");
        Console.WriteLine("0. Back to Main Menu");
        Console.WriteLine("1. Create");
        Console.WriteLine("2. Read");
        Console.WriteLine("3. ReadAll");
        Console.WriteLine("4. Update");
        Console.WriteLine("5. Delete");
        // Add more options specific to the entity if needed
        Console.Write("Enter your choice: ");
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
