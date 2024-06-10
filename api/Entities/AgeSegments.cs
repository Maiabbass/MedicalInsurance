using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class AgeSegments
    {
     
         public int Id { get; set; }

         public int Year { get; set; }

        public int FromYear { get; set; }

        public int ToYear { get; set; }

       [Column(TypeName = "decimal(18,2)")]
        public decimal TheAmount { get; set; }

       // public string CardPrice { get; set; }

       public string? EnduranceRatio { get; set; }
        

    }
}