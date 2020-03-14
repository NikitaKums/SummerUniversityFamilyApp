using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonInRelationshipRepository : IBaseRepositoryAsync<PersonInRelationship>
    {
        void DeleteAllRelationsForPerson(int personId);
        Task<PersonInRelationshipSingleDTO> FindAsyncSingle(int id);
        Task<PersonDataDTO> FindYoungestAunt();
        Task<PersonDataDTO> FindYoungestUncle();
    }
}