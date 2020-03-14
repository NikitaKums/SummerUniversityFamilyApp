using Contracts.DAL.Base;

namespace Domain
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; } // Primary Key   
    }
}