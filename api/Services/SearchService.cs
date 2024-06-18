using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public async Task<Person?>GetWithEN(string EnsuranceNumber){
             return  await _unitOfWork.SearchRepository.GetWithEN(EnsuranceNumber) ;
        }

         public async Task<IEnumerable<Person>?> GetWithName(string Name){
             return  await _unitOfWork.SearchRepository.GetWithName(Name) ;
        }

          public async Task<Person?> GetWithNationalId(string NationalId){
             return  await _unitOfWork.SearchRepository.GetWithNationalId(NationalId) ;
        }
    
    }
}