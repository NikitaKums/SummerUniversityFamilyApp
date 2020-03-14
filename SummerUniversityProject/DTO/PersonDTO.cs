using System;
using System.Collections.Generic;

namespace DTO
{
    public class PersonDTO : BaseEntityDTO
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public int Age { get; set; }
        
        public int RelationCount { get; set; }
        
        public List<PersonInRelationshipDTO> RelatedFrom { get; set; }
        public List<PersonInRelationshipDTO> RelatedTo { get; set; }
    }
}