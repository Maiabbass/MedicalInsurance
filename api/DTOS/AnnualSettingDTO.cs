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
         public int Year { get; set; }
         public decimal? InsideHospitalPercentage { get; set; }
         public decimal? OutsideHospitalPercentage { get; set; }
         public decimal? CardPrice { get; set; }
         public List<AgeSegments>? AgeSegments{get;set;}
    }
}