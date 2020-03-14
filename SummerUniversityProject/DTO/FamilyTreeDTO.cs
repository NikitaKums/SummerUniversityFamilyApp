using System.Collections.Generic;

namespace DTO
{
    public class FamilyTreeDTO : PersonDataDTO
    {
        public List<FamilyTreeDTO> Family { get; set; }
    }
}