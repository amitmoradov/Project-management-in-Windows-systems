
using DO;

namespace DalApi;
public interface ICrud<T> where T : class
{
    int Create(T item); //Creates new entity object in DAL
    T? Read(int id); //Reads entity object by its ID 
    /// <summary>
    /// Return item by every parameters that it got .
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    T? Read(Func<T, bool> filter); // Return item by every parameters that it got .
    IEnumerable<T?> ReadAll(Func<T , bool> ? filter = null); // Pointer to func .
    void Update(T item); //Updates entity object
    void Delete(int id); //Deletes an object by its Id



}

