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
         public bool Update(int Id,AnnalDataForEdit annualDataForEdit);
         public bool Update(int Id,AnnualDataDetailForView annualDataDetailForView);
         
          public decimal calcualteAmount(DateTime? birthDate,int year);

         
           Task<Response> AddAnnualSettings(string title, AnnualSettingDTO annualSettingDTO); 
           Task<bool> DeleteAnnuaSetting(int year);

           //Task<Response> UpdateAnnualSettings( AnnualSettingDTO annualSettingDTO);

           Task<IEnumerable<AnnualDataWithDetails>> GetByYear(int year, int pageNumber, int pageSize);

           Task<(SimpleEngineer engineer, bool? cardStatus, int? payMethod)> GetEngineerDetailsAndCardStatus(string insuranceNumber, int year);



            Task<AnnualDataDetail?> GetAnnualDataDetailAsync(string ensuranceNumber, int year);


         Task<decimal> GetClaimsSumForPersonAsync(string ensuranceNumber);


         Task<Entities.AnnualData> GetByEngineerIdAndYear(int engineerId, int year);

         Task<List<AnnualDataDetail>> GetAnnualDataDetails(int annualDataId);
/*
        Task<Response> CopyAnnualDataForNewYear(
            int previousYear, 
            int newYear, 
            string ensuranceNumber, 
            bool waiting, 
            bool cardStatus, 
            bool copyAnnualData, 
            bool copyAnnualDataDetails,
            List<AnnualNewDTO> detailIdsToCopy // استخدام DTO هنا
        );
        */

            Task<PayMethod> GetPayMethodByEngineerIdAsync(int engineerId);

            Task UpdateYearConfigurationAsync(YearConfiguration yearConfiguration);

            Task AddNoteToYearConfigAsync(int yearConfigId, NoteCreateDTO noteDto);




            Task<Response> RenewEngineerAnnualData(
            int previousYear, 
            int newYear, 
            string insuranceNumber, 
            bool waiting, 
            bool cardStatus);


            Task<Response> RenewFamilyMembersAnnualData(
    int engineerId,
    int previousYear,
    int newYear,
    List<FamilyMemberRenewalDTO> familyMembersToRenew);


    Task<Response> UpdateBeneficiaryStatus(int year);

    Task<List<EngineerStatusDto>> GetEngineerStatusByYear(int engineerId);

    Task<List<AnnualDataDetailStatusDto>> GetFamilyMemberStatusByYear(int personId);
     Task UpdatePersonAmountsBasedOnNewAgeSegments(int year);

    }
}