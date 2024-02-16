
namespace BlImplementation;
using BlApi;
internal class ProjectImplementation : IProject
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    public DateTime ReturnStartProjectDate()
    {
        return _dal.Project.ReturnStartProjectDate();
    }

    public string ReturnStatusProject()
    {
        return _dal.Project.ReturnStatusProject();
    }

    public void SaveChangeOfStatus(string status)
    {
        _dal.Project.SaveChangeOfStatus(status);
    }

    public void SaveStartProjectDate(DateTime startProject)
    {
       _dal.Project.SaveStartProjectDate(startProject);
    }

    public void InitializeDB() => DalTest.Initialization.Do();
}
