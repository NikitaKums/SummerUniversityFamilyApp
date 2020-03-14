using System.Collections.Generic;

namespace Domain
{
    public class Relationship : BaseEntity
    {
        public Relation Relation { get; set; }
        
        public ICollection<PersonInRelationship> PersonInRelationships { get; set; }
    }
}