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
    public class SurgicalProceduresRepository : ISurgicalProceduresRepository
    {
      private readonly DataContext _dataContext;
         public SurgicalProceduresRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }


        public async Task<int> Add(SurgicalProcedures surgicalProcedures )
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            SurgicalProcedures newS =new SurgicalProcedures()
             {
               Name=surgicalProcedures.Name,
             
               Pathological_specialization=surgicalProcedures.Pathological_specialization,
               Price=surgicalProcedures.Price,
               Year=surgicalProcedures.Year,
               Ceiling=surgicalProcedures.Ceiling,
               OUT = surgicalProcedures.OUT,
               IN= surgicalProcedures.IN
              
               //Date=surgicalProcedures.Date,

              
             };
#pragma warning restore IDE0090 // Use 'new(...)'


            _dataContext.SurgicalProcedures.Add(newS);
              await _dataContext.SaveChangesAsync();

              return newS.Id;
        }

        public async Task<SurgicalProcedures?> Get(int Id)
        {
              return await _dataContext.SurgicalProcedures.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SurgicalProcedures>> GetAll()
        {
             return await _dataContext.SurgicalProcedures.ToListAsync();
        }



        public bool Update(int Id, SurgicalProceduresEditDTO surgicalProceduresEditDTO)
        {
       var databaseEntity= _dataContext.SurgicalProcedures.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Name=surgicalProceduresEditDTO.Name;
    
       databaseEntity.Pathological_specialization=surgicalProceduresEditDTO.Pathological_specialization;
       databaseEntity.Price=surgicalProceduresEditDTO.Price;
       databaseEntity.Year= (int)surgicalProceduresEditDTO.Year;
       databaseEntity.Ceiling=surgicalProceduresEditDTO.Ceiling;
       databaseEntity.IN=surgicalProceduresEditDTO.IN;
       databaseEntity.OUT=surgicalProceduresEditDTO.OUT;
       
      // databaseEntity.Date=surgicalProceduresEditDTO.Date;
  

       return _dataContext.SaveChanges()>0;
      
        }

         




     public async Task Delete(int Id)
{
    var surgicalProcedure = await _dataContext.SurgicalProcedures.FirstOrDefaultAsync(x => x.Id == Id);
    if (surgicalProcedure != null)
    {
        // Nullify SurgicalProceduresId in Claims
        var claims = await _dataContext.Claims.Where(c => c.SurgicalProceduresId == Id).ToListAsync();
        foreach (var claim in claims)
        {
            claim.SurgicalProceduresId = null;
        }

        // Nullify SurgicalProceduresId in Recovered
        var recovereds = await _dataContext.Recovereds.Where(c => c.SurgicalProceduresId == Id).ToListAsync();
        foreach (var recovered in recovereds)
        {
            recovered.SurgicalProceduresId = null;
        }

        // Remove the surgical procedure
        _dataContext.SurgicalProcedures.Remove(surgicalProcedure);
        await _dataContext.SaveChangesAsync();
    }
}




        public bool Update(int id, SurgicalProcedures surgicalProcedures)
        {
            throw new NotImplementedException();
        }

        

         public async Task<SurgicalProcedures> GetByNameAsync(string name)
    {
        return await _dataContext.SurgicalProcedures.FirstOrDefaultAsync(sp => sp.Name == name);
    }





     public async Task<bool> Add_SurgicalProceduers(List<SurgicalProcedures> surgicalProcedures)
        {
            
            await _dataContext.SurgicalProcedures.AddRangeAsync(surgicalProcedures);
           return  await _dataContext.SaveChangesAsync()>0;
        }






            public async Task Delete_SurgicalProceduresByYear(int year)
            {
                var surgicalProcedures = await _dataContext.SurgicalProcedures
                    .Where(sp => sp.Year == year)
                    .ToListAsync();

                var surgicalProcedureIds = surgicalProcedures.Select(sp => sp.Id).ToList();

                var recovereds = await _dataContext.Recovereds
                    .Where(r => surgicalProcedureIds.Contains((int)r.SurgicalProceduresId))
                    .ToListAsync();

                foreach (var recovered in recovereds)
                {
                    recovered.SurgicalProceduresId = null;
                }

                var claims = await _dataContext.Claims
                    .Where(c => surgicalProcedureIds.Contains((int)c.SurgicalProceduresId))
                    .ToListAsync();

                foreach (var claim in claims)
                {
                    claim.SurgicalProceduresId = null;
                }

                await _dataContext.SaveChangesAsync();

                _dataContext.SurgicalProcedures.RemoveRange(surgicalProcedures);

                await _dataContext.SaveChangesAsync();
            }




    }
}