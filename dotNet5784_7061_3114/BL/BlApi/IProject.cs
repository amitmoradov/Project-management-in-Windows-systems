
namespace BlApi;

/// <summary>
/// The IProject interface communicates between the BL layer 
/// and the DAL layer and makes it possible to save in files or in implementation through lists the date of the project's creation
/// and its acceptance and the project's status and its acceptance
/// </summary>
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

    public void InitializeDB();
}
