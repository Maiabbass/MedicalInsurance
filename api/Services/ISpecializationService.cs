using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface ISpecializationService
    {
        Task<Response> Add(SpecializationEditDto specializationEditDto);
         Task<IEnumerable<Specialization>>  GetAll();
        

     Task<Specialization?>GetWithId(int Id);
     public bool Delete(int Id);

    public bool Update(int id, SpecializationEditDto specializationEditDto);
        
    }
}