

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
    void Delete(int id); //Deletes an object by its Id
}
