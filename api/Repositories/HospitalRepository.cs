using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using DocumentFormat.OpenXml.Bibliography;
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
               Year=hospital.Year,

              
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
        databaseEntity.Year=hospital.Year;

       return _dataContext.SaveChanges()>0;
      
        }

           





        public async Task Delete(int Id)
{
    var result = await _dataContext.Hospitals.Where(x => x.Id == Id).ToListAsync();  // Asynchronously fetching data

    var claims = await _dataContext.Claims.Where(c => c.HospitalId == Id).ToListAsync();  // Asynchronously fetching claims
    foreach (var claim in claims)
    {
        claim.HospitalId = null;  // Setting foreign key to null
    }

    var reco = await _dataContext.Recovereds.Where(c => c.HospitalId == Id).ToListAsync();  // Asynchronously fetching Recovereds
    foreach (var recoe in reco)
    {
        recoe.HospitalId = null;
    }

    if (result != null)
    {
        _dataContext.Hospitals.RemoveRange(result);
        await _dataContext.SaveChangesAsync();  // Asynchronously saving changes
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



 public async Task<IEnumerable<Hospital>> GetHospitalsByYear(int year)
        {
            return await _dataContext.Hospitals
                .Where(h => h.Year == year)
                .ToListAsync();
        }


         public async Task<bool> Add_Hospital(List<Hospital> hospital)
        {
            
            await _dataContext.Hospitals.AddRangeAsync(hospital);
           return  await _dataContext.SaveChangesAsync()>0;
        }




        public async Task Delete_HospitalsByYear(int year)
{
    // الحصول على المستشفيات التي سيتم حذفها بناءً على السنة
    var hospitals = await _dataContext.Hospitals
        .Where(h => h.Year == year)
        .ToListAsync();

    // التحقق من وجود HospitalId في جدول Recovereds وتعيينه إلى NULL
    var hospitalIds = hospitals.Select(h => h.Id).ToList();

    var recovereds = await _dataContext.Recovereds
        .Where(r => hospitalIds.Contains((int)r.HospitalId))
        .ToListAsync();

    foreach (var recovered in recovereds)
    {
        recovered.HospitalId = null;
    }

    // التحقق من وجود HospitalId في جدول Claims وتعيينه إلى NULL
    var claims = await _dataContext.Claims
        .Where(c => hospitalIds.Contains((int)c.HospitalId))
        .ToListAsync();

    foreach (var claim in claims)
    {
        claim.HospitalId = null;
    }

    // تحديث الجداول بعد تعيين القيم إلى NULL
    await _dataContext.SaveChangesAsync();

    // حذف المستشفيات
    _dataContext.Hospitals.RemoveRange(hospitals);
    
    // حفظ التغييرات
    await _dataContext.SaveChangesAsync();
}



 
    }
}

