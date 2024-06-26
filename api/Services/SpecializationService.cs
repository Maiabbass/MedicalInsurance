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
    public class SpecializationService : ISpecializationService
    {
         private  readonly IUnitOfWork _unitOfWork;

        public SpecializationService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }

          public async Task<Response> Add(SpecializationEditDto specializationEditDto)
        {

            Response response =new Response ();
             int insertedId=0; 
              try 
               {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                {
                       
                        Specialization newsp =new Specialization()
           {
          
             Name = specializationEditDto.Name,
             EngineeringeDeparId=specializationEditDto.EngineeringeDeparId,
          
           };

           
         insertedId=  await _unitOfWork.SpecializationRepository.Add(newsp);

             

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

         public async Task<IEnumerable<Specialization>>  GetAll()
        {
            return  await _unitOfWork.SpecializationRepository.GetAll() ;
        }

     public async Task<Specialization?>GetWithId(int Id)
     {
      return await _unitOfWork.SpecializationRepository.Get(Id);
     }

     public bool Delete(int Id){

      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
      
        _unitOfWork.SpecializationRepository.Delete(Id);
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException)
            {

                  
                  return false;
                 }
                 
    
     }

     public bool Update(int id, SpecializationEditDto specializationEditDto)
        {
             return _unitOfWork.SpecializationRepository.Update(id, specializationEditDto);
        }

        
    }
}