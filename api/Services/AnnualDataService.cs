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


        public AnnualDataService(IUnitOfWork unitOfWork)
         {
            _unitOfWork = unitOfWork;
         }

        public async Task<Response> Add(RegisterAnnualDataDTO registerAnnualDataDTO)
        {
             Response response =new Response ();
             try
             {
                 using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                 {
                    
                    

                   var  ageSegments= await _unitOfWork.AgeSegmentsRepository.Get(registerAnnualDataDTO.Year);
                    
                    
                    // engineer...
                   
                    AnnualDataDetail engineer_annualDataDetail=new  AnnualDataDetail();
                    if (registerAnnualDataDTO.EngineerId>0&&registerAnnualDataDTO.EngineerIsRegistered)
                    {
                         engineer_annualDataDetail=new  AnnualDataDetail()
                    {
                        IsEngineer=true,
                        PersonId= registerAnnualDataDTO.EngineerId,
                        Amount=0m,
                        
                    };

                       Person? engineere= await _unitOfWork.PersonRepository.Get(registerAnnualDataDTO.EngineerId);
                       if (engineere!=null)
                       {
                            var engineerAge=  engineere.BirthDate.GetAge();
                            var segment= ageSegments.Where(x=>engineerAge>=x.FromYear&&engineerAge<=x.ToYear).FirstOrDefault();
                            if (segment!=null)
                            {
                                engineer_annualDataDetail.Amount = segment.TheAmount;
                            }
                       }
                    }


                    List<AnnualDataDetail> persons_AnnualDataDetailList=new  List<AnnualDataDetail>();
                    // persons
                     foreach(var personid  in registerAnnualDataDTO.PersonsIds)
                     {
                       AnnualDataDetail annualDataDetail_person=new  AnnualDataDetail()
                    {
                        IsEngineer=false,
                        PersonId= personid,
                        Amount=0m
                    };

                       Person? person= await _unitOfWork.PersonRepository.Get(personid);
                       if (person!=null)
                       {
                            var personAge=  person.BirthDate.GetAge();
                            var segment= ageSegments.Where(x=>personAge>=x.FromYear&&personAge<=x.ToYear).FirstOrDefault();
                            if (segment!=null)
                            {
                                annualDataDetail_person.Amount = segment.TheAmount;
                            }
                       }
                       persons_AnnualDataDetailList.Add(annualDataDetail_person);
                     }



                     // prepare AnnualData Object ...
                     AnnualData annualData =new AnnualData ();
                     annualData.Year = registerAnnualDataDTO.Year;
                     annualData.ExAmount = registerAnnualDataDTO.ExAmount;
                      // get required engineer info ...
                     var engineer= await _unitOfWork.EngineerRepository.Get(registerAnnualDataDTO.EngineerId);
                     if (engineer!=null)
                     {
                        annualData.EngineereId = engineer.Id;
                        annualData.EngineeringUnitsId = null;// TO DO : fill later...
                        annualData.WorkPlaceId = engineer.WorkPlaceId;
                         annualData.PayMethodId = registerAnnualDataDTO.PayMethodId; 
                     }
                     // amount...
                     if (engineer_annualDataDetail.PersonId>0)
                     {
                        annualData.Amount+=engineer_annualDataDetail.Amount;
                     }
                     decimal persons_amount= persons_AnnualDataDetailList.Sum(x=>x.Amount);
                     annualData.Amount+=persons_amount;
                     annualData.TotalAmount = annualData.Amount+annualData.ExAmount;
                   
                     
                     int insertedAnnualDataId= await _unitOfWork.AnnualDataRepository.Add_AnnualData(annualData);
                     response.InsertedId = insertedAnnualDataId;
                     // insert details ...
                     
                     // insert engineer detail
                      engineer_annualDataDetail.AnnualDataId = insertedAnnualDataId;
                      await _unitOfWork.AnnualDataRepository.Add_AnnualDataDetail(engineer_annualDataDetail);

                     // insert persons details 
                      foreach(var item in persons_AnnualDataDetailList)
                      {
                        item.AnnualDataId = insertedAnnualDataId;
                        await _unitOfWork.AnnualDataRepository.Add_AnnualDataDetail(item);
                      }



                    scope.Complete();
                 } // using

             }
              catch(TransactionAbortedException ex)
            {

                response.ErrorMessage = ex.Message;
                 
            }
             return response;

           
        }
         

        Task<Response> IAnnualDataService.Add(RegisterAnnualDataDTO registerAnnualDataDTO)
        {
            throw new NotImplementedException();
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
     public bool Update(int Id,Dictionary<string, object> updateFields){
           return _unitOfWork.AnnualDataRepository.Update(Id, updateFields);
        }

        public async Task<IEnumerable<AnnualDataWithDetails>> GetAll(){
            return await _unitOfWork.AnnualDataRepository.GetAll();
     
    }
     public bool Update(int Id,decimal Amount){
           return _unitOfWork.AnnualDataRepository.Update(Id, Amount);
        }
    


    }}
