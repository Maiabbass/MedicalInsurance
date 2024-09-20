using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class CityEditDTO
    {
        #nullable disable
        public int Id { get; set; } 
        public string  Name  { get; set; }
        public string CallingCode { get ; set;}
    }
}