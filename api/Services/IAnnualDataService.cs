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
         Task<IEnumerable<AnnualDataWithDetails>> GetAll() ;
        
      
        
         public bool Delete(int Id);
         public bool Update(int Id,  Dictionary<string, object> updateFields);
         public bool Update(int Id,decimal Amount);
         

         
    }
}