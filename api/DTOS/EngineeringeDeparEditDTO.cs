using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class EngineeringeDeparEditDTO 
    {
         #nullable disable
        public int Id { get; set; } 
        public String  Name  { get; set; }
        public int SpecializationId { get; set; }
    }
}