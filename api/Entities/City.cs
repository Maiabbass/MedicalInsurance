using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class City
    {
        public City()
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public City(int id , string name )
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
            this.Name= name ; 
            Hospitals=new List<Hospital>();
   
        }
        public int Id { get; set; }

        public string Name  { get; set; }


        public ICollection<Hospital>Hospitals{get; set;}
        //hfjd
        //gfg
    }
}