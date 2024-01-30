

namespace BlApi;

public interface IEngineer
{
    int Create(BO.Engineer boEngineer); //Creates new Engineer object in BL
    BO.Engineer? Read(int id); //Reads Engineer object by its ID 
    /// <summary>
    /// Return item by every parameters that it got .
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// return bool and bring type T
    BO.Engineer? Read(Func<BO.Engineer, bool> filter); // Return item by every parameters that it got .
    IEnumerable<BO.Engineer?> ReadAll(Func<BO.Engineer, bool>? filter = null); // Pointer to func .
    void Update(BO.Engineer item); //Updates Engineer object
    void Delete(int id); //Deletes an Engineer object by its Id
}
