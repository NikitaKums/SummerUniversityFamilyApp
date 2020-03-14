using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepositoryAsync<TEntity> where TEntity : class, IBaseEntity, new()
    {
        protected readonly DbContext RepositoryDbContext;
        protected readonly DbSet<TEntity> RepositoryDbSet;

        public BaseRepository(IDataContext dataContext)
        {
            RepositoryDbContext = (dataContext as DbContext) ?? throw new ArgumentNullException(nameof(dataContext));
            RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> All()
        {
            return RepositoryDbSet.ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await RepositoryDbSet.ToListAsync();
        }

        // id - primary key or composite primary key components in right order
        public virtual TEntity Find(params object[] id)
        {
            return RepositoryDbSet.Find(id);
        }

        public virtual async Task<TEntity> FindAsync(params object[] id)
        {
            return await RepositoryDbSet.FindAsync(id);
        }

        public virtual void Add(TEntity entity)
        {
            RepositoryDbSet.Add(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await RepositoryDbSet.AddAsync(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return RepositoryDbSet.Update(entity).Entity;
        }

        public virtual void Remove(TEntity entity)
        {
            RepositoryDbSet.Remove(entity);
        }

        public virtual void Remove(params object[] id)
        {
            RepositoryDbSet.Remove(Find(id));
        }
    }
}