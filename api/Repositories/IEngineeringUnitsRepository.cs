using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IEngineeringUnitsRepository
    {
        
         Task <IEnumerable<EngineeringUnits>> GetAll();

         Task <EngineeringUnits?> Get(int Id);

         Task <int> Add (EngineeringUnits person);
         public void DeleteByEngineeringUnitsId(int EngineeringUnitsId);
         public void DeleteByEngineeringUnitsId2(int EngineeringUnitsId);
         public void Delete(int id);
          public bool Update(int Id, string Name);

    } 
    }
