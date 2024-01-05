
namespace DO;
/// <summary>
/// Show the order between the tesks .
/// </summary>
/// <param name="Id">The id of dependency of the tesks .</param>
/// <param name="DependentTask">Id of the DependentTask .</param>
/// <param name="DependsOnTask">Id of the previous tesk .</param>
public record Dependency
(
    int Id,
    int DependentTask,
    int DependsOnTask
)
{
    public Dependency(): this(0, 0, 0) { }
}
