using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class SurgicalProceduresEditDTO
    {
        #nullable disable
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}