using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class EngineeringeDeparRepositry : IEngineeringeDeparRepositry
    {
         private readonly DataContext _dataContext;
         public EngineeringeDeparRepositry(DataContext dataContext)
         {
            _dataContext =dataContext;
         }


         public async Task<int> Add(EngineeringeDepar EngDepa)
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            EngineeringeDepar newdepar =new EngineeringeDepar()
             {
               Name=EngDepa.Name,
               SpecializationId=EngDepa.SpecializationId,
              
             };
#pragma warning restore IDE0090 // Use 'new(...)'


            _dataContext.EngineeringeDepars.Add(newdepar);
              await _dataContext.SaveChangesAsync();

              return newdepar.Id;
        }



        public async Task<EngineeringeDepar?> Get(int Id)
        {
              return await _dataContext.EngineeringeDepars.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EngineeringeDepar>> GetAll()
        {
             return await _dataContext.EngineeringeDepars.ToListAsync();
        }

        public bool Update(int Id, EngineeringeDeparEditDTO EngDepar)
        {
       var databaseEntity= _dataContext.EngineeringeDepars.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Name=EngDepar.Name;
       databaseEntity.SpecializationId=EngDepar.SpecializationId;

       return _dataContext.SaveChanges()>0;
      
        }




        

         public void Delete(int Id){
            var result = _dataContext.EngineeringeDepars.Where(x=>x.Id==Id).ToList();
            if (result!=null){
                 _dataContext.EngineeringeDepars.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }
    }
}