using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class AnnualDataDetailForView
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int AnnualDataId { get; set; }
        public bool IsEngineer { get; set; }
        public decimal Amount { get; set; }
    }
}