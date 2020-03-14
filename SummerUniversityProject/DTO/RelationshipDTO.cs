namespace DTO
{
    public class RelationshipDTO : BaseEntityDTO
    {
        public string Relation { get; set; }
        
        public int PersonInRelationshipId { get; set; }
        
        public PersonDataDTO PersonData { get; set; }
    }
}