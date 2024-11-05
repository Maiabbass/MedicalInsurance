using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;
using static api.DTOS.PersonWithEngineereDTO;

namespace api.Repositories
{
    public interface IEngineerRepository
    {
      Task<PagedResult<PersonWithEngineereDTO>> GetAll(int pageNumber, int pageSize);

       Task<PersonWithEngineereDTO?> Get(int Id);

        Task <int> Add (Engineere person);
        public bool Update(int id, EngineerPersonEditDTO engineerPersonEditDTO, int year);
         Task DeleteAsync(int id);
         Task DeleteByEngIdAsync(int engineerId) ;
         Task DeleteByEngId2Async(int id) ;
    }
}