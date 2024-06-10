using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface ISurgicalProceduresServices
    {
          Task <Response> Add (SurgicalProceduresEditDTO SurgicalProceduresEditDTO);

          Task <IEnumerable<SurgicalProcedures>> GetAll();
          Task<SurgicalProcedures?> Get (int id);
        bool Update(int id, SurgicalProceduresEditDTO surgicalProceduresEditDTO);
        public bool Delete(int Id);
    }
}