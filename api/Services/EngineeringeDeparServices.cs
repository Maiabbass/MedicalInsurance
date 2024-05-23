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
    public class EngineeringeDeparServices : IEngineeringeDeparServices
    {
        private  readonly IUnitOfWork _unitOfWork;

        public EngineeringeDeparServices(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }
        public async Task<Response> Add(EngineeringeDeparEditDTO EngDepae)
        {

            Response response =new Response ();
             int insertedId=0; 
              try 
               {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                {
                        // add city data 
                        EngineeringeDepar newEngDepa =new EngineeringeDepar()
           {
          
             Name = EngDepae.Name,
             SpecializationId=EngDepae.SpecializationId,
             
            
           };

           
         insertedId=  await _unitOfWork.EngineeringeDeparRepositry.Add(newEngDepa);

             

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

        

     
        public bool Update(int Id, EngineeringeDeparEditDTO Eng){
           return _unitOfWork.EngineeringeDeparRepositry.Update(Id, Eng);
        }

        
         public async Task<EngineeringeDepar?>Get(int Id)
     {
      return await _unitOfWork.EngineeringeDeparRepositry.Get(Id);
     }

     public async Task<IEnumerable<EngineeringeDepar>>GetAll()
        {
            return  await _unitOfWork.EngineeringeDeparRepositry.GetAll() ;
        }

        public bool Update(int Id, EngineeringeDepar EngDepa)
        {
            throw new NotImplementedException();
        }
       public bool Delete(int Id){
      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
        _unitOfWork.EngineeringeDeparRepositry.Delete(Id);
      
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException){
                  return false;
                 }}
    }
}