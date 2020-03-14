using System;
using System.Collections.Generic;
using Domain;
using DTO;

namespace Contracts.DAL.App.Mappers
{
    public interface IPersonMapper
    {
        PersonWithMostPredecessorsDTO MapToPredecessorDtoFormat(
            Dictionary<int, Tuple<int, Person>> personsWithMostPredecessors,
            Dictionary<int, List<Person>> predecessorPersonsDictionary);

        NthChildInFamilyDTO MapToNthChild(int childCount, Person person);
    }
}