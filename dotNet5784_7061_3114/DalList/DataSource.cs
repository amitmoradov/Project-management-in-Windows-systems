

namespace Dal;

internal static class DataSource
{
    internal static class Config
    {
        internal const int startTeskId = 1;
        private static int nextTeskId = startTeskId;
        internal static int NextTeskId { get =>  nextTeskId++; }

        internal const int startDependencyId = 1;
        private static int nextDependencyId = startDependencyId;
        internal static int NextDependencyId { get => nextDependencyId++; }

        internal static DateTime startProject = new DateTime(2024-01-01);
        internal static string status = "planning";
    }
    internal static List<DO.Dependency> Dependencies { get; } = new();
    internal static List<DO.Engineer> Engineers { get; } = new();
    internal static List<DO.Task> Tasks { get; } = new();
}
