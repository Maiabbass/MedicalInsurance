using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;
using static api.DTOS.RegisterAnnualDataDTO;
using static api.Repositories.AnnualDataRepository;

namespace api.Repositories
{
    public interface IAnnualDataRepository
    {
        Task<AnnualDataWithDetails> Get(int AnnualDataId);

     
       Task<IEnumerable<AnnualDataWithDetails>> GetAll() ;
      

        Task<int>Add_AnnualData (AnnualData annualData);

        Task<int> Add_AnnualDataDetail(AnnualDataDetail annualDataDetail);
         void DeleteByPersonId(int PersonId);
         void DeleteByAnnualDataId(int AnnualDataId);

         void Delete(int Id);
       
        bool Update(int id, Dictionary<string, object> updateFields);
        public bool Update(int Id, decimal Amount );
   
    }
}