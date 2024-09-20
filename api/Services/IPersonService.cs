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

        Task<Response> Add(PersonEditDTO personEditDTO,  IFormFile[] imageFiles, IFormFile[] wordFiles);
        Task<PagedResult<PersonForView>> GetAll(int pageNumber, int pageSize);

        Task<Person?>GetWithId(int Id);

        public bool Delete(int Id);
         //Task<bool> UpdateAsync(int id, PersonEditDTO personEditDTO);
         
         Task<AnnualData?> GetEngId(int EngineereId);
          Task<bool> IsEnsuranceNumberInClaimsAsync(string ensuranceNumber);

          Task<bool> UpdatePersonDetails(int id, PersonEditDTO personEditDTO);

          
    }}
