using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Mappers;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.Mappers;
using DAL.Base.Repositories;
using Domain;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly IPersonMapper _personMapper;
        
        public PersonRepository(IDataContext dataContext) : base(dataContext)
        {
            _personMapper = new PersonMapper();
        }

        public async Task<int> CountDataAmount(string search)
        {
            var query = RepositoryDbSet.AsQueryable();
            query = Search(query, search);

            return await query.CountAsync();
        }
        
        public async Task<IEnumerable<PersonDTO>> AllAsyncApi(string search, int? pageIndex, int? pageSize)
        {
            var query = RepositoryDbSet
                .Include(i => i.RelatedFrom)
                .Include(i => i.RelatedTo).AsQueryable();

            query = Search(query, search);

            if (pageIndex != null && pageSize != null)
            {
                var tempPageIndex = pageIndex.GetValueOrDefault();
                var tempPageSize = pageSize.GetValueOrDefault();
                query = query.Skip((tempPageIndex - 1) * tempPageSize).Take(tempPageSize);
            }

            return await query.Select(e => new PersonDTO()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Age = e.Age,
                Id = e.Id,
                RelationCount = e.RelatedFrom.Count + e.RelatedTo.Count
            }).ToListAsync();
        }

        public async Task<PersonInRelationshipDTO> FindAllRelationshipsAsync(int id)
        {
            return await RepositoryDbSet
                .Include(i => i.RelatedFrom).ThenInclude(ii => ii.Relationship)
                .Include(i => i.RelatedTo).ThenInclude(ii => ii.Relationship)
                .Where(w => w.Id == id)
                .Select(e => new PersonInRelationshipDTO()
                {
                    PersonData = new PersonDataDTO()
                    {
                        Id = e.Id,
                        Age = e.Age,
                        FirstName = e.FirstName,
                        LastName = e.LastName
                    },
                    PersonRelatedTo = e.RelatedTo.Select(ee => new RelationshipDTO()
                    {
                        Id = ee.RelationshipId,
                        PersonData = new PersonDataDTO()
                        {
                            Id = ee.PersonId,
                            Age = ee.Person.Age,
                            FirstName = ee.Person.FirstName,
                            LastName = ee.Person.LastName
                        },
                        PersonInRelationshipId = ee.Id,
                        Relation = ee.Relationship.Relation.ToString()
                    }).ToList(),
                    PersonRelatedFrom = e.RelatedFrom.Select(ee => new RelationshipDTO()
                    {
                        Id = ee.RelationshipId,
                        PersonData = new PersonDataDTO()
                        {
                            Id = ee.Person1Id,
                            Age = ee.Person1.Age,
                            FirstName = ee.Person1.FirstName,
                            LastName = ee.Person1.LastName
                        },
                        PersonInRelationshipId = ee.Id,
                        Relation = ee.Relationship.Relation.ToString()
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PersonDataDTO>> GetDaughtersAndSons()
        {
            return await RepositoryDbContext.Set<Domain.PersonInRelationship>()
                .Include(i => i.Person)
                .Where(w => w.Relationship.Relation == Relation.Tütar || w.Relationship.Relation == Relation.Poeg)
                .Select(e => new PersonDataDTO()
                {
                    Id = e.Person.Id,
                    FirstName = e.Person.FirstName,
                    LastName = e.Person.LastName,
                    Age = e.Person.Age
                }).Distinct().ToListAsync();
        }

        public async Task<NthChildInFamilyDTO> GetNthChildInFamily(int id)
        {
            // get mom
            var mom = await RepositoryDbContext.Set<Domain.PersonInRelationship>()
                .Where(w => w.Person1Id == id && w.Relationship.Relation == Relation.Ema)
                .Select(e => e.Person).FirstOrDefaultAsync();
            
            // get dad
            var dad = await RepositoryDbContext.Set<Domain.PersonInRelationship>()
                .Where(w => w.Person1Id == id && w.Relationship.Relation == Relation.Isa)
                .Select(e => e.Person).FirstOrDefaultAsync();
            
            // get children
            var childrenCount = await RepositoryDbContext.Set<Domain.PersonInRelationship>()
                .Include(i => i.Person)
                .Include(i => i.Person1)
                .Where(w => 
                    (dad != null && w.PersonId == dad.Id) && w.Relationship.Relation == Relation.Isa ||
                    (mom != null && w.PersonId == mom.Id) && w.Relationship.Relation == Relation.Ema)
                .Select(e => e.Person1).Distinct().CountAsync();

            return _personMapper.MapToNthChild(
                childrenCount, 
                await RepositoryDbSet.Where(w => w.Id == id).FirstOrDefaultAsync());
        }

        private int RelationsCounter { get; set; }
        private Dictionary<int, List<Person>> RelatedPersonsIds { get; set; } = new Dictionary<int, List<Person>>();

        public async Task<PersonWithMostPredecessorsDTO> GetPersonWithMostPredecessors()
        {
            var result = new Dictionary<int, Tuple<int, Person>>();
            var index = 0;

            var personsWithParents = await RepositoryDbContext.Set<Domain.PersonInRelationship>()
                .Include(i => i.Relationship)
                .Include(i => i.Person1)
                .Where(w => (w.Relationship.Relation == Relation.Ema || w.Relationship.Relation == Relation.Isa))
                .Select(e => e.Person1)
                .Where(w => w.RelatedFrom.Any(ww => ww.Relationship.Relation == Relation.Poeg || 
                                                  ww.Relationship.Relation == Relation.Tütar)).ToListAsync();

            foreach (var person in personsWithParents)
            {
                RelatedPersonsIds[person.Id] = new List<Person>();
                await GetParents(person, person.Id);
                result.Add(index, new Tuple<int, Person>(RelationsCounter, person));
                
                index += 1;
                RelationsCounter = new int();
            }

            return _personMapper.MapToPredecessorDtoFormat(result, RelatedPersonsIds);

        }

        private List<int> IdsToExclude { get; set; }
        
        public async Task<FamilyTreeDTO> GetFamilyTreeForPerson(int id)
        {
            IdsToExclude = new List<int>();
            var person = await RepositoryDbSet
                .Where(w => w.Id == id).FirstOrDefaultAsync();

            return await GetRelatedPeople(person, new FamilyTreeDTO());
        }
        
        private async Task<FamilyTreeDTO> GetRelatedPeople(Person childPerson, FamilyTreeDTO familyTree)
        {
            var personsQueue = new Queue<Person>();
            familyTree.Family = new List<FamilyTreeDTO>();
            
            // get all related persons
            var relatedPeople = await RepositoryDbContext.Set<Domain.PersonInRelationship>()
                .Include(i => i.Person)
                .Include(i => i.Person1)
                .Include(i => i.Relationship)
                .Where(w => w.PersonId == childPerson.Id)
                .Select(e => e.Person1).Distinct().ToListAsync();
            
            IdsToExclude.Add(childPerson.Id);
            
            familyTree.Id = childPerson.Id;
            familyTree.FirstName = childPerson.FirstName;
            familyTree.LastName = childPerson.LastName;
            familyTree.Age = childPerson.Age;
            
            foreach (var person in relatedPeople.Where(person => !IdsToExclude.Contains(person.Id)))
            {
                personsQueue.Enqueue(person);
            }

            while (personsQueue.Count != 0)
            {
                var personToCheck = personsQueue.Dequeue();
                familyTree.Family.Add(await GetRelatedPeople(personToCheck, new FamilyTreeDTO()));
            }

            return familyTree;
        }

        private async Task GetParents(Person child, int initialPersonId)
        {
            var query = RepositoryDbContext.Set<Domain.PersonInRelationship>()
                .Include(i => i.Relationship)
                .Where(w => w.Person1 == child);
            
            var mom = await query.Where(w => w.Relationship.Relation == Relation.Ema).Select(e => e.Person)
                .FirstOrDefaultAsync();
            
            var dad = await query.Where(w => w.Relationship.Relation == Relation.Isa).Select(e => e.Person)
                .FirstOrDefaultAsync();

            if (!PersonFirstNameMissing(mom))
            {
                RelationsCounter += 1;
                RelatedPersonsIds[initialPersonId].Add(mom);
                await GetParents(mom, initialPersonId);
            }

            if (!PersonFirstNameMissing(dad))
            {
                RelationsCounter += 1;
                RelatedPersonsIds[initialPersonId].Add(dad);
                await GetParents(dad, initialPersonId);
            }
        }
        
        private IQueryable<Person> Search(IQueryable<Person> query, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                query = query.Where(p =>
                    p.FirstName.ToLower().Contains(search) ||
                    p.LastName.ToLower().Contains(search));
            }

            return query;
        }

        private static bool PersonFirstNameMissing(Person person)
        {
            try
            {
                var firstName = person.FirstName;
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}