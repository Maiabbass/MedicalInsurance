using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class AnnualData
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AnnualData(int id, int year, decimal amount,  decimal exAmount , int engineereId, int payMethodId, int workPlaceId, int engineeringUnitsId)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
            this.Id = id;
    this.Year = year;
    this.Amount = amount;
    this.ExAmount= exAmount;
    this.EngineereId = engineereId;
    this.PayMethodId = payMethodId;
    this.WorkPlaceId = workPlaceId;
    this.EngineeringUnitsId = engineeringUnitsId;
   
        }
        public int Id { get; set; }

        public int Year { get; set; }

      [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }


  [Column(TypeName = "decimal(18,2)")]
        public decimal ExAmount  { get; set; }

        public int EngineereId { get; set;}
        public Engineere Engineere {get; set;}

        public int PayMethodId{ get; set;}
        public PayMethod PayMethod { get; set;}

        public int WorkPlaceId { get; set;}
        public WorkPlace WorkPlace { get; set;}

        public int EngineeringUnitsId { get; set;} 
        public  EngineeringUnits EngineeringUnits{ get; set;}
    }
}