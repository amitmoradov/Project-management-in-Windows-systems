
using DalApi;
using System;
namespace Dal;

internal class ProjectImplementation : IProject
{
    public DateTime ReturnStartProjectDate()
    {
        return DataSource.Config.startProject;
    }

    public string ReturnStatusProject()
    {
        return DataSource.Config.status;
    }

    public void SaveChangeOfStatus(string status)
    {
        DataSource.Config.status = status;
    }

    public void SaveStartProjectDate(DateTime startProject)
    {
        DataSource.Config.startProject = startProject;
    }

    public void SaveVirtualTimeInDal(DateTime virtualTime) 
    { DataSource.Config.VrtualTime = virtualTime; }

    public DateTime ReturnVirtualTimeInDal()
    {
        return DataSource.Config.VrtualTime;
    }
}
