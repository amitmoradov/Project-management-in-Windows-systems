
namespace BlImplementation;
using BlApi;
using BO;

internal class BL : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();

    public ProjectScheduled StatusProject => new ();
}
