using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.DTOS;
using api.Entities;
using api.Repositories;
using static api.DTOS.PersonWithEngineereDTO;

namespace api.Services
{
    public class EngineerService : IEngineerService
    {


         private readonly IUnitOfWork _unitOfWork;

         private readonly IAnnualDataService _annualDataService;

         public EngineerService(IUnitOfWork unitOfWork , IAnnualDataService annualDataService)
         {
            _unitOfWork=unitOfWork;
            _annualDataService=annualDataService;
         }


     public async Task<Response> Add(EngineerPersonEditDTO engineerPersonEditDTO, IFormFile[] contentImage, IFormFile[] contentFile, int year)
{
    Response response = new Response();
    int insertedId = 0;

    try
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            // إضافة بيانات الشخص
            Person person = new Person()
            {
                FirstName = engineerPersonEditDTO.FirstName,
                FatherName = engineerPersonEditDTO.FatherName,
                MotherName = engineerPersonEditDTO.MotherName,
                LastName = engineerPersonEditDTO.LastName,
                BirthDate = engineerPersonEditDTO.BirthDate,
                NationalId = engineerPersonEditDTO.NationalId,
                EnsuranceNumber = engineerPersonEditDTO.EnsuranceNumber,
                Address = engineerPersonEditDTO.Address,
                Phone = engineerPersonEditDTO.Phone,
                Mobile = engineerPersonEditDTO.Mobile,
                Email = engineerPersonEditDTO.Email,
                GenderId = engineerPersonEditDTO.GenderId,
                StatusId = engineerPersonEditDTO.statusId,
                Amount = _annualDataService.calcualteAmount(engineerPersonEditDTO.BirthDate, year)
            };

            insertedId = await _unitOfWork.PersonRepository.AddPerson(person, contentImage, contentFile,year);

            // إضافة بيانات المهندس
            Engineere engineer = new Engineere()
            {
                EngNumber = engineerPersonEditDTO.EngNumber,
                SubNumber = engineerPersonEditDTO.SubNumber,
                Id = insertedId,
                SpecializationId = engineerPersonEditDTO.SpecializationId,
                WorkPlaceId = engineerPersonEditDTO.WorkPlaceId
            };

            await _unitOfWork.EngineerRepository.Add(engineer);

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




     public async Task<PersonWithEngineereDTO?> Get(int Id)
{
    return await _unitOfWork.EngineerRepository.Get(Id);
}


public async Task<PagedResult<PersonWithEngineereDTO>> GetAll(int pageNumber, int pageSize)
{
    return await _unitOfWork.EngineerRepository.GetAll(pageNumber, pageSize);
}

        

           public bool Update(int id, EngineerPersonEditDTO engineerPersonEditDTO, int year){
           return _unitOfWork.EngineerRepository.Update(id, engineerPersonEditDTO , year);
        }
        

       public async Task<bool> DeleteAsync(int Id) {
    try {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) {
            await _unitOfWork.EngineerRepository.DeleteByEngIdAsync(Id);
            await _unitOfWork.EngineerRepository.DeleteByEngId2Async(Id);
           // await _unitOfWork.NoteRepository.DeleteNotesByPersonIdAsync(Id);
            await _unitOfWork.EngineerRepository.DeleteAsync(Id);

            scope.Complete();
            return true;
        }
    }
    catch (TransactionAbortedException) {
        return false;
    }
}

    }
}
