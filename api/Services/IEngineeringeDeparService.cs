using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface IEngineeringeDeparService
    {
        
    Task <Response> Add (EngineeringeDeparEditDTO EngineeringeDeparEditDTO);
     Task<EngineeringeDepar?>Get(int Id);
    

     Task<IEnumerable<EngineeringeDepar>>GetAll();

     public bool Update(int Id, EngineeringeDeparEditDTO eng) ;

     public bool Delete(int Id);
        
    }
}