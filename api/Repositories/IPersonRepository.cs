using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface IPersonRepository
    {
        Task<PagedResult<PersonForView>> GetAll(int pageNumber, int pageSize);

        Task <Person?> Get(int Id);

        Task<int> AddPerson(Person person, IFormFile[] imageFiles, IFormFile[] wordFiles);
        Task<int> Add(City city);
        void Delete(int Id);

        Task<bool> UpdatePersonDetails(int id, PersonEditDTO personEditDTO);
      //  Task<bool> Update(int id, PersonEditDTO personEditDTO);
        
        Task<AnnualData?> GetEngId(int EngineereId);
        Task<bool> IsEnsuranceNumberInClaimsAsync(string ensuranceNumber);

       // public void SavePerson(Person person);
        Task SavePerson(PersonWithEngineereDTO person);

         Task<byte[]> ConvertFileToByteArray(IFormFile file);
    }
}