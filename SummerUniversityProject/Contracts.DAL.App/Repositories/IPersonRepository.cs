using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPersonRepository : IBaseRepositoryAsync<Person>
    {
        Task<IEnumerable<PersonDTO>> AllAsyncApi(string search, int? pageIndex, int? pageSize);
        Task<PersonInRelationshipDTO> FindAllRelationshipsAsync(int id);
        Task<IEnumerable<PersonDataDTO>> GetDaughtersAndSons();
        Task<NthChildInFamilyDTO> GetNthChildInFamily(int id);
        Task<FamilyTreeDTO> GetFamilyTreeForPerson(int id);
        Task<int> CountDataAmount(string search);
        Task<PersonWithMostPredecessorsDTO> GetPersonWithMostPredecessors();
    }
}