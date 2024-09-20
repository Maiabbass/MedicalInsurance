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

         private readonly IRelationRepository _relationRepository;
         private readonly ILimitRepository _limitRepository;
     
        private readonly IEnduranceRatioRepository _enduranceRatioRepository;

        public AnnualDataService(IUnitOfWork unitOfWork, IClimsRepository climsRepository , IRelationRepository relationRepository,ILimitRepository limitRepository, IEnduranceRatioRepository enduranceRatioRepository)
         {
            _unitOfWork = unitOfWork;
            _climsRepository=climsRepository;
            _relationRepository = relationRepository;
            _limitRepository=limitRepository;
            _enduranceRatioRepository=enduranceRatioRepository;
         }



      public async Task<Response> Add(RegisterAnnualDataDTO registerAnnualDataDTO)
{
    Response response = new Response();
    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {

            registerAnnualDataDTO.Subscrib = true;
            registerAnnualDataDTO.Affiliate = true;
            registerAnnualDataDTO.Waiting=false;


            bool isBeneficiary = await _unitOfWork.ClimsRepository.ExistsAsync(registerAnnualDataDTO.EngineerId, registerAnnualDataDTO.Year);
            registerAnnualDataDTO.Beneficiary = isBeneficiary;


            AnnualData annualData = new AnnualData
            {
                Year = registerAnnualDataDTO.Year,
                ExAmount = registerAnnualDataDTO.ExAmount,
                HisDic = registerAnnualDataDTO.HisDic,
                CardStatuse = registerAnnualDataDTO.CardStatuse,
                Subscrib = registerAnnualDataDTO.Subscrib,
                Affiliate = registerAnnualDataDTO.Affiliate,
                Beneficiary = registerAnnualDataDTO.Beneficiary,
                Waiting = registerAnnualDataDTO.Waiting,
            };



            var engineer = await _unitOfWork.EngineerRepository.Get(registerAnnualDataDTO.EngineerId);
            if (engineer != null)
            {
                annualData.EngineereId = engineer.PersonId;
                annualData.EngineeringUnitsId = null; 
                annualData.WorkPlaceId = engineer.WorkPlaceId;
                annualData.PayMethodId = registerAnnualDataDTO.PayMethodId;
            }

            // حساب المبالغ
            var ageSegments = await _unitOfWork.AgeSegmentsRepository.Get(registerAnnualDataDTO.Year);

            // إعداد تفاصيل المهندس
            AnnualDataDetail engineer_annualDataDetail = new AnnualDataDetail
            {
                IsEngineer = true,
                PersonId = registerAnnualDataDTO.EngineerId,
                Amount = 0m,
                Year = registerAnnualDataDTO.Year,
                CardStatuse = registerAnnualDataDTO.CardStatuse,
                ExAmount = registerAnnualDataDTO.ExAmount,
                Subscrib = true,
                Affiliate = true,
                Beneficiary = registerAnnualDataDTO.Beneficiary,
                Waiting = registerAnnualDataDTO.Waiting,
            };

            if (registerAnnualDataDTO.EngineerIsRegistered)
            {
                Person? engineerPerson = await _unitOfWork.PersonRepository.Get(registerAnnualDataDTO.EngineerId);
                if (engineerPerson != null)
                {
                    engineer_annualDataDetail.Amount = calcualteAmount(engineerPerson.BirthDate, registerAnnualDataDTO.Year);
                }
            }

            List<AnnualDataDetail> persons_AnnualDataDetailList = new List<AnnualDataDetail>();

            // إضافة تفاصيل الأشخاص
            foreach (var personDTO in registerAnnualDataDTO.Persons)
            {
                bool personBeneficiary = await _unitOfWork.ClimsRepository.ExistsAsync(personDTO.PersonId, registerAnnualDataDTO.Year);

                AnnualDataDetail annualDataDetail_person = new AnnualDataDetail()
                {
                    IsEngineer = false,
                    PersonId = personDTO.PersonId,
                    Amount = 0m,
                    Year = registerAnnualDataDTO.Year,
                    CardStatuse = personDTO.CardStatuse,
                    ExAmount = personDTO.ExAmount,
                    Subscrib = true,
                    Affiliate = true,
                    Beneficiary = personBeneficiary,
                    Waiting=false,
                };

                Person? personEntity = await _unitOfWork.PersonRepository.Get(personDTO.PersonId);
                if (personEntity != null)
                {
                    annualDataDetail_person.Amount = calcualteAmount(personEntity.BirthDate, registerAnnualDataDTO.Year);
                }
                persons_AnnualDataDetailList.Add(annualDataDetail_person);
            }

            // حساب المبالغ الإجمالية
            if (engineer_annualDataDetail.PersonId > 0)
            {
                annualData.Amount += engineer_annualDataDetail.Amount;
            }
            decimal persons_amount = persons_AnnualDataDetailList.Sum(x => x.Amount);
            annualData.Amount += persons_amount;
            annualData.TotalAmount = annualData.Amount + annualData.ExAmount;

            // إدراج السجل في جدول AnnualData
            int insertedAnnualDataId = await _unitOfWork.AnnualDataRepository.Add_AnnualData(annualData);
            response.InsertedId = insertedAnnualDataId;

            // إدراج تفاصيل المهندس في جدول AnnualDataDetail
            engineer_annualDataDetail.AnnualDataId = insertedAnnualDataId;
            await _unitOfWork.AnnualDataRepository.Add_AnnualDataDetail(engineer_annualDataDetail);

            // إدراج تفاصيل الأشخاص في جدول AnnualDataDetail
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
     public bool Update(int Id,AnnalDataForEdit annualDataForEdit){
           return _unitOfWork.AnnualDataRepository.Update(Id, annualDataForEdit);
        }

        public async Task<IEnumerable<AnnualDataWithDetails>> GetAll(int pageNumber, int pageSize){
            return await _unitOfWork.AnnualDataRepository.GetAll(pageNumber,pageSize);
     
    }
     public bool Update(int Id,AnnualDataDetailForView annualDataDetailForView ){
           return _unitOfWork.AnnualDataRepository.Update(Id, annualDataDetailForView);
        }
    
      



       public async Task<Response> AddAnnualSettings(AnnualSettingDTO annualSettingDTO)
{
    Response response = new Response();

    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // إضافة YearConfiguration
            YearConfiguration yearConfiguration = new YearConfiguration()
            {
                Id = 0,  // Ensure new record
                Year = annualSettingDTO.Year,
                CardPrice = annualSettingDTO.CardPrice
            };
            await _unitOfWork.AnnualDataRepository.Add_Year_Configuration(yearConfiguration);

            // إضافة AgeSegments
            var ageSegmentsEntities = annualSettingDTO.AgeSegments.Select(dto => new AgeSegments
            {
                Id = 0,  // Set Id to 0 for new entries
                FromYear = dto.FromYear,
                ToYear = dto.ToYear,
                TheAmount = dto.TheAmount,
                EnduranceRatio = dto.EnduranceRatio,
                Year = annualSettingDTO.Year
            }).ToList();

            await _unitOfWork.AgeSegmentsRepository.Add_Age_Segments(ageSegmentsEntities);

            // إضافة RelationTypes
            var relationTypes = annualSettingDTO.RelationTypes.Select(dto => new RelationType
            {
                Id = 0,  // Set Id to 0 for new entries
                Name = dto.Name,
                Relations = null,
                Year = annualSettingDTO.Year
            }).ToList();

            await _unitOfWork.RelationRepository.Add_RelationType(relationTypes);

            // إضافة Hospitals
            var hospitals = annualSettingDTO.Hospitals.Select(dto => new Hospital
            {
                Id = 0,  // Set Id to 0 for new entries
                Name = dto.Name,
                Address = dto.Address,
                Enabled = dto.Enabled,
                Inside = dto.Inside,
                CityId = dto.CityId,
                Phone = dto.Phone,
                Email = dto.Email,
                Year = yearConfiguration.Year,
                latitude = dto.Latitude,
                Longitude = dto.Longitude
            }).ToList();

            // Save hospitals to the database to get their IDs
            await _unitOfWork.HospitalRepository.Add_Hospital(hospitals);

            // إضافة SurgicalProcedures
            var surgicalProceduresEntities = annualSettingDTO.Surgicals.Select(dto => new SurgicalProcedures
            {
                Id = 0,  // Set Id to 0 for new entries
                Name = dto.Name,
                Pathological_specialization = dto.Pathological_specialization,
                Price = dto.Price,
                Year = annualSettingDTO.Year
            }).ToList();

            await _unitOfWork.SurgicalProceduresRepository.Add_SurgicalProceduers(surgicalProceduresEntities);

            // تجميع وإضافة جميع الملاحظات (Notes)
            var notes = new List<Note>();

            // ملاحظات AgeSegments
            notes.AddRange(annualSettingDTO.AgeSegments.Select(dto => new Note
            {
                Content = dto.NoteContent,
                AgeSegmentId = ageSegmentsEntities.FirstOrDefault(a => a.FromYear == dto.FromYear && a.ToYear == dto.ToYear)?.Id,  // Ensure correct ID mapping
                YearConfigId = yearConfiguration.Id
            }));

            // ملاحظات RelationTypes
            notes.AddRange(annualSettingDTO.RelationTypes.Select(dto => new Note
            {
                Content = dto.NoteContent,
                RelationId = relationTypes.FirstOrDefault(r => r.Name == dto.Name)?.Id,  // Ensure correct ID mapping
                YearConfigId = yearConfiguration.Id
            }));

            // ملاحظات Hospitals
            notes.AddRange(annualSettingDTO.Hospitals.Select(dto => new Note
            {
                Content = dto.NoteContent,
                HospitalId = hospitals.FirstOrDefault(h => h.Name == dto.Name)?.Id,  // Ensure correct ID mapping
                YearConfigId = yearConfiguration.Id
            }));

            // ملاحظات SurgicalProcedures
            notes.AddRange(annualSettingDTO.Surgicals.Select(dto => new Note
            {
                Content = dto.NoteContent,
                SurgicalProcedureId = surgicalProceduresEntities.FirstOrDefault(s => s.Name == dto.Name)?.Id,  // Ensure correct ID mapping
                YearConfigId = yearConfiguration.Id
            }));

            // إضافة جميع الملاحظات دفعة واحدة
            await _unitOfWork.NoteRepository.AddNotesAsyncList(notes);

            // إكمال المعاملة إذا تم كل شيء بنجاح
            scope.Complete();
        }
    }
    catch (TransactionAbortedException ex)
    {
        response.ErrorMessage = $"Transaction aborted: {ex.Message}";
        if (ex.InnerException != null)
        {
            response.ErrorMessage += $"\nInner Exception: {ex.InnerException.Message}";
        }
    }
    catch (Exception exx)
    {
        response.ErrorMessage = $"An error occurred: {exx.Message}";
        if (exx.InnerException != null)
        {
            response.ErrorMessage += $"\nInner Exception: {exx.InnerException.Message}";
        }
    }

    return response;
}






    public async Task<bool> DeleteAnnuaSetting(int year)
{
    bool completed = false;
    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {


            await _unitOfWork.NoteRepository.Delete_NotesByYearAsync(year);

            await _unitOfWork.AnnualDataRepository.Delete_Year_Configuration(year);

           await _unitOfWork.AgeSegmentsRepository.Delete_AgeSegmentsByYear(year);

            await _unitOfWork.RelationRepository.Delete_RelationTypesByYear(year);

            await _unitOfWork.HospitalRepository.Delete_HospitalsByYear(year);

            await _unitOfWork.SurgicalProceduresRepository.Delete_SurgicalProceduresByYear(year);


            scope.Complete();
            completed = true;
        }
    }
    catch (TransactionAbortedException ex)
    {
        // Handle transaction abort exception
        // Optionally log the exception
    }
    catch (Exception exx)
    {
        // Handle generic exception
        // Optionally log the exception
    }

    return completed;
}




/*

 public async Task<Response> UpdateAnnualSettings(AnnualSettingDTO annualSettingDTO)
{
    Response response = new Response();

    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // Step 1: Update Year Configuration
            YearConfiguration yearConfiguration = new YearConfiguration()
            {
                Id = annualSettingDTO.Id,  // Use the provided Id for update
                Year = annualSettingDTO.Year,
                CardPrice = annualSettingDTO.CardPrice
            };
            await _unitOfWork.AnnualDataRepository.Update_Year_Configuration(yearConfiguration);

            

            // Step 2: Delete Old AgeSegments and Add New AgeSegments
            await _unitOfWork.AgeSegmentsRepository.Delete_AgeSegmentsByYear(annualSettingDTO.Year);

            var ageSegmentsEntities = annualSettingDTO.AgeSegments.Select(dto => new AgeSegments
            {
                Id = 0,  // Set Id to 0 for new entries
                FromYear = dto.FromYear,
                ToYear = dto.ToYear,
                TheAmount = dto.TheAmount,
                EnduranceRatio = dto.EnduranceRatio,
                Year = annualSettingDTO.Year
            }).ToList();
            await _unitOfWork.AgeSegmentsRepository.Add_Age_Segments(ageSegmentsEntities);

            // Step 3: Delete Old RelationTypes and Add New RelationTypes
            await _unitOfWork.RelationRepository.Delete_RelationTypesByYear(annualSettingDTO.Year);

            var relationTypes = annualSettingDTO.RelationTypes.Select(dto => new RelationType
            {
                Id = 0,  // Set Id to 0 for new entries
                Name = dto.Name,
                Year = annualSettingDTO.Year,
                Relations = null  // Set Relations to null if not required
            }).ToList();
            await _unitOfWork.RelationRepository.Add_RelationType(relationTypes);

   
            // Step 6: Delete Old Notes and Add New Notes
            await _unitOfWork.NoteRepository.Delete_NotesByYearConfigId(yearConfiguration.Id);

            var notes = new List<Note>();

            // AgeSegments Notes
            notes.AddRange(annualSettingDTO.AgeSegments.Select(dto => new Note
            {
                Content = dto.NoteContent,
                AgeSegmentId = dto.Id,
                YearConfigId = yearConfiguration.Id
            }));

            // RelationTypes Notes
            notes.AddRange(annualSettingDTO.RelationTypes.Select(dto => new Note
            {
                Content = dto.NoteContent,
                RelationId = dto.Id,
                YearConfigId = yearConfiguration.Id
            }));

            // Hospitals Notes
            notes.AddRange(annualSettingDTO.Hospitals.Select(dto => new Note
            {
                Content = dto.NoteContent,
                HospitalId = dto.Id,
                YearConfigId = yearConfiguration.Id
            }));

            // SurgicalProcedures Notes
            notes.AddRange(annualSettingDTO.Surgicals.Select(dto => new Note
            {
                Content = dto.NoteContent,
                SurgicalProcedureId = dto.Id,
                YearConfigId = yearConfiguration.Id
            }));

            await _unitOfWork.NoteRepository.AddNotesAsyncList(notes);

            // Mark the transaction as complete
            scope.Complete();
            response.Status = "Success";
        }
    }
    catch (TransactionAbortedException ex)
    {
        response.ErrorMessage = $"Transaction aborted: {ex.Message}";
    }
    catch (Exception exx)
    {
        response.ErrorMessage = $"An error occurred: {exx.Message}";
    }

    return response;
}


*/



  public async Task<IEnumerable<AnnualDataWithDetails>> GetByYear(int year, int pageNumber, int pageSize){
    return await _unitOfWork.AnnualDataRepository.GetByYear(year,pageNumber , pageSize);
  }

   public async Task<(SimpleEngineer engineer, bool? cardStatus, int? payMethod)> GetEngineerDetailsAndCardStatus(string insuranceNumber, int year){
    return await _unitOfWork.AnnualDataRepository.GetEngineerDetailsAndCardStatus(insuranceNumber, year);
  }






  public async Task<AnnualDataDetail?> GetAnnualDataDetailAsync(string ensuranceNumber, int year){
    return await _unitOfWork.AnnualDataRepository.GetAnnualDataDetailAsync(ensuranceNumber,year);
  }



  public async Task<decimal> GetClaimsSumForPersonAsync(string ensuranceNumber){
    return await _unitOfWork.AnnualDataRepository.GetClaimsSumForPersonAsync(ensuranceNumber);
  }






   public async Task<Response> CopyAnnualDataForNewYear(
    int previousYear, 
    int newYear, 
    string ensuranceNumber, 
    bool waiting, 
    bool cardStatus, 
    bool copyAnnualData, 
    bool copyAnnualDataDetails,
    List<AnnualNewDTO> detailIdsToCopy // استخدام DTO هنا
)
{
    Response response = new Response();
    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // الحصول على المهندس بناءً على الرقم التأميني
            var engineer = await _unitOfWork.AnnualDataRepository.GetByInsuranceNumber(ensuranceNumber);
            if (engineer == null)
            {
                response.ErrorMessage = "Engineer not found.";
                return response;
            }

            // الحصول على البيانات السنوية السابقة
            var previousAnnualData = await _unitOfWork.AnnualDataRepository.GetByEngineerIdAndYear(engineer.Id, previousYear);
            if (previousAnnualData == null)
            {
                response.ErrorMessage = "Previous annual data not found.";
                return response;
            }

            int newAnnualDataId = 0;

            // Check if a claim exists for the engineer in the previous year to determine Beneficiary status
            bool isBeneficiary = await _unitOfWork.ClimsRepository.CheckClaimExistsAsync(engineer.Id, previousYear);

            // نسخ البيانات السنوية إذا تم تحديد نسخها
            if (copyAnnualData)
            {
                AnnualData newAnnualData = new AnnualData
                {
                    Year = newYear,
                    ExAmount = previousAnnualData.ExAmount,
                    HisDic = previousAnnualData.HisDic,
                    CardStatuse = cardStatus, // استخدام المتغير cardStatus هنا
                    Subscrib = previousAnnualData.Subscrib,
                    Affiliate = previousAnnualData.Affiliate,
                    Beneficiary = isBeneficiary, // استخدام نتيجة الفحص من CheckClaimExistsAsync
                    Waiting = waiting, // استخدام المتغير waiting هنا
                    EngineereId = previousAnnualData.EngineereId,
                    WorkPlaceId = previousAnnualData.WorkPlaceId,
                    PayMethodId = previousAnnualData.PayMethodId,
                    Amount = previousAnnualData.Amount,
                    TotalAmount = previousAnnualData.TotalAmount
                };

                newAnnualDataId = await _unitOfWork.AnnualDataRepository.Add_AnnualData(newAnnualData);
                response.InsertedId = newAnnualDataId;
            }

            // نسخ تفاصيل البيانات السنوية إذا تم تحديد نسخها
            if (detailIdsToCopy != null && copyAnnualDataDetails)
            {
                var previousAnnualDataDetails = (await _unitOfWork.AnnualDataRepository
                    .GetAnnualDataDetails(previousAnnualData.Id))
                    .Select(detail => new 
                    {
                        Id = (int)detail.Id,  // تأكد أن النمط يطابق النمط المطلوب
                        detail.PersonId,
                        detail.Year,
                        detail.ExAmount,
                        detail.Subscrib,
                        detail.Affiliate,
                        detail.Beneficiary,
                        detail.Waiting,
                        detail.Amount,
                        detail.IsEngineer
                    }).ToList();

                // نسخ التفاصيل بناءً على detailIdsToCopy وتحديث Waiting و CardStatuse
                foreach (var detail in previousAnnualDataDetails)
                {
                    var matchingDetail = detailIdsToCopy.FirstOrDefault(d => d.Id == detail.Id);
                    if (matchingDetail != null)
                    {
                        // Check claim for the current detail's person and year
                        bool isDetailBeneficiary = await _unitOfWork.ClimsRepository.CheckClaimExistsAsync(detail.PersonId, previousYear);

                        AnnualDataDetail newDetail = new AnnualDataDetail
                        {
                            AnnualDataId = newAnnualDataId,
                            PersonId = detail.PersonId,
                            Year = newYear,
                            CardStatuse = matchingDetail.CardStatuse, // استخدام CardStatuse من الـ DTO
                            ExAmount = detail.ExAmount,
                            Subscrib = detail.Subscrib,
                            Affiliate = detail.Affiliate,
                            Beneficiary = isDetailBeneficiary, // استخدام نتيجة الفحص من CheckClaimExistsAsync للتفاصيل
                            Waiting = matchingDetail.Waiting, // استخدام Waiting من الـ DTO
                            Amount = detail.Amount,
                            IsEngineer = detail.IsEngineer
                        };

                        await _unitOfWork.AnnualDataRepository.Add_AnnualDataDetail(newDetail);
                    }
                }
            }

            scope.Complete();
        }
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




  public async Task<AnnualData> GetByEngineerIdAndYear(int engineerId, int year){
    return await _unitOfWork.AnnualDataRepository.GetByEngineerIdAndYear(engineerId,year);
  }


   public async Task<List<AnnualDataDetail>> GetAnnualDataDetails(int annualDataId){
    return await _unitOfWork.AnnualDataRepository.GetAnnualDataDetails(annualDataId);
   }




       public async Task<PayMethod> GetPayMethodByEngineerIdAsync(int engineerId)
    {
        return await _unitOfWork.AnnualDataRepository.GetPayMethodByEngineerId(engineerId);
    }



     public async Task UpdateYearConfigurationAsync(YearConfiguration yearConfiguration)
    {
        await _unitOfWork.AnnualDataRepository.Update_Year_Configuration(yearConfiguration);
    }



      public async Task AddNoteToYearConfigAsync(int yearConfigId, NoteCreateDTO noteDto)
    {
        var yearConfig = await _unitOfWork.NoteRepository.GetYearConfigurationByIdAsync(yearConfigId);
        if (yearConfig == null)
        {
            throw new Exception("Year Configuration not found.");
        }

        var note = new Note
        {
            Content = noteDto.Content,
           
            YearConfiguration = yearConfig
        };

        await _unitOfWork.NoteRepository.AddNoteAsync(note);
    }
}


    }
    
    
    
