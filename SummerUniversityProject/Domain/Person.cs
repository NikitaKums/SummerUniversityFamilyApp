using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Person : BaseEntity
    {
        [MaxLength(128)]
        public string FirstName { get; set; }
        
        [MaxLength(128)]
        public string LastName { get; set; }

        public int Age { get; set; }

        public ICollection<PersonInRelationship> RelatedFrom { get; set; }
        public ICollection<PersonInRelationship> RelatedTo { get; set; }
    }
}