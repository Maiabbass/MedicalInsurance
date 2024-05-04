using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class EngineeringUnitsRepository : IEngineeringUnitsRepository
    { private readonly DataContext _dataContext;
         public EngineeringUnitsRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }
        public async Task<int> Add(EngineeringUnits engineeringUnits)
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            EngineeringUnits newE =new EngineeringUnits()
             {
               Number=engineeringUnits.Number,
              
             };
#pragma warning restore IDE0090 // Use 'new(...)'


            _dataContext.EngineeringUnits.Add(newE);
              await _dataContext.SaveChangesAsync();

              return newE.Id;
        }

        public async Task<EngineeringUnits?> Get(int Id)
        {
              return await _dataContext.EngineeringUnits.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EngineeringUnits>> GetAll()
        {
             return await _dataContext.EngineeringUnits.ToListAsync();
        }
    }
}