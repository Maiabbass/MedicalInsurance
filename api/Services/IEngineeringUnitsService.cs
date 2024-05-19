using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface IEngineeringUnitsService
    {
         
         Task <Response> Add (EngineeringUnitsEditDTO  engineeringUnitsEditDTO );

          Task <IEnumerable<EngineeringUnits>> GetAll();
          

       Task<EngineeringUnits?> Get(int Id);
       public bool Delete(int Id);
        public bool Update(int Id, string Name);
         
    
    }
}