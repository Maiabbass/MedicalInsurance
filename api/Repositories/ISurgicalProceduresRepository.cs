using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface ISurgicalProceduresRepository
    {
          Task <IEnumerable<SurgicalProcedures>> GetAll();

         Task <SurgicalProcedures?> Get(int Id);

         Task <int> Add (SurgicalProcedures Surpro);

          bool Update ( int Id, SurgicalProceduresEditDTO Surpro );
        bool Update(int id, SurgicalProcedures surgicalProcedures);
       Task Delete(int Id);
         Task<SurgicalProcedures> GetByNameAsync(string name);

         Task<bool> Add_SurgicalProceduers(List<SurgicalProcedures> surgicalProcedures);

         Task Delete_SurgicalProceduresByYear(int year);
    }

    
}