using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.Data;
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
             Phone=workplaceEditDTO.Phone,
             
            
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

        public async Task<IEnumerable<WorkPlace>> GetAll()
        {
          return await _unitOfWork.WorkplaceRepository.GetAll();
        }



          public async Task<WorkPlace?> Get(int Id)
        {
              return await _unitOfWork.WorkplaceRepository.Get(Id);
        }

          public bool Delete(int Id){
      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
        _unitOfWork.WorkplaceRepository.DeleteByWorkPlaceId(Id);
      
        _unitOfWork.WorkplaceRepository.Delete(Id);
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException){
                  return false;
                 }}




             public bool Update(int Id, WorkplaceEditDTO workplaceEditDTO) {
             return _unitOfWork.WorkplaceRepository.Update(Id,workplaceEditDTO );
             }   

    }
} 
