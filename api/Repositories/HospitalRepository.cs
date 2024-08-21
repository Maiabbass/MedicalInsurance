using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
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
               Address=hospital.Address,
               Email=hospital.Email,
               Phone=hospital.Phone,
               Longitude=hospital.Longitude,
               latitude=hospital.latitude,

              
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


      


        public bool Update(int Id, HospitalEditDTO hospital)
        {
       var databaseEntity= _dataContext.Hospitals.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Enabled=hospital.Enabled;
        databaseEntity.Inside=hospital.Inside;
        databaseEntity.Name=hospital.Name;
        databaseEntity.Address=hospital.Address;
        databaseEntity.Phone=hospital.Phone;
        databaseEntity.Email=hospital.Email;
        databaseEntity.CityId=hospital.CityId;
        databaseEntity.Longitude=hospital.Longitude;
        databaseEntity.latitude=hospital.Latitude;

       return _dataContext.SaveChanges()>0;
      
        }

           

         public void Delete(int Id){
            var result = _dataContext.Hospitals.Where(x=>x.Id==Id).ToList();

            var claims = _dataContext.Claims.Where(c => c.HospitalId == Id).ToList();
        foreach (var claim in claims)
        {
            claim.HospitalId = null;
        }
        var Reco = _dataContext.Recovereds.Where(c => c.HospitalId == Id).ToList();
        foreach ( var Recoe in Reco){
          Recoe.HospitalId = null;
        }  
            if (result!=null){
                 _dataContext.Hospitals.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }


         public async Task<IEnumerable<Hospital>> GetHospitalsByCityIdAsync(int cityId)
        {
            return await _dataContext.Hospitals
                .Where(h => h.CityId == cityId)
                .ToListAsync();
        }


        public async Task<Hospital> GetByNameAsync(string name)
    {
        return await _dataContext.Hospitals.FirstOrDefaultAsync(h => h.Name == name);
    }


 
    }
}

