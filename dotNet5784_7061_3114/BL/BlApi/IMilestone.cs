
namespace BlApi;

public interface IMilestone
{
    void Create(BO.Milestone item); //Creates new entity object in DAL
    BO.Milestone? Read(int id); //Reads entity object by its ID 
    /// <summary>
    /// Return item by every parameters that it got .
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// return bool and bring type T
    void Update(BO.Milestone item); //Updates entity object
}
