using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class PersonInRelationship : BaseEntity
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        
        public int Person1Id { get; set; }
        public Person Person1 { get; set; }

        public int RelationshipId { get; set; }
        public Relationship Relationship { get; set; }
    }
}