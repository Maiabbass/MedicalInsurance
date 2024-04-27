using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class EngineerRepository : IEngineerRepository
    {
         private readonly DataContext _dataContext;
         public EngineerRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }
        public async Task<int> Add(Engineere engineere)
        {
            Engineere newEngineer =new Engineere()
             {
               EngNumber=engineere.EngNumber,
               SubNumber = engineere.SubNumber,
               Id =engineere.Id,
               statusId= engineere.statusId,
               SpecializationId=engineere.SpecializationId,
               WorkPlaceId= engineere.WorkPlaceId
             }; 


              _dataContext.Engineeres.Add(newEngineer);
              await _dataContext.SaveChangesAsync();

              return newEngineer.Id;
        }

        public async Task<Engineere?> Get(int Id)
        {
              return await _dataContext.Engineeres.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Engineere>> GetAll()
        {
             return await _dataContext.Engineeres.ToListAsync();
        }
    }
}