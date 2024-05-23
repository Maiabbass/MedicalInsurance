using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.DTOS;
using api.Entities;
using api.Repositories;

namespace api.Services
{
    public class EngineeringUnitsService : IEngineeringUnitsService
    {
        private  readonly IUnitOfWork _unitOfWork;

        public EngineeringUnitsService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }
        public async Task<Response> Add(EngineeringUnitsEditDTO engineeringUnitsEditDTO)
        {

            Response response =new Response ();
             int insertedId=0; 
              try 
               {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                {
                         
                        EngineeringUnits engineeringUnits =new EngineeringUnits()
           {
          
            Name = engineeringUnitsEditDTO.Name,
             
            
           };

           
         insertedId=  await _unitOfWork.EngineeringUnitsRepository.Add(engineeringUnits);

             

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

        public async Task<IEnumerable<EngineeringUnits>> GetAll()
        {
            return await _unitOfWork.EngineeringUnitsRepository.GetAll();
        }

        public  async Task<EngineeringUnits?> Get(int Id){
            return await _unitOfWork.EngineeringUnitsRepository.Get(Id);
        }


        public bool Delete(int Id){
      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
            
          
        _unitOfWork.EngineeringUnitsRepository.DeleteByEngineeringUnitsId(Id);
        _unitOfWork.EngineeringUnitsRepository.DeleteByEngineeringUnitsId2(Id);
      
        _unitOfWork.EngineeringUnitsRepository.Delete(Id);
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException){
                  return false;
                 }}


        public bool Update(int Id, string Name){
             return _unitOfWork.EngineeringUnitsRepository.Update(Id, Name);


        }
    }
} 
  