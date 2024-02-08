namespace DalApi;

public interface IProject 
{
    void SaveStartProjectDate(DateTime startProject);
    void SaveChangeOfStatus(string status);
    /// <summary>
    /// Return start projectDate .
    /// </summary>
    /// <returns></returns>
    DateTime ReturnStartProjectDate();

    string ReturnStatusProject();
}