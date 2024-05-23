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
        public void Delete(int Id);
    }
}