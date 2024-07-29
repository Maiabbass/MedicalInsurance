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
               Technical=surgicalProcedures.Technical,
               Financial=surgicalProcedures.Financial,
               Pathological_specialization=surgicalProcedures.Pathological_specialization,
               EnduranceRatio=surgicalProcedures.EnduranceRatio,
               Limit=surgicalProcedures.Limit,

              
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
       databaseEntity.Financial=surgicalProceduresEditDTO.Financial;
       databaseEntity.Technical=surgicalProceduresEditDTO.Technical;
       databaseEntity.Pathological_specialization=surgicalProceduresEditDTO.Pathological_specialization;
       databaseEntity.Limit=surgicalProceduresEditDTO.Limit;
       databaseEntity.EnduranceRatio=surgicalProceduresEditDTO.EnduranceRatio;
  

       return _dataContext.SaveChanges()>0;
      
        }

         



        public void Delete(int Id)
{
    var surgicalProcedure = _dataContext.SurgicalProcedures.FirstOrDefault(x => x.Id == Id);
    if (surgicalProcedure != null)
    {
       
        var claims = _dataContext.Claims.Where(c => c.SurgicalProceduresId == Id).ToList();
        foreach (var claim in claims)
        {
            claim.SurgicalProceduresId = null;
        }

        var Reco = _dataContext.Recovereds.Where(c => c.SurgicalProceduresId == Id).ToList();
        foreach ( var Recoe in Reco){
          Recoe.SurgicalProceduresId = null;
        }


     
        _dataContext.SurgicalProcedures.Remove(surgicalProcedure);
        _dataContext.SaveChanges();
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
    }
}