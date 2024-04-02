using DO; // Importing the namespace DO
using System.Xml.Linq; // Importing the namespace for XML manipulation

namespace DalApi
{
    /// <summary>
    /// Represents a generic interface for CRUD operations on objects of type T.
    /// </summary>
    /// <typeparam name="T">The type of objects to perform CRUD operations on.</typeparam>
    public interface ICrud<T> where T : class
    {
        // Creates a new entity object in the DAL
        int Create(T item);

        // Reads an entity object by its ID
        T? Read(int id);

        /// <summary>
        /// Returns an item based on the provided filter.
        /// </summary>
        /// <param name="filter">The filter condition to apply.</param>
        /// <returns>The item matching the filter condition.</returns>
        T? Read(Func<T, bool> filter);

        // Reads all items based on an optional filter
        IEnumerable<T?> ReadAll(Func<T, bool>? filter = null);

        // Updates an entity object
        void Update(T item);

        // Deletes an object by its ID
        void Delete(int id);

        /// <summary>
        /// Resets the database.
        /// </summary>
        void reset();
    }
}
