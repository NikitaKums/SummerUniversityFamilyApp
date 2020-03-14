using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRelationshipRepository : IBaseRepositoryAsync<Relationship>
    {
        Task<IEnumerable<RelationshipDTO>> AllAsyncApi();
        Task<int> FindIdByString(Relation relation);
    }
}