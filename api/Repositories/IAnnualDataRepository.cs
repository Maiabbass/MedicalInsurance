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

     
       Task<IEnumerable<AnnualDataWithDetails>> GetAll(int pageNumber, int pageSize) ;
      

        Task<int>Add_AnnualData (AnnualData annualData);

        Task<int> Add_AnnualDataDetail(AnnualDataDetail annualDataDetail);
         void DeleteByPersonId(int PersonId);
         void DeleteByAnnualDataId(int AnnualDataId);

         void Delete(int Id);
       
        public bool Update(int Id,AnnalDataForEdit  annualDataForEdit );
        public bool Update(int Id, AnnualDataDetailForView annualDataDetailForView  );


        Task<int> Add_Year_Configuration(YearConfiguration yearConfiguration);
        Task Delete_Year_Configuration(int year);

        Task Update_Year_Configuration(YearConfiguration yearConfiguration);
        Task<IEnumerable<AnnualDataWithDetails>> GetByYear(int year, int pageNumber, int pageSize);

       Task<(SimpleEngineer engineer, bool? cardStatus, int? payMethod)> GetEngineerDetailsAndCardStatus(string insuranceNumber, int year);


        Task<AnnualDataDetail?> GetAnnualDataDetailAsync(string ensuranceNumber, int year);

       Task<decimal> GetClaimsSumForPersonAsync(string ensuranceNumber);

     Task<AnnualData> GetByEngineerIdAndYear(int engineerId, int year);
     Task<List<AnnualDataDetail>> GetAnnualDataDetails(int annualDataId);
      Task<Person> GetByInsuranceNumber(string engineerEnsuranceNumber);

      
     Task<PayMethod> GetPayMethodByEngineerId(int engineerId);

      Task<AnnualDataDetail> GetAnnualDataDetailByPersonIdAndYear(int personId, int year);
      Task<List<int>> GetPersonsWithClaimsByYearAsync(int year);
      Task UpdateAnnualData(AnnualData annualData);
       Task UpdateAnnualDataDetail(AnnualDataDetail annualDataDetail);
       Task<List<EngineerStatusDto>> GetEngineerStatusByYear(int engineerId);
       Task<List<AnnualDataDetailStatusDto>> GetFamilyMemberStatusByYear(int personId);

         
   
    }
}