using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface IHospitalService
    {
        
         Task <Response> Add (HospitalEditDTO hospitalEditDTO);

          Task <IEnumerable<Hospital>> GetAll();
           Task<Hospital?>Get(int Id);
        public bool Update(int Id, HospitalEditDTO hospital);
        public bool Delete(int Id);
          
         
    }
}
