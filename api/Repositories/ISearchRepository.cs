using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface ISearchRepository
    {
         Task<PersonWithEngineereDTO> GetByEnsuranceNumberAsync(string ensuranceNumber);

          Task<IEnumerable<PersonWithEngineereDTO>> GetWithNameAsync(string userSearch);

           Task<PersonWithEngineereDTO> GetByNationalIdAsync(string nationalId);

            Task<PersonWithEngineereDTO> GetEngNumberAsync(string engNumber);

            Task<IEnumerable<EngineeringUnits>> GetEngUnits(string name);

            Task<IEnumerable<WorkPlace>> GetWorkPlace(string name);

             Task<IEnumerable<Hospital>> GetHospital(string name);

             Task<IEnumerable<Claims>> GetClaim(string ensuranceNumber);
        
         

         
       
    }
}