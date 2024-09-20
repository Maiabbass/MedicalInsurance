using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.DTOS
{

    /// <summary>
    /// this class will be used to insert new annual data 
    /// 
    /// </summary>
    public class AnnualSettingDTO
    {
        public int Id { get; set; }
         public int Year { get; set; }
         
         public decimal? CardPrice { get; set; }
         
         public List<AgeSegmentDTO> AgeSegments { get; set; }

          
          
          public List<RelationTypeDTO> RelationTypes { get; set; } 
     
          public List<HospitalEditDTO> Hospitals  {get ; set;}
         

          public List<SurgicalProceduresEditDTO> Surgicals {get ; set;}
    }
}