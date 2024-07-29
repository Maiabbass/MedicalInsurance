using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class RecoveredDto
    {
 
    public string EnsuranceNumber { get; set; }
    public string FullName { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? Company_fees { get; set; }
    public decimal? ApprovedPrice { get; set; }
    public decimal? non_Add { get; set; }
    public decimal? non_AddForPerson { get; set; }
    public decimal? EnduranceRatio { get; set; }
    public int HospitalId { get; set; } 
    public bool Send { get; set; }
    public DateTime? LoginDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public int PersonId { get; set; }
    public int  SurgicalProceduresId { get; set; } 
}

    }
