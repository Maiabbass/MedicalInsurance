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
    public class WorkPlaceService : IWorkplaceService
    {
        private  readonly IUnitOfWork _unitOfWork;

      

        public WorkPlaceService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }
        public async Task<Response> Add(WorkplaceEditDTO workplaceEditDTO)
        {

            Response response =new Response ();
             int insertedId=0; 
              try 
               {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                {
                       
                        WorkPlace workplace =new WorkPlace()
           {
          
             Name = workplaceEditDTO.Name,
             Location=workplaceEditDTO.Location,
             EngineeringUnitsId=workplaceEditDTO.EngineeringUnitsId ,
             
            
           };

           
         insertedId=  await _unitOfWork.WorkplaceRepository.Add(workplace);

             

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

        public Task<IEnumerable<WorkPlace>> GetAll()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<WorkPlace>> IWorkplaceService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
} 
