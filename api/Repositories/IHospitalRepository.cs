using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface IHospitalRepository
    
         {
       Task <int> Add (Hospital person);

        Task <IEnumerable<Hospital>> GetAll();

           Task <Hospital?> Get(int Id);
           public bool Update(int Id, HospitalEditDTO hospital);

           

         Task Delete(int Id);

          Task<IEnumerable<Hospital>> GetHospitalsByCityIdAsync(int cityId);

           Task<Hospital> GetByNameAsync(string name);
            Task<IEnumerable<Hospital>> GetHospitalsByYear(int year);
            Task<bool> Add_Hospital(List<Hospital> hospital);
            Task Delete_HospitalsByYear(int year);

         
    }
    
}