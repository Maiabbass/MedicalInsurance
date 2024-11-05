using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class EngineerFamilyFullDataDTO
    {
        public FullPersonDataDTO EngineerInfo { get; set; }
        public List<FullPersonDataDTO> FamilyData { get; set; } = new List<FullPersonDataDTO>();
        public decimal TotalAmount { get; set; }
    }
}
