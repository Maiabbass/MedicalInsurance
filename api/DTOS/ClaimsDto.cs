using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api.Entities;

namespace api.DTOS
{
    public class ClaimsDto
    {
         public int Id { get; set; }
    public string EnsuranceNumber { get; set; }
    public string FullName { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Company_fees { get; set; }
    public decimal ApprovedPrice { get; set; }
    public decimal Non_Add { get; set; }
    public decimal Non_AddForPerson { get; set; }
    public decimal EnduranceRatio { get; set; }
    public int HospitalId { get; set; }
    public DateTime LoginDate { get; set; }
    public DateTime ExitDate { get; set; }
    public int PersonId { get; set; }
    public int SurgicalProceduresId { get; set; }
    public DateTime ClimeData { get; set; }
    public int Number { get; set; }

    public int Year {get ; set; }

   
    }
}