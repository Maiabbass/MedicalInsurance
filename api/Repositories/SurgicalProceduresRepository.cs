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
               Type=surgicalProcedures.Type,

              
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
       databaseEntity.Type=surgicalProceduresEditDTO.Type;

       return _dataContext.SaveChanges()>0;
      
        }

         

         public void Delete(int Id){
            var result = _dataContext.SurgicalProcedures.Where(x=>x.Id==Id).ToList();
            if (result!=null){
                 _dataContext.SurgicalProcedures.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }

        public bool Update(int id, SurgicalProcedures surgicalProcedures)
        {
            throw new NotImplementedException();
        }
    }
}