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
         Task<Response> Add(EngineerPersonEditDTO engineerPersonEditDTO, IFormFile[] contentImage, IFormFile[] contentFile);

         Task<PagedResult<PersonWithEngineereDTO>> GetAll(int pageNumber, int pageSize);
         Task<PersonWithEngineereDTO?> Get(int Id);
         public bool Update(int Id, EngineerPersonEditDTO engineerPersonEditDTO);

           public bool Delete(int Id);
     

  
} }