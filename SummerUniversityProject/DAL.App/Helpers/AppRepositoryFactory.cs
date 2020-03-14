using Contracts.DAL.App.Mappers;
using Contracts.DAL.App.Repositories;
using DAL.App.Mappers;
using DAL.App.Repositories;
using DAL.Base.Helpers;

namespace DAL.App.Helpers
{
    public class AppRepositoryFactory : BaseRepositoryFactory
    {
        public AppRepositoryFactory()
        {
            RepositoryCreationMethods.Add(typeof(IPersonRepository),
                dataContext => new PersonRepository(dataContext));

            RepositoryCreationMethods.Add(typeof(IRelationshipRepository),
                dataContext => new RelationshipRepository(dataContext));

            RepositoryCreationMethods.Add(typeof(IPersonInRelationshipRepository),
                dataContext => new PersonInRelationshipRepository(dataContext));
        }
    }
}