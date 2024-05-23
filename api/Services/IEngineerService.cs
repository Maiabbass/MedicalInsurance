using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface IEngineerService
    {
         Task <Response> Add (EngineerPersonEditDTO engineerPersonEditDTO);

          Task <IEnumerable<Engineere>> GetAll();
         Task<Engineere?>Get(int Id);
         public bool Update(int Id, EngineerPersonEditDTO engineerPersonEditDTO);

           public bool Delete(int Id);
     

  
} }