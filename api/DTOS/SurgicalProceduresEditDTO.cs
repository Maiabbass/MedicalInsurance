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
        public bool Technical{get ; set ;}

        public bool Financial {get ; set ; }

        public string Pathological_specialization { get; set; }

        
         public decimal Limit { get; set; }
         

         public decimal EnduranceRatio { get; set; }
    }
}