using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers;
using api.DTOS;
using api.Entities;
using static api.DTOS.RegisterAnnualDataDTO;
using static api.Repositories.AnnualDataRepository;

namespace api.Services
{
    public interface IAnnualDataService
    {
          Task <Response> Add (RegisterAnnualDataDTO registerAnnualDataDTO); 
          Task<AnnualDataWithDetails> Get (int  AnnualDataId);
         Task<IEnumerable<AnnualDataWithDetails>> GetAll(int pageNumber, int pageSize) ;
        
      
        
         public bool Delete(int Id);
         public bool Update(int Id,   AnnualDataForView annualDataForViews);
         public bool Update(int Id,AnnualDataDetailForView annualDataDetailForView);
         
          public decimal calcualteAmount(DateTime? birthDate,int year);

         
          Task <Response> AddAnnualSettings (AnnualSettingDTO annualSettingDTO); 
           public bool DeleteAnnuaSetting(int year);

           Task<Response> UpdateAnnualSettings( AnnualSettingDTO annualSettingDTO);

           Task<IEnumerable<AnnualDataWithDetails>> GetByYear(int year, int pageNumber, int pageSize);

           Task<(SimpleEngineer engineer, bool? cardStatus, int? payMethod)> GetEngineerDetailsAndCardStatus(string insuranceNumber, int year);

          
    }
}