using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.Base.Repositories;
using Domain;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.Repositories
{
    public class PersonInRelationshipRepository : BaseRepository<PersonInRelationship>, IPersonInRelationshipRepository
    {
        public PersonInRelationshipRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public void DeleteAllRelationsForPerson(int personId)
        {
            RepositoryDbSet.RemoveRange(RepositoryDbSet.
                Where(w => w.Person1Id == personId || w.PersonId == personId));
        }

        public async Task<PersonInRelationshipSingleDTO> FindAsyncSingle(int id)
        {
            return await RepositoryDbSet
                .Include(i => i.Person)
                .Include(i => i.Person1)
                .Include(i => i.Relationship)
                .Where(w => w.Id == id)
                .Select(e => new PersonInRelationshipSingleDTO()
                {
                    Id = e.Id,
                    Person = new PersonDTO()
                    {
                        Id = e.PersonId,
                        Age = e.Person.Age,
                        FirstName = e.Person.FirstName,
                        LastName = e.Person.LastName
                    },
                    Person1 = new PersonDTO()
                    {
                        Id = e.Person1Id,
                        Age = e.Person1.Age,
                        FirstName = e.Person1.FirstName,
                        LastName = e.Person1.LastName
                    },
                    Relationship = new RelationshipDTO()
                    {
                        Id = e.RelationshipId,
                        Relation = e.Relationship.Relation.ToString()
                    }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PersonDataDTO> FindYoungestAunt()
        {
            return await FindYoungest(Relation.Tädi);
        }

        public async Task<PersonDataDTO> FindYoungestUncle()
        {
            return await FindYoungest(Relation.Onu);
        }

        private Task<PersonDataDTO> FindYoungest(Relation relation)
        {
            return RepositoryDbSet
                .Include(i => i.Person)
                .Include(i => i.Relationship)
                .Where(w => w.Relationship.Relation == relation)
                .OrderBy(o => o.Person.Age)
                .Select(e => new PersonDataDTO()
                {
                    Id = e.PersonId,
                    Age = e.Person.Age,
                    FirstName = e.Person.FirstName,
                    LastName = e.Person.LastName
                })
                .FirstOrDefaultAsync();
        }
    }
}