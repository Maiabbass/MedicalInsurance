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

        Task<Response> Add(PersonEditDTO personEditDTO, IFormFile[] imageFiles, IFormFile[] wordFiles, int year);
        Task<PagedResult<PersonForView>> GetAll(int pageNumber, int pageSize);

        Task<Person?>GetWithId(int Id);

        Task<bool> DeleteAsync(int Id);
         //Task<bool> UpdateAsync(int id, PersonEditDTO personEditDTO);
         
         Task<AnnualData?> GetEngId(int EngineereId);
          Task<bool> IsEnsuranceNumberInClaimsAsync(string ensuranceNumber);

          Task<bool> UpdatePersonDetails(int id, PersonEditDTO personEditDTO, int year);

          
    }}
