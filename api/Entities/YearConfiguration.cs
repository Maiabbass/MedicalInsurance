using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class YearConfiguration
    {
         public int Id { get; set; }
        public int Year { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? InsideHospitalPercentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
         public decimal? OutsideHospitalPercentage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
         public decimal? CardPrice { get; set; }

    }
}