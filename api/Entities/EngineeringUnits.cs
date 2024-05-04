using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class EngineeringUnits
    {
        public EngineeringUnits()
        {
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public EngineeringUnits(int id,int number)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
            this.Number=number;
 
   
        }
        public int Id { get; set; }

        public int Number  { get; set; }

      //  public Engineere Engineere{ get; set;}

        public ICollection<AnnualData>AnnualDatas { get; set;}

        public ICollection<WorkPlace> WorkPlaces { get; set; }
    }
}