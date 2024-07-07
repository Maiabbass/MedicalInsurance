using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class SurgicalProcedures
    {

        #nullable disable
         public  int  Id { get; set; }

         public string Name { get; set; }

          public bool Technical {get ; set;}

         public bool Financial {get ; set ;}

         public string Pathological_specialization { get; set; }

         public ICollection<Claims> Claims{get ; set; }

    }
}