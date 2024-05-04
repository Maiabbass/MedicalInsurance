using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IHospitalRepository
    
         {
       Task <int> Add (Hospital person);

        Task <IEnumerable<Hospital>> GetAll();

           Task <Hospital?> Get(int Id);

         
    }
    
}