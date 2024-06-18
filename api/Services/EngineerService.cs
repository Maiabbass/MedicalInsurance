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
    public class EngineerService : IEngineerService
    {


         private readonly IUnitOfWork _unitOfWork;

         public EngineerService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }
        public async Task<Response> Add(EngineerPersonEditDTO engineerPersonEditDTO)
        {

            Response response =new Response ();
             int insertedId=0; 
              try 
               {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                {
                        // add person data 
                        Person person =new Person()
           {
          
             FirstName = engineerPersonEditDTO.FirstName,
             FatherName =engineerPersonEditDTO.FatherName,
             MotherName =engineerPersonEditDTO.MotherName,
             LastName = engineerPersonEditDTO.LastName,
             BirthDate = engineerPersonEditDTO.BirthDate,
             NationalId = engineerPersonEditDTO.NationalId,
             EnsuranceNumber = engineerPersonEditDTO.EnsuranceNumber,
             Address = engineerPersonEditDTO.Address,
             Phone = engineerPersonEditDTO.Phone,
             Mobile=engineerPersonEditDTO.Mobile,
             Email=engineerPersonEditDTO.Email,
             Subscrib=engineerPersonEditDTO.Subscrib,
             Affiliate=engineerPersonEditDTO.Affiliate,
             Beneficiary=engineerPersonEditDTO.Beneficiary,
             GenderId = engineerPersonEditDTO.GenderId,
             statusId = engineerPersonEditDTO.statusId,
           };

           
             insertedId=  await _unitOfWork.PersonRepository.Add(person);

             // add engineer data 

               Engineere engineere =new Engineere ()
               {
                
                EngNumber = engineerPersonEditDTO.EngNumber,
                SubNumber = engineerPersonEditDTO.SubNumber,
                Id = insertedId,
                
                SpecializationId = engineerPersonEditDTO.SpecializationId,
                WorkPlaceId =engineerPersonEditDTO.WorkPlaceId
               
                
               };

               await _unitOfWork.EngineerRepository.Add(engineere);

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

        public async Task<Engineere?>Get(int Id)
     {
      return await _unitOfWork.EngineerRepository.Get(Id);
     }

     public async Task<IEnumerable<Engineere>>GetAll()
        {
            return  await _unitOfWork.EngineerRepository.GetAll() ;
        }

           public bool Update(int Id, EngineerPersonEditDTO engineerPersonEditDTO){
           return _unitOfWork.EngineerRepository.Update(Id, engineerPersonEditDTO);
        }

         public bool Delete(int Id){
      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
        
      
        _unitOfWork.EngineerRepository.Delete(Id);
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException){
                  return false;
                 }}
    }
}