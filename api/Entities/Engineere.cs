using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Engineere
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Engineere(int id, string engNumber, string subNumber, int personId,  int statusId,  int workPlaceId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
    this.EngNumber = engNumber;
    this.SubNumber = subNumber;
    this.PersonId = personId;
    this.statusId = statusId;
    this.WorkPlaceId = workPlaceId;
  
   
        }
         public int Id{ get; set; }

        public string EngNumber { get; set; }

        public string SubNumber { get; set; }

        


        public int PersonId { get; set; }    
        public Person Person { get; set; }

        public ICollection<Relation>Relations{get ; set ;}

        public int statusId{ get; set;}
        public Status Status { get; set;}

        public int  SpecializationId{ get; set;}
        public Specialization Specialization{ get; set;}

        public int WorkPlaceId{ get; set;}
        public WorkPlace WorkPlace { get; set;}

        

        public AnnualData AnnualData{ get; set;}
    }
}