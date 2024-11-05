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



      public async Task<Response> Add(PersonEditDTO personEditDTO, IFormFile[] imageFiles, IFormFile[] wordFiles, int year)
{
    Response response = new Response();
    int insertedId = 0;

    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // إعداد بيانات الشخص
            Person person = new Person()
            {
                FirstName = personEditDTO.FirstName,
                FatherName = personEditDTO.FatherName,
                MotherName = personEditDTO.MotherName,
                LastName = personEditDTO.LastName,
                BirthDate = personEditDTO.BirthDate,
                NationalId = personEditDTO.NationalId,
                EnsuranceNumber = personEditDTO.EnsuranceNumber,
                Address = personEditDTO.Address,
                Phone = personEditDTO.Phone,
                Mobile = personEditDTO.Mobile,
                Email = personEditDTO.Email,
                GenderId = personEditDTO.GenderId,
                StatusId = personEditDTO.StatusId,
            };

            // تمرير السنة إلى دالة AddPerson
            insertedId = await _unitOfWork.PersonRepository.AddPerson(person, imageFiles, wordFiles, year);

            // إضافة بيانات العلاقة
            Relation relation = new Relation()
            {
                Name = "",
                PersonId = insertedId,
                EngineereId = personEditDTO.EngineereId,
                RelationTypeId = personEditDTO.RelationTypeId
            };

            await _unitOfWork.RelationRepository.Add(relation);

            scope.Complete();
        }
    }
    catch (TransactionAbortedException ex)
    {
        response.ErrorMessage = ex.Message;
    }
    catch (Exception exx)
    {
        response.ErrorMessage = exx.Message;
    }

    response.InsertedId = insertedId;
    return response;
}






       public async Task<PagedResult<PersonForView>> GetAll(int pageNumber, int pageSize)
{
    return await _unitOfWork.PersonRepository.GetAll(pageNumber, pageSize);
}




     public async Task<Person?>GetWithId(int Id)
     {
      return await _unitOfWork.PersonRepository.Get(Id);
     }



    public async Task<bool> DeleteAsync(int Id){
    try {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) {
            await _unitOfWork.AnnualDataRepository.DeleteByPersonIdAsync(Id);
            await _unitOfWork.RelationRepository.DeleteByPersonIdAsync(Id);
            await _unitOfWork.NoteRepository.DeleteNotesByPersonIdAsync(Id);
            await _unitOfWork.PersonRepository.DeleteAsync(Id);

            scope.Complete();
            return true;
        }
    } catch (TransactionAbortedException) {
        return false;
    }
}


     public async Task<bool> UpdatePersonDetails(int id, PersonEditDTO personEditDTO, int year)
    {
        return await _unitOfWork.PersonRepository.UpdatePersonDetails(id, personEditDTO, year);
    }


        public async Task<AnnualData?> GetEngId(int EngineereId){
          return await _unitOfWork.PersonRepository.GetEngId(EngineereId);

        }

        public async Task<bool> IsEnsuranceNumberInClaimsAsync(string ensuranceNumber){
         return await _unitOfWork.PersonRepository.IsEnsuranceNumberInClaimsAsync(ensuranceNumber);

        }

       

     

        
    }
}