
namespace BlImplementation;
using BlApi;
using BO;
using System;

internal class BL : IBl
{
    public IEngineer Engineer => new EngineerImplementation();

    public ITask Task => new TaskImplementation();
    public IProject Project => new ProjectImplementation();

}
