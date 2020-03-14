namespace DTO
{
    public class PersonInRelationshipSingleDTO : BaseEntityDTO
    {
        public PersonDTO Person { get; set; }
        
        public PersonDTO Person1 { get; set; }
        
        public RelationshipDTO Relationship { get; set; }
    }
}