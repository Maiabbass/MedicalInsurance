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
         
    
    }
}