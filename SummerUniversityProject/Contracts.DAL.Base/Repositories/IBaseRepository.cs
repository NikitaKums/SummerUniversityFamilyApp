using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepositoryAsync<TEntity> : IBaseRepositoryAsync<TEntity, int>
        where TEntity : class, IBaseEntity<int>, new()
    {
    }
    
    public interface IBaseRepositoryAsync<TEntity, TKey>
        where TKey : struct, IComparable
    {
        Task<IEnumerable<TEntity>> AllAsync();
        Task<TEntity> FindAsync(params object[] id);
        Task AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(params object[] id);
    }
}