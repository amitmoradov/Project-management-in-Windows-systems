

namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        // Initial value for task ID
        internal const int startTaskId = 1;

        // Static variable to track the next task ID
        private static int nextTaskId = startTaskId;

        // Property to get and increment the next task ID
        internal static int NextTaskId { get => nextTaskId++; set { nextTaskId = value; } }

        // Initial value for dependency ID
        internal const int startDependencyId = 1;

        // Static variable to track the next dependency ID
        private static int nextDependencyId = startDependencyId;

        // Property to get and increment the next dependency ID
        internal static int NextDependencyId { get => nextDependencyId++; set { nextDependencyId = value; } }

        //Default Start date for projects
        internal static DateTime startProject = new DateTime(2024, 01, 01);

        // Default status for projects
        internal static string status = "planning";

        // Variable to store the virtual time (clock) in the DAL
        internal static DateTime VirtualTime = DateTime.Now;
    }
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
}
