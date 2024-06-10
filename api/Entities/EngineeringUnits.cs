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
        public EngineeringUnits(int id,string name, string namepr , string phonepre , string emailpre)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
            this.Name=name;
            this.Namepresident=namepr;  
            this.Phonepresident=phonepre;   
            this.Emailpresident=emailpre;

 
   
        }
        public int Id { get; set; }

        public string Name  { get; set; }

      //  public Engineere Engineere{ get; set;}
      public string  Namepresident { get; set; }

      public string Phonepresident { get; set; }

      public string Emailpresident { get; set; }

        public ICollection<AnnualData>AnnualDatas { get; set;}

        public ICollection<WorkPlace> WorkPlaces { get; set; }
    }
}