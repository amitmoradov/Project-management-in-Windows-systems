

namespace BlApi;

public interface ITask
{
    void Create(BO.Task item); //Creates new entity object in DAL
    BO.Task? Read(int id); //Reads entity object by its ID 
    /// <summary>
    /// Return item by every parameters that it got .
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// return bool and bring type T
    BO.Task? Read(Func<BO.Task, bool> filter); // Return item by every parameters that it got .
    IEnumerable<BO.TaskInList?> ReadAll(Func<BO.Task, bool>? filter = null); // Pointer to func .
    void Update(BO.Task item); //Updates entity object

    //void Update(int idTask, DateTime date);
    void Delete(int id); //Deletes an object by its Id

    /// <summary>
    /// Brings back all the tasks I'm dependent on in full.
    /// </summary>
    /// <param name="boTask"></param>
    /// <returns></returns>
   // IEnumerable<BO.Task?> BringTasksDependsOn(BO.Task boTask);

    void ScheduleFieldsInitialization();
    IEnumerable<int> AllTaskSId();

    void AddDependency(int dependencyTask, int dependencyOnTask);

    void DeleteDependency(int dependencyTask, int dependencyOnTask);

    //void CreateStartDateProject(DateTime startDate);

    ///// <summary>
    ///// Change the status of project in data base
    ///// </summary>
    ///// <param name="status"></param>

    //void ChangeOfStatus(string status);

    //string ReturnStatusProject();

}
