using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface IPersonService
    {
        object Person { get; }

        Task <Response> Add (PersonEditDTO  personEditDTO);
        Task<IEnumerable<Person>> GetAll();

        Task<Person?>GetWithId(int Id);

        public bool Delete(int Id);
         public bool Update(int Id, PersonEditDTO personEditDTO);
         
         Task<AnnualData?> GetEngId(int EngineereId);
          Task<bool> IsEnsuranceNumberInClaimsAsync(string ensuranceNumber);
    }}
