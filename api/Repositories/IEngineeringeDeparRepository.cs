using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface IEngineeringeDeparRepository
    {
        Task<int> Add(EngineeringeDepar EngineeringeDepar);
         Task<EngineeringeDepar?> Get(int Id);
       

         Task<IEnumerable<EngineeringeDepar>> GetAll();
         public bool Update(int Id, EngineeringeDeparEditDTO engineeringeDeparEditDTO);

         public void Delete(int Id);
          

         public void DeleteByEngineeringeDeparId(int EngineeringeDeparId);
    }
}