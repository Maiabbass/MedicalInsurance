using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class HospitalEditDTO
    {
         #nullable disable
         public int Id { get; set; }
         public string Name { get; set; }
         public bool Enabled { get; set; }
         public bool Inside { get; set; }
         public int CityId { get; set; }
    }
}