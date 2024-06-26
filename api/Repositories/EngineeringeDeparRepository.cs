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
    public class EngineeringeDeparRepository : IEngineeringeDeparRepository
    {
          private readonly DataContext _dataContext;
         public EngineeringeDeparRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }
        public async Task<int> Add(EngineeringeDepar EngineeringeDepar)
        {
          try{

            EngineeringeDepar newEngD =new EngineeringeDepar()
             {
               Name=EngineeringeDepar.Name,
              
             };



            _dataContext.EngineeringeDepars.Add(newEngD);
              await _dataContext.SaveChangesAsync();

              return newEngD.Id;
          }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            
            throw new Exception("Duplicate entry detected for unique index or constraint.", sqlEx);
        }
        }

         public async Task<EngineeringeDepar?> Get(int Id)
        {
              return await _dataContext.EngineeringeDepars.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EngineeringeDepar>> GetAll()
        {
             return await _dataContext.EngineeringeDepars.ToListAsync();
        }


         public bool Update(int Id, EngineeringeDeparEditDTO engineeringeDeparEditDTO)
        {
       var databaseEntity= _dataContext.EngineeringeDepars.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Name=engineeringeDeparEditDTO.Name;
        

       return _dataContext.SaveChanges()>0;
      
        }

        

         public void DeleteByEngineeringeDeparId(int engineeringeDeparId){

          try{
         var rest = _dataContext.Specializations.Where(x => x.EngineeringeDeparId == engineeringeDeparId).ToList();
    if (rest.Any())
    {
        _dataContext.Specializations.RemoveRange(rest);
        _dataContext.SaveChanges();
    }
          }
           catch (Exception ex)
    {
        Console.WriteLine($"Error in DeleteByEngineeringeDeparId: {ex.Message}");
        throw;
    }
}


         public void Delete(int id){
             var entity = _dataContext.EngineeringeDepars.FirstOrDefault(x => x.Id == id);
    if (entity != null)
    {
        _dataContext.EngineeringeDepars.Remove(entity);
        _dataContext.SaveChanges();
    }
    }
    }}