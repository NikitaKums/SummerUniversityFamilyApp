using System;
using System.Collections.Generic;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using DAL.Base.Repositories;

namespace DAL.Base.Helpers
{
    public class BaseRepositoryFactory : IRepositoryFactory
    {
        protected readonly Dictionary<Type, Func<IDataContext, object>> RepositoryCreationMethods;

        public BaseRepositoryFactory() : this(new Dictionary<Type, Func<IDataContext, object>>())
        {
        }
        
        public BaseRepositoryFactory(Dictionary<Type, Func<IDataContext, object>> repositoryCreationMethods)
        {
            RepositoryCreationMethods = repositoryCreationMethods;
        }
        
        public Func<IDataContext, object> GetRepositoryFactory<TRepo>()
        {
            RepositoryCreationMethods.TryGetValue(typeof(TRepo), out var repoCreationMethod);
            return repoCreationMethod;
        }

        public Func<IDataContext, object> GetRepositoryFactoryForEntity<TEntity>() where TEntity : class, IBaseEntity, new()
        {
            return dataContext => new BaseRepository<TEntity>(dataContext);
        }
    }
}