using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class PersonDetailDTO
    {
    public int PersonId { get; set; }
    public bool Subscrib { get; set; }
    public bool Affiliate { get; set; }
    public bool Beneficiary { get; set; }
    public bool CardStatuse { get; set; }
    public decimal ExAmount { get; set; }
    public int Year { get; set; } 

    public bool Waiting{ get ; set;}
    }
}