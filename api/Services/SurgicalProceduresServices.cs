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
    public class SurgicalProceduresServices : ISurgicalProceduresServices
    {
        private  readonly IUnitOfWork _unitOfWork;

        public SurgicalProceduresServices(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }
        public async Task<Response> Add(SurgicalProceduresEditDTO SurgicalProceduresEditDTO)
        {

            Response response =new Response ();
             int insertedId=0; 
              try 
               {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                {
                        // add city data 
                        SurgicalProcedures newSur =new SurgicalProcedures()
           {
          
             Name = SurgicalProceduresEditDTO.Name,
             Type=SurgicalProceduresEditDTO.Type,
             
            
           };

           
         insertedId=  await _unitOfWork.SurgicalProceduresRepository.Add(newSur);

             

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

        

     
        

        
         public async Task<SurgicalProcedures?>Get(int Id)
     {
      return await _unitOfWork.SurgicalProceduresRepository.Get(Id);
     }

     public async Task<IEnumerable<SurgicalProcedures>>GetAll()
        {
            return  await _unitOfWork.SurgicalProceduresRepository.GetAll() ;
        }



        public bool Update(int id, SurgicalProceduresEditDTO surgicalProceduresEditDTO)
        {
             return _unitOfWork.SurgicalProceduresRepository.Update(id, surgicalProceduresEditDTO);
        }

         public bool Delete(int Id){
      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
       
      
        _unitOfWork.SurgicalProceduresRepository.Delete(Id);
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException){
                  return false;
                 }}
    }
}