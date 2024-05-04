using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CityRepository : ICityRepository
    {
         private readonly DataContext _dataContext;
         public CityRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }
        public async Task<int> Add(City city)
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            City newCity =new City()
             {
               Name=city.Name,
              
             };
#pragma warning restore IDE0090 // Use 'new(...)'


            _dataContext.Cities.Add(newCity);
              await _dataContext.SaveChangesAsync();

              return newCity.Id;
        }

        public async Task<City?> Get(int Id)
        {
              return await _dataContext.Cities.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<City>> GetAll()
        {
             return await _dataContext.Cities.ToListAsync();
        }
    }
}
