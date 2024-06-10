using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.DTOS;
using api.Entities;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Services
{

    
    public class PersonService : IPersonService
    {

        
        private readonly IUnitOfWork _unitOfWork;

         public PersonService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }

        public object Person => throw new NotImplementedException();

        public async Task<Response> Add(PersonEditDTO personEditDTO)
        {
            Response response =new Response ();
              int insertedId=0; 
                 

                 try 
                 {
                using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
                 {
                            // add person 
                              Person person =new Person ()
           {
          
             FirstName = personEditDTO.FirstName,
             FatherName =personEditDTO.FatherName,
             MotherName =personEditDTO.MotherName,
             LastName = personEditDTO.LastName,
             BirthDate = personEditDTO.BirthDate,
             NationalId = personEditDTO.NationalId,
             EnsuranceNumber = personEditDTO.EnsuranceNumber,
             Address = personEditDTO.Address,
             Phone = personEditDTO.Phone,
             Mobile=personEditDTO.Mobile,
             Email=personEditDTO.Email,
             Subscrib=personEditDTO.Subscrib,
             Affiliate=personEditDTO.Affiliate,
             Beneficiary=personEditDTO.Beneficiary,
             GenderId = personEditDTO.GenderId
                 };

                  insertedId=  await _unitOfWork.PersonRepository.Add(person);

                  // add relation data ...
                  Relation relation =new Relation()
                  {
                    Name="",
                    PersonId = insertedId,
                    EngineereId = personEditDTO.EngineereId,
                    RelationTypeId  =personEditDTO.RelationTypeId
                  };

                    await _unitOfWork.RelationRepository.Add(relation);

                    scope.Complete();
                    

                 }
                 }
                   catch(TransactionAbortedException ex)
                {

                   response.ErrorMessage = ex.Message;
                 
                 }
                response.InsertedId  =insertedId;
             return response;
        }

        public async Task<IEnumerable<Person>>  GetAll()
        {
            return  await _unitOfWork.PersonRepository.GetAll() ;
        }

     public async Task<Person?>GetWithId(int Id)
     {
      return await _unitOfWork.PersonRepository.Get(Id);
     }



     public bool Delete(int Id){

      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
            _unitOfWork.AnnualDataRepository.DeleteByPersonId(Id);
           _unitOfWork.RelationRepository.DeleteByPersonId(Id);
      
        _unitOfWork.PersonRepository.Delete(Id);
            scope.Complete();
            return true;
         }
      } 
          catch (TransactionAbortedException)
            {

                  
                  return false;
                 }
    
     }
      public bool Update(int Id, PersonEditDTO personEditDTO){
           return _unitOfWork.PersonRepository.Update(Id, personEditDTO);
        }

        public async Task<AnnualData?> GetEngId(int EngineereId){
          return await _unitOfWork.PersonRepository.GetEngId(EngineereId);

        }

        public async Task<bool> IsEnsuranceNumberInClaimsAsync(string ensuranceNumber){
         return await _unitOfWork.PersonRepository.IsEnsuranceNumberInClaimsAsync(ensuranceNumber);

        }

        
    }
}