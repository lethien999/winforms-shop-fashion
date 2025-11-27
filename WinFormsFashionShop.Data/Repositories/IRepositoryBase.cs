using System.Collections.Generic;

namespace WinFormsFashionShop.Data.Repositories
{
    /// <summary>
    /// Basic CRUD contract for repositories.
    /// </summary>
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities of type T.
        /// </summary>
        /// <returns>Enumerable of T.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Retrieves an entity by identifier.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Entity instance or null if not found.</returns>
        T? GetById(int id);

        /// <summary>
        /// Inserts a new entity into the data store.
        /// </summary>
        /// <param name="entity">Entity to insert.</param>
        void Insert(T entity);

        /// <summary>
        /// Updates an existing entity in the data store.
        /// </summary>
        /// <param name="entity">Entity with updated values.</param>
        void Update(T entity);

        /// <summary>
        /// Deletes an entity by identifier from the data store.
        /// </summary>
        /// <param name="id">Identifier of the entity to delete.</param>
        void Delete(int id);
    }
}