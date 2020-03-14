using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.Base.Helpers
{
    public interface IRepositoryProvider
    {
        /// <summary>
        /// Return TRepository from cache or call factory to create it
        /// </summary>
        /// <typeparam name="TRepository">Repository you are looking for</typeparam>
        /// <returns></returns>
        TRepository GetRepository<TRepository>();

        /// <summary>
        /// Return IBaseRepositoryAsync from cache or call factory to create it
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns></returns>
        IBaseRepositoryAsync<TEntity> GetRepositoryForEntity<TEntity>()
            where TEntity : class, IBaseEntity, new();
    }
}