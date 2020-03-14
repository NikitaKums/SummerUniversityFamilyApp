using System;

namespace Contracts.DAL.Base.Helpers
{
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Get method for repo creation
        /// </summary>
        /// <typeparam name="TRepo">Repo type to create</typeparam>
        /// <returns></returns>
        Func<IDataContext, object> GetRepositoryFactory<TRepo>();
        
        /// <summary>
        /// Get method for repo creation based on entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type for base repository</typeparam>
        /// <returns></returns>
        Func<IDataContext, object> GetRepositoryFactoryForEntity<TEntity>() 
            where TEntity: class, IBaseEntity, new();

    }
}