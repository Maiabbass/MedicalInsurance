using System;
using System.Threading.Tasks;
using System.Transactions;
using api.DTOS;
using api.Entities;
using api.Repositories;
using api.Helpers;

namespace api.Services
{
    public class RecoveredServices : IRecoveredServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecoveredServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Add(RecoveredDto recoveredDto)
        {

            Response response =new Response ();
             int insertedId=0; 
              try 
               {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                {
                       
                        Recovered newRE =new Recovered()
           {
          
            EnsuranceNumber=recoveredDto.EnsuranceNumber,
            FullName=recoveredDto.FullName,
            TotalPrice=recoveredDto.TotalPrice,
            Company_fees=recoveredDto.Company_fees,
            ApprovedPrice=recoveredDto.ApprovedPrice,
            non_Add=recoveredDto.non_Add,
            non_AddForPerson=recoveredDto.non_AddForPerson,
            EnduranceRatio=recoveredDto.EnduranceRatio,
            Status=recoveredDto.Status,
            SurgicalProceduresId=recoveredDto.SurgicalProceduresId,
            PersonId=recoveredDto.PersonId,
            HospitalId=recoveredDto.HospitalId,
            LoginDate=recoveredDto.LoginDate,
            ExitDate=recoveredDto.ExitDate,
            RecoDate=recoveredDto.RecoDate,
            Number=recoveredDto.Number,
            DateSurgicalProcedures=recoveredDto.DateSurgicalProcedures,
            NameHospital_Out=recoveredDto.NameHospital_Out,
            NumberBOK=recoveredDto.NumberBOK,
            Phone=recoveredDto.Phone,

            
          
           };

           
         insertedId=  await _unitOfWork.RecoveredRepository.Add(newRE);

             

                scope.Complete();
                } // using 
               } // try 
              catch(TransactionAbortedException ex)
            {

                response.ErrorMessage = ex.Message;
                 
            }

            response.InsertedId  =insertedId;
             return response;
            
        }

         public bool Update(int Id, RecoveredDto reco){
           return _unitOfWork.RecoveredRepository.Update(Id, reco);
        }

        
         public async Task<Recovered?>Get(int Id)
     {
      return await _unitOfWork.RecoveredRepository.Get(Id);
     }

     public async Task<RecoveredSummary> GetAll()
        {
            return  await _unitOfWork.RecoveredRepository.GetAll() ;
        }




        public bool Delete(int Id){
      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
        
      
        _unitOfWork.RecoveredRepository.Delete(Id);
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException){
                  return false;
                 }}



                  public async Task<List<RecoveredDto>> GetByEnsuranceNumber(string ensuranceNumber){
                    return await _unitOfWork.RecoveredRepository.GetByEnsuranceNumber(ensuranceNumber);
                  }



                  public async Task<List<Recovered>> GetRecoveredBetweenDatesAsync(DateTime startDate, DateTime endDate){
                    return await _unitOfWork.RecoveredRepository.GetRecoveredBetweenDatesAsync(startDate,endDate);
                  }
    }
}