using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOS
{
    public class RegisterAnnualDataDTO
    {
         public int EngineerId { get; set; }

         public bool EngineerIsRegistered { get; set; }
         
         public List<int>PersonsIds { get; set; }

         public decimal  ExAmount { get; set; }

         public int Year { get; set; }

          public int PayMethodId { get; set; }


    }
}