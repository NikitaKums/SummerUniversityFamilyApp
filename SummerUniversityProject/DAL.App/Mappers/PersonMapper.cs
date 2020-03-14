using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.DAL.App.Mappers;
using Domain;
using DTO;

namespace DAL.App.Mappers
{
    public class PersonMapper : IPersonMapper
    {
        public NthChildInFamilyDTO MapToNthChild(int childCount, Person person)
        {
            return new NthChildInFamilyDTO()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Age = person.Age,
                NthChildInFamily = childCount
            };
        }
        
        public PersonWithMostPredecessorsDTO MapToPredecessorDtoFormat(Dictionary<int, Tuple<int, Person>> personsWithMostPredecessors,
            Dictionary<int, List<Person>> predecessorPersonsDictionary)
        {
            if (personsWithMostPredecessors.Count == 0)
            {
                return null;
            }
            
            var person = personsWithMostPredecessors.OrderByDescending(a => a.Value.Item1).First().Value;
            var personPredecessors = predecessorPersonsDictionary[person.Item2.Id];

            return new PersonWithMostPredecessorsDTO()
            {
                PersonData = new PersonDataDTO()
                {
                    Id = person.Item2.Id,
                    FirstName = person.Item2.FirstName,
                    LastName = person.Item2.LastName,
                    Age = person.Item2.Age
                },
                PredecessorsCount = person.Item1,
                Predecessors = personPredecessors.Select(e => new PersonDataDTO()
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Age = e.Age
                }).ToList()
            };
        }
    }
}