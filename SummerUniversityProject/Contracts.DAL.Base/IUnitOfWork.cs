using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.Base
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();

        IBaseRepositoryAsync<TEntity> BaseRepository<TEntity>()
            where TEntity : class, IBaseEntity, new();
    }
}