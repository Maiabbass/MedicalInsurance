using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
     public class HospitalRepository : IHospitalRepository
    {
         private readonly DataContext _dataContext;
         public HospitalRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }
        public async Task<int> Add(Hospital hospital)
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            Hospital newHospital =new Hospital()
             {
               Name=hospital.Name,
               Enabled=hospital.Enabled,
               Inside=hospital.Inside,
               CityId=hospital.CityId,
              
             };
#pragma warning restore IDE0090 // Use 'new(...)'


            _dataContext.Hospitals.Add(newHospital);
              await _dataContext.SaveChangesAsync();

              return newHospital.Id;
        }

        public async Task<Hospital?> Get(int Id)
        {
              return await _dataContext. Hospitals.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Hospital>> GetAll()
        {
             return await _dataContext.Hospitals.ToListAsync();
        }
    }
}

