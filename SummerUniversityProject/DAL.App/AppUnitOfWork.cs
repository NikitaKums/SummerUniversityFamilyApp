using System;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using Contracts.DAL.Base.Repositories;

namespace DAL.App
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        private readonly IRepositoryProvider _repositoryProvider;

        public AppUnitOfWork(IDataContext dataContext, IRepositoryProvider repositoryProvider)
        {
            _appDbContext = dataContext as AppDbContext ?? throw new ArgumentNullException(nameof(dataContext));
            _repositoryProvider = repositoryProvider;
        }

        public IPersonRepository Persons =>
            _repositoryProvider.GetRepository<IPersonRepository>();
        
        public IRelationshipRepository Relationships =>
            _repositoryProvider.GetRepository<IRelationshipRepository>();
        
        public IPersonInRelationshipRepository PersonInRelationships =>
            _repositoryProvider.GetRepository<IPersonInRelationshipRepository>();

        public IBaseRepositoryAsync<TEntity> BaseRepository<TEntity>() where TEntity : class, IBaseEntity, new() =>
            _repositoryProvider.GetRepositoryForEntity<TEntity>();
        
        
        public virtual int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}