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
        Task <IEnumerable<Person>> GetAll();

        Task <Person?> Get(int Id);

        Task <int> Add (Person person);
        Task<int> Add(City city);
        void Delete(int Id);
        public bool Update(int Id, PersonEditDTO PersonEditDTO);
        Task<AnnualData?> GetEngId(int EngineereId);
        Task<bool> IsEnsuranceNumberInClaimsAsync(string ensuranceNumber);
    }
}