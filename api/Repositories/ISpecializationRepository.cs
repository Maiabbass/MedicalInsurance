using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface ISpecializationRepository
    {
        Task<int> Add(Specialization specialization);
         Task<Specialization?> Get(int Id);
    
         Task<IEnumerable<Specialization>> GetAll();
         public void   Delete(int Id);

         public bool Update(int Id, SpecializationEditDto specializationEditDto);
        
    }
}