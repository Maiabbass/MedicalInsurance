using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class HospitalEditDTO
    {
        
         public int Id { get; set; }
         public string Name { get; set; }
         public bool Enabled { get; set; }
         public bool Inside { get; set; }
         public int CityId { get; set; }
         public string Address { get; set; }
         public string? Email { get; set; }
         public string? Phone { get; set; }
         public decimal? Longitude{get ; set ;}
         public decimal? Latitude { get ; set;}
         public int Year { get ; set;}
         public string? NoteContent { get; set; } 
    }
}