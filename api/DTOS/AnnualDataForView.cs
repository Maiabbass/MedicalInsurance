using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class AnnualDataForView
    {
         #nullable disable
          public int Id { get; set; }
          

        public int Year { get; set; }

     
        public decimal Amount { get; set; }


     
        public decimal ExAmount  { get; set; }

    
        public decimal TotalAmount  { get; set; }

        public int EngineereId { get; set;}


        public int PayMethodId{ get; set;}
       

        public int? WorkPlaceId { get; set;}
       

        public int? EngineeringUnitsId { get; set;} 
        public List<AnnualDataDetailForView> AnnualDataDetails { get;  set; }
   


        
    }
}