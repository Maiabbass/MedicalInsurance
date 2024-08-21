using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class ClaimDetailsDTO
{
    public int Id { get; set; }
    public string EnsuranceNumber { get; set; }
    public string FullName { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Company_fees { get; set; }
    public decimal ApprovedPrice { get; set; }
    public decimal non_Add { get; set; }
    public decimal non_AddForPerson { get; set; }
    public decimal EnduranceRatio { get; set; }
    public string HospitalName { get; set; }
    public bool Trust { get; set; }
    public DateTime? LoginDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public int PersonId { get; set; }
    public SurgicalProceduresEditDTO SurgicalProcedures { get; set; }

    public DateTime? DateSurgicalProcedures {get ; set; }
}
}