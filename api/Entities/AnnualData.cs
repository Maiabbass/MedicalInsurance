using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;

namespace api.Entities
{
    public class AnnualData
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public AnnualData()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        {
         
        }
        public int Id { get; set; }

        public int Year { get; set; }

      [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }


         [Column(TypeName = "decimal(18,2)")]
        public decimal ExAmount  { get; set; }

         [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount  { get; set; }

        public int EngineereId { get; set;}
        public Engineere Engineere {get; set;}

        public int PayMethodId{ get; set;}
        public PayMethod PayMethod { get; set;}

        public int? WorkPlaceId { get; set;}
        public WorkPlace? WorkPlace { get; set;}

        public int? EngineeringUnitsId { get; set;} 
        public  EngineeringUnits? EngineeringUnits{ get; set;}


        public ICollection<AnnualDataDetail>  AnnualDataDetails { get; set; }

        internal object Select(Func<object, AnnualDataForView> value)
        {
            throw new NotImplementedException();
        }

        public static explicit operator List<object>(AnnualData? v)
        {
            throw new NotImplementedException();
        }
    }
}