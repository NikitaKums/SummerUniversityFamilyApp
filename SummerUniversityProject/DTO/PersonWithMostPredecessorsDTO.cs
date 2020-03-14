using System.Collections.Generic;

namespace DTO
{
    public class PersonWithMostPredecessorsDTO
    {
        public PersonDataDTO PersonData { get; set; }
        
        public int PredecessorsCount { get; set; }
        
        public List<PersonDataDTO> Predecessors { get; set; }
    }
}