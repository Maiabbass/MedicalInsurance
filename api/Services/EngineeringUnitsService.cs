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
          
            Number = engineeringUnitsEditDTO.Number,
             
            
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

        public Task<IEnumerable<EngineeringUnits>> GetAll()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<EngineeringUnits>> IEngineeringUnitsService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
} 
  