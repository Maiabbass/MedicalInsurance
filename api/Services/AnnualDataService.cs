using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.DTOS;
using api.Entities;
using api.Repositories;
using api.Extensions;
using static api.Repositories.AnnualDataRepository;
using static api.DTOS.RegisterAnnualDataDTO;

namespace api.Services
{
    public class AnnualDataService : IAnnualDataService
    {

         private readonly IUnitOfWork _unitOfWork;
         private readonly IClimsRepository _climsRepository;


        public AnnualDataService(IUnitOfWork unitOfWork, IClimsRepository climsRepository)
         {
            _unitOfWork = unitOfWork;
            _climsRepository=climsRepository;
         }

       public async Task<Response> Add(RegisterAnnualDataDTO registerAnnualDataDTO)
{
    Response response = new Response();
    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // ضبط القيم الافتراضية
            registerAnnualDataDTO.Subscrib = true;
            registerAnnualDataDTO.Affiliate = true;

            // تحقق مما إذا كان EngineerId موجودًا في جدول Claims
            bool isBeneficiary = await _unitOfWork.ClimsRepository.ExistsAsync(registerAnnualDataDTO.EngineerId);
            registerAnnualDataDTO.Beneficiary = isBeneficiary;

            // prepare AnnualData Object ...
            AnnualData annualData = new AnnualData
            {
                Year = registerAnnualDataDTO.Year,
                ExAmount = registerAnnualDataDTO.ExAmount,
                HisDic = registerAnnualDataDTO.HisDic,
               CardStatuse=registerAnnualDataDTO.CardStatuse,
                Subscrib = registerAnnualDataDTO.Subscrib,
                Affiliate = registerAnnualDataDTO.Affiliate,
                Beneficiary = registerAnnualDataDTO.Beneficiary
            };

            // get required engineer info ...
            var engineer = await _unitOfWork.EngineerRepository.Get(registerAnnualDataDTO.EngineerId);
            if (engineer != null)
            {
                annualData.EngineereId = engineer.Id;
                annualData.EngineeringUnitsId = null; // TO DO: fill later...
                annualData.WorkPlaceId = engineer.WorkPlaceId;
                annualData.PayMethodId = registerAnnualDataDTO.PayMethodId;
            }

            // حساب المبالغ
            var ageSegments = await _unitOfWork.AgeSegmentsRepository.Get(registerAnnualDataDTO.Year);

            // engineer...
            AnnualDataDetail engineer_annualDataDetail = new AnnualDataDetail();
            if (registerAnnualDataDTO.EngineerId > 0 && registerAnnualDataDTO.EngineerIsRegistered)
            {
                engineer_annualDataDetail = new AnnualDataDetail()
                {
                    IsEngineer = true,
                    PersonId = registerAnnualDataDTO.EngineerId,
                    Amount = 0m
                };

                Person? engineere = await _unitOfWork.PersonRepository.Get(registerAnnualDataDTO.EngineerId);
                if (engineere != null)
                {
                    engineer_annualDataDetail.Amount = calcualteAmount(engineere.BirthDate, registerAnnualDataDTO.Year);
                }
            }

            List<AnnualDataDetail> persons_AnnualDataDetailList = new List<AnnualDataDetail>();
            // persons
            foreach (var personid in registerAnnualDataDTO.PersonsIds)
            {
                AnnualDataDetail annualDataDetail_person = new AnnualDataDetail()
                {
                    IsEngineer = false,
                    PersonId = personid,
                    Amount = 0m
                };

                Person? person = await _unitOfWork.PersonRepository.Get(personid);
                if (person != null)
                {
                    annualDataDetail_person.Amount = calcualteAmount(person.BirthDate, registerAnnualDataDTO.Year);
                }
                persons_AnnualDataDetailList.Add(annualDataDetail_person);
            }

            if (engineer_annualDataDetail.PersonId > 0)
            {
                annualData.Amount += engineer_annualDataDetail.Amount;
            }
            decimal persons_amount = persons_AnnualDataDetailList.Sum(x => x.Amount);
            annualData.Amount += persons_amount;
            annualData.TotalAmount = annualData.Amount + annualData.ExAmount;

            int insertedAnnualDataId = await _unitOfWork.AnnualDataRepository.Add_AnnualData(annualData);
            response.InsertedId = insertedAnnualDataId;

            // insert engineer detail
            engineer_annualDataDetail.AnnualDataId = insertedAnnualDataId;
            await _unitOfWork.AnnualDataRepository.Add_AnnualDataDetail(engineer_annualDataDetail);

            // insert persons details 
            foreach (var item in persons_AnnualDataDetailList)
            {
                item.AnnualDataId = insertedAnnualDataId;
                await _unitOfWork.AnnualDataRepository.Add_AnnualDataDetail(item);
            }

            scope.Complete();
        } // using
    }
    catch (TransactionAbortedException ex)
    {
        response.ErrorMessage = ex.Message;
    }
    catch (Exception ex)
    {
        response.ErrorMessage = ex.Message;
    }
    return response;
}





        // helper method to calculate the register annual amount based on birth date and current year...
        public decimal calcualteAmount(DateTime? birthDate,int year)
        {
         
         
          decimal amount=0m;
          var  ageSegments=   _unitOfWork.AgeSegmentsRepository.Get(year).GetAwaiter().GetResult();
           var age=  birthDate.GetAge();
                            var segment= ageSegments.Where(x=> age>=x.FromYear&&age<=x.ToYear
                            ).FirstOrDefault();
                            if (segment!=null)
                            {
                                amount= segment.TheAmount;
                            }

                            return amount;
        }
         




//data from tow tabel
       public async Task<AnnualDataWithDetails?>Get(int AnnualDataId){
        return await _unitOfWork.AnnualDataRepository.Get(AnnualDataId);
       }

         
     public bool Delete(int Id){

      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
        _unitOfWork.AnnualDataRepository.DeleteByAnnualDataId(Id);
      
        _unitOfWork.AnnualDataRepository.Delete(Id);
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException)
            {

                  
                  return false;
                 }
    
     }
     public bool Update(int Id,AnnualDataForView annualDataForView){
           return _unitOfWork.AnnualDataRepository.Update(Id, annualDataForView);
        }

        public async Task<IEnumerable<AnnualDataWithDetails>> GetAll(int pageNumber, int pageSize){
            return await _unitOfWork.AnnualDataRepository.GetAll(pageNumber,pageSize);
     
    }
     public bool Update(int Id,AnnualDataDetailForView annualDataDetailForView ){
           return _unitOfWork.AnnualDataRepository.Update(Id, annualDataDetailForView);
        }
    
      


      public async Task<Response> AddAnnualSettings(AnnualSettingDTO annualSettingDTO)
      {
         Response response =new Response ();
        

         try 
         {
              using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
              {
                  YearConfiguration yearConfiguration =new  YearConfiguration()
                  {
                    Id=0,
                    Year=annualSettingDTO.Year,
                    InsideHospitalPercentage =annualSettingDTO.InsideHospitalPercentage,
                    OutsideHospitalPercentage = annualSettingDTO.OutsideHospitalPercentage,
                    CardPrice = annualSettingDTO.CardPrice,
                    Limit=annualSettingDTO.Limit,
                    

                  };
                   await _unitOfWork.AnnualDataRepository.Add_Year_Configuration(yearConfiguration);
                    
                    // add age segments 
                    foreach(var item in annualSettingDTO.AgeSegments)
                    {
                      item.Year=annualSettingDTO.Year;
                    }
                    await _unitOfWork.AgeSegmentsRepository.Add_Age_Segments(annualSettingDTO.AgeSegments);
                    response.Status="Success";
                  scope.Complete();

              }//using transaction scope 

         }
          catch(TransactionAbortedException ex){  response.ErrorMessage = ex.Message;}
         catch(Exception exx){  response.ErrorMessage = exx.Message;}
         return response;
      }



      public bool DeleteAnnuaSetting(int year)
      {
        bool completed=false;
         try 
         {
              using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
              {
                   
                    
                     _unitOfWork.AnnualDataRepository.Delete_Year_Configuration(year);
                    
                    // add age segments 
                      _unitOfWork.AgeSegmentsRepository.Delete_Age_Segments(year);
                  
                  scope.Complete();
                  completed=true;

              }//using transaction scope 

         }
          catch(TransactionAbortedException ex){   }
         catch(Exception exx){   }
         
        return completed;
      }






 public async Task<Response> UpdateAnnualSettings(AnnualSettingDTO annualSettingDTO)
{
    Response response = new Response();

    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // تحديث إعدادات السنة
            YearConfiguration yearConfiguration = new YearConfiguration()
            {
                Id = annualSettingDTO.Id,
                Year = annualSettingDTO.Year,
                InsideHospitalPercentage = annualSettingDTO.InsideHospitalPercentage,
                OutsideHospitalPercentage = annualSettingDTO.OutsideHospitalPercentage,
                CardPrice = annualSettingDTO.CardPrice,
                Limit=annualSettingDTO.Limit
            };

            await _unitOfWork.AnnualDataRepository.Update_Year_Configuration(yearConfiguration);

            // تعيين السنة في جميع الشرائح العمرية
            foreach (var item in annualSettingDTO.AgeSegments)
            {
                item.Year = annualSettingDTO.Year;
            }

            // تمرير الشرائح العمرية الجديدة
            await _unitOfWork.AgeSegmentsRepository.Update_Age_Segments(annualSettingDTO.AgeSegments);

            response.Status = "Success";
            scope.Complete();
        }
    }
    catch (TransactionAbortedException ex)
    {
        response.ErrorMessage = ex.Message;
    }
    catch (Exception exx)
    {
        response.ErrorMessage = exx.Message;
    }
    return response;
}



  public async Task<IEnumerable<AnnualDataWithDetails>> GetByYear(int year, int pageNumber, int pageSize){
    return await _unitOfWork.AnnualDataRepository.GetByYear(year,pageNumber , pageSize);
  }

   public async Task<(SimpleEngineer engineer, bool? cardStatus, int? payMethod)> GetEngineerDetailsAndCardStatus(string insuranceNumber, int year){
    return await _unitOfWork.AnnualDataRepository.GetEngineerDetailsAndCardStatus(insuranceNumber, year);
  }


    

    }
    
    
    }
