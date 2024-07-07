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


         
         

           public async Task<PersonWithEngineereDTO> GetByNationalIdAsync(string nationalId){
             return  await _unitOfWork.SearchRepository.GetByNationalIdAsync(nationalId) ;
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

    
    }
}