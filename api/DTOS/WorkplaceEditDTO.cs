using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class WorkplaceEditDTO
    {
        #nullable disable
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int EngineeringUnitsId { get; set; }
        public string Phone { get; set; }
    }
}