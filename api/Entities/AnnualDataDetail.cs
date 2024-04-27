using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class AnnualDataDetail
    {
        public int Id { get; set; }

        


         public int PersonId { get; set; }

         public Person? Person { get; set; }

        public int AnnualDataId { get; set; }

         public AnnualData? AnnualData { get; set; }



         public bool IsEngineer { get; set; }


           [Column(TypeName = "decimal(18,2)")]
          public decimal Amount { get; set; }


        
        



    }
}