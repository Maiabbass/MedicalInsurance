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

         public EngineerService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         }
       public async Task<Response> Add(EngineerPersonEditDTO engineerPersonEditDTO, IFormFile[] contentImage, IFormFile[] contentFile)
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
                StatusId = engineerPersonEditDTO.statusId
            };

            insertedId = await _unitOfWork.PersonRepository.AddPerson(person,contentImage, contentFile);
/*
            // إضافة بيانات الصورة (Image)
            if (contentImage != null && contentImage.Length > 0)
            {
                Images image = new Images()
                {
                    PersonId = insertedId,
                    Image = await _unitOfWork.ImageRepository.ConvertImageToByteArrayAsync(contentImage)
                };

               // await _unitOfWork.ImageRepository.AddImageAsync(image);
            }

            // إضافة بيانات الملف (Word)
            if (contentFile != null && contentFile.Length > 0)
            {
                Words word = new Words()
                {
                    PersonId = insertedId,
                    Content = await _unitOfWork.WordRepository.ConvertFileToByteArrayAsync(contentFile)
                };

               // await _unitOfWork.WordRepository.AddWordAsync(word);
            }
*/
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

        

           public bool Update(int Id, EngineerPersonEditDTO engineerPersonEditDTO){
           return _unitOfWork.EngineerRepository.Update(Id, engineerPersonEditDTO);
        }
        

         public bool Delete(int Id){
      try
      {
         using(TransactionScope scope=new TransactionScope (TransactionScopeAsyncFlowOption.Enabled))
         {
        
         _unitOfWork.EngineerRepository.DeleteByEngId(Id);
         _unitOfWork.EngineerRepository.DeleteByEngId2(Id);
         
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
