using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.DTOS
{
    public class RecoveredSummary
    {
         public IEnumerable<Recovered> Recovereds { get; set; }
    public decimal? TotalPriceSum { get; set; }
    public decimal? CompanyFeesSum { get; set; }
    public decimal? ApprovedPriceSum { get; set; }
    public decimal? NonAddSum { get; set; }
    public decimal? NonAddForPersonSum { get; set; }
    public int Count { get; set; }
    }
}