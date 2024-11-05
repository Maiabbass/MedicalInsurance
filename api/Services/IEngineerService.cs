using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;
using static api.DTOS.PersonWithEngineereDTO;

namespace api.Services
{
    public interface IEngineerService
    {
         Task<Response> Add(EngineerPersonEditDTO engineerPersonEditDTO, IFormFile[] contentImage, IFormFile[] contentFile ,int year);

         Task<PagedResult<PersonWithEngineereDTO>> GetAll(int pageNumber, int pageSize);
         Task<PersonWithEngineereDTO?> Get(int Id);
         public bool Update(int id, EngineerPersonEditDTO engineerPersonEditDTO, int year);

          Task<bool> DeleteAsync(int Id);
     

  
} }