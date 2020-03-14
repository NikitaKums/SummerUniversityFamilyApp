using System.Collections.Generic;

namespace DTO
{
    public class PersonInRelationshipDTO
    {
        public PersonDataDTO PersonData { get; set; }
        
        public List<RelationshipDTO> PersonRelatedTo { get; set; }
        public List<RelationshipDTO> PersonRelatedFrom { get; set; }
    }
}