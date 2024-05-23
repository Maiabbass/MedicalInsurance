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

       return _dataContext.SaveChanges()>0;
      
        }

           public void DeleteByHospitalId(int HospitalId){
         var rest=   _dataContext.Claims.Where(x=>x.HospitalId==HospitalId).ToList();
         if(rest!=null){
            _dataContext.Claims.RemoveRange(rest);
            _dataContext.SaveChanges();
         }}

         public void Delete(int Id){
            var result = _dataContext.Hospitals.Where(x=>x.Id==Id).ToList();
            if (result!=null){
                 _dataContext.Hospitals.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }

 
    }
}

