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
                        Person person =new Person ()
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
             Subscrib=engineerPersonEditDTO.Subscrib,
             Affiliate=engineerPersonEditDTO.Affiliate,
             Beneficiary=engineerPersonEditDTO.Beneficiary,
             GenderId = engineerPersonEditDTO.GenderId
           };

           
             insertedId=  await _unitOfWork.PersonRepository.Add(person);

             // add engineer data 

               Engineere engineere =new Engineere ()
               {
                
                EngNumber = engineerPersonEditDTO.EngNumber,
                SubNumber = engineerPersonEditDTO.SubNumber,
                Id = insertedId,
                statusId = engineerPersonEditDTO.statusId,
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

    
          

    }
}