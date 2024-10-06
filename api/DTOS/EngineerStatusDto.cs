using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
  public class EngineerStatusDto
{
    public int Year { get; set; }
    public string InsuranceNumber { get; set; }
    public string Status { get; set; }
    public decimal? NonAddForPersonSum { get; set; } // Add a new property for the sum
}

}