using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Hospital
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Hospital(int id, string name, bool stat, bool stat2,int cityId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
            this.Name=name;
    this.Stat = stat;
    this.Stat2= stat2;
    this.CityId=cityId;

   
        }
        public int Id { get; set; }
        
        public string Name  { get; set; }

        public bool Stat { get; set; }

        public bool Stat2 { get ; set ;}

        public int CityId {get ; set ;}
        public City City { get ; set ;}

        public ICollection<Claims> Claims{get ; set; }
    }
}