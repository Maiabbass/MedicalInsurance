using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class ClaimEditDTO
    {
         public int? HospitalId { get; set; }
    public int? PersonId { get; set; }
    public string EnsuranceNumber { get; set; }
    public string FullName { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Company_fees { get; set; }
    public decimal ApprovedPrice { get; set; }
    public decimal NonAdd { get; set; }
    public decimal NonAddForPerson { get; set; }
    public decimal EnduranceRatio { get; set; }
    public bool Trust { get; set; }
    public DateTime? LoginDate { get; set; }
    public DateTime? ExitDate { get; set; }
    public int? SurgicalProceduresId { get; set; }
    public DateTime? DateSurgicalProcedures { get; set; }
    public int? Year { get; set; }
    public DateTime? ClimeData { get; set; }

    public int Number { get ; set ;}
    }
}