using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;
using api.Repositories;

namespace api.Services
{
    public class SearchService : ISearchService
    {
        private  readonly IUnitOfWork _unitOfWork;

        public SearchService(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         } 



         public async Task<PersonWithEngineereDTO> GetByEnsuranceNumberAsync(string ensuranceNumber){
            return await _unitOfWork.SearchRepository.GetByEnsuranceNumberAsync(ensuranceNumber);
         }


         public async Task<IEnumerable<PersonWithEngineereDTO>> GetWithNameAsync(string userSearch){
            return await _unitOfWork.SearchRepository.GetWithNameAsync(userSearch) ;
         }


         
         

            public async Task<PersonWithEngineereDTO> GetByNationalIdAsync(string nationalId)
    {
        if (string.IsNullOrWhiteSpace(nationalId))
        {
            throw new ArgumentException("NationalId cannot be null or empty.", nameof(nationalId));
        }

        return await _unitOfWork.SearchRepository.GetByNationalIdAsync(nationalId);
    }

        public async Task<PersonWithEngineereDTO> GetEngNumberAsync(string engNumber){
            return await _unitOfWork.SearchRepository.GetEngNumberAsync(engNumber);
        }

         public async Task<IEnumerable<EngineeringUnits>> GetEngUnits(string name){
            return await _unitOfWork.SearchRepository.GetEngUnits(name);
         }

         public async Task<IEnumerable<WorkPlace>> GetWorkPlace(string name){
            return await _unitOfWork.SearchRepository.GetWorkPlace(name);
         }

         public async Task<IEnumerable<Hospital>> GetHospital(string name){
            return await _unitOfWork.SearchRepository.GetHospital(name) ;
         }

         public async Task<IEnumerable<Claims>> GetClaim(string ensuranceNumber){
            return await _unitOfWork.SearchRepository.GetClaim(ensuranceNumber);
         }

         public async Task<IEnumerable<Claims>> GetClaimsByDateRange(DateTime startDate, DateTime endDate) {

            return await _unitOfWork.SearchRepository.GetClaimsByDateRange(startDate, endDate);
         }

         public async Task<IEnumerable<Claims>> GetAllClaims(){
            return await _unitOfWork.SearchRepository.GetAllClaims();
         }

         public async Task<PersonWithEngineereDTO> GetEngNumberAndSupNumber(string engNumber , string supNumber)
         {
            return await _unitOfWork.SearchRepository.GetEngNumberAndSupNumber(supNumber,engNumber);
         }


         public async Task<PersonWithEngineereDTO> GetSubNumberAsync(string subNumber){
            return await _unitOfWork.SearchRepository.GetSubNumberAsync(subNumber);
         }




        public async Task<PersonWithEngineereDTO?> GetPersonWithEngineerByNationalIdAsync(string nationalId)
{
    var person = await _unitOfWork.SearchRepository.GetPersonWithEngineerByNationalIdAsync(nationalId);
    if (person == null) return null;

    // Map the data from person and engineer to PersonWithEngineereDTO
    var dto = new PersonWithEngineereDTO
    {
        // Mapping Person data
        PersonId = person.Id,
        FirstName = person.FirstName,
        FatherName = person.FatherName,
        LastName = person.LastName,
        MotherName = person.MotherName,
        NationalId = person.NationalId,
        EnsuranceNumber = person.EnsuranceNumber,
        BirthDate = person.BirthDate,
        Address = person.Address,
        Phone = person.Phone,
        Mobile = person.Mobile,
        Email = person.Email,
        StatusId = person.StatusId,
        GenderId = person.GenderId,

        // Mapping Engineer data
        EngNumber = person.Engineere?.EngNumber,
        SubNumber = person.Engineere?.SubNumber,
        SpecializationId = person.Engineere?.SpecializationId,
        WorkPlaceId = person.Engineere?.WorkPlaceId,
        Amount = person.Amount,

        // Mapping images to ImageDTO
        Images = person.Images?.Select(image => new ImageDTO
        {
            Id = image.Id,
            
            FileName = image.Image        // محتويات الصورة كـ byte[]
        }).ToList(),

        // Mapping word files to WordDTO
        Words = person.Words?.Select(word => new WordDTO
        {
            Id = word.Id,
              
            FileName = word.Content       // محتويات الملف كـ byte[]
        }).ToList()
    };

    return dto;
}


    
    } }
