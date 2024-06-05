using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers;
using api.Entities;

namespace api.DTOS
{
    public class RegisterAnnualDataDTO
    {
        public int Id { get; set;}
         public int EngineerId { get; set; }

         public bool EngineerIsRegistered { get; set; }
         
         public List<int>? PersonsIds { get; set; }

         public decimal  ExAmount { get; set; }
       

         public int Year { get; set; }
         public  DateTime? HisDic { get; set; }

          public int PayMethodId { get; set; }

          //for edit 

          public int WorkPlaceId { get; set; }
          public int EngineeringUnitsId { get; set; }
        public decimal  Amount { get; set; }

          public class AnnualDataWithDetails
{
    public  Entities.AnnualData? AnnualData { get; set; }
    public ICollection<AnnualDataDetail>? AnnualDataDetails { get; set; }
}
          


    }
}