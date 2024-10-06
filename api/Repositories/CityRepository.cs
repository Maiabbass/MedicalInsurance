using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.Data.SqlClient;
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
          try{
#pragma warning disable IDE0090 // Use 'new(...)'
            City newCity =new City()
             {
               Name=city.Name,
               CallingCode=city.CallingCode
              
             };
#pragma warning restore IDE0090 // Use 'new(...)'


            _dataContext.Cities.Add(newCity);
              await _dataContext.SaveChangesAsync();

              return newCity.Id;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            throw new Exception("Duplicate entry detected for unique index or constraint.", sqlEx);
        }
        }
        public async Task<City?> Get(int Id)
        {
              return await _dataContext.Cities.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<City>> GetAll()
        {
             return await _dataContext.Cities.ToListAsync();
        }

        public bool Update(int Id, CityEditDTO city)
        {
       var databaseEntity= _dataContext.Cities.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Name=city.Name;
       databaseEntity.CallingCode=city.CallingCode;

       return _dataContext.SaveChanges()>0;
      
        }

          public void DeleteByCityId(int CityId){
         var rest=   _dataContext.Hospitals.Where(x=>x.CityId==CityId).ToList();
         if(rest!=null){
            _dataContext.Hospitals.RemoveRange(rest);
            _dataContext.SaveChanges();
         }}

         public void Delete(int Id){
            var result = _dataContext.Cities.Where(x=>x.Id==Id).ToList();
            if (result!=null){
                 _dataContext.Cities.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }

        

    public async Task<int?> GetCityIdByName(string cityName)
{
    var city = await _dataContext.Cities
                .FirstOrDefaultAsync(c => c.Name == cityName);

    return city?.Id;
} 



    public async Task<List<int>> GetExistingCityIds(List<int> cityIds)
    {
        return await _dataContext.Cities
            .Where(c => cityIds.Contains(c.Id))
            .Select(c => c.Id)
            .ToListAsync();
    }
    
    }
}
