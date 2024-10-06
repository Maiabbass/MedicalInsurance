using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
  public class AnnualDataDetailStatusDto
{
    public int Year { get; set; }
    public string InsuranceNumber { get; set; }
    public string Status { get; set; }
    public decimal? NonAddForPersonSum { get; set; } // Sum for NonAddForPerson
}

}