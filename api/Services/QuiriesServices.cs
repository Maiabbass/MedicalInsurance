using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;
using api.Repositories;

namespace api.Services
{
    public class QuiriesServices : IQuiriesServices
    {

        private  readonly IUnitOfWork _unitOfWork;

        public QuiriesServices(IUnitOfWork unitOfWork)
         {
            _unitOfWork=unitOfWork;
         } 

         public async Task<SimpleEngineer> GetEngineerWithRelationsAsync(string EngNumber){
            return await _unitOfWork.QuiriesRepositories.GetEngineerWithRelationsAsync(EngNumber);
         }

         public async Task<IEnumerable<SimpleEngineer>> GetEngineers(int? workPlaceId ,int? specializationId , int? engineeringUnitsId , int? PayMethodId  ){
            return await _unitOfWork.QuiriesRepositories.GetEngineers(workPlaceId,specializationId,engineeringUnitsId,PayMethodId);
         }



        
    }
}