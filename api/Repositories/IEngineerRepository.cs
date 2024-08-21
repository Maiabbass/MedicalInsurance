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
           Task<IEnumerable<EngineerFull>> GetAll(int pageNumber, int pageSize);

        Task <Engineere?> Get(int Id);

        Task <int> Add (Engineere person);
         public bool Update(int Id, EngineerPersonEditDTO engineerPersonEditDTO);
         public void Delete(int Id);
         public void DeleteByEngId(int EngineereId);
         public void DeleteByEngId2(int EngineereId);
    }
}