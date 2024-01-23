
namespace DO;
/// <summary>
/// Show the order between the tesks .
/// </summary>
/// <param name="Id">The id of dependency of the tesks .</param>
/// <param name="DependentTask">Id of the DependentTask .</param>
/// <param name="DependsOnTask">Id of the previous tesk .</param>
public record Dependency
(
    int _dependentTask, // מספר משימה 
    int _dependsOnTask, // תלויה במשימה
    int _id = 0, // מספר רץ
    bool _active = true,
    bool _canToRemove = true
)
{
    public Dependency(): this(0, 0) { }
}
