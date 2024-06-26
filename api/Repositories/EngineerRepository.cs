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
    public class EngineerRepository : IEngineerRepository
    {
         private readonly DataContext _dataContext;
         public EngineerRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }
        public async Task<int> Add(Engineere engineere)
        {
          try{
            Engineere newEngineer =new Engineere()
             {
               EngNumber=engineere.EngNumber,
               SubNumber = engineere.SubNumber,
               Id =engineere.Id,
               
               SpecializationId=engineere.SpecializationId,
               WorkPlaceId= engineere.WorkPlaceId
             }; 


              _dataContext.Engineeres.Add(newEngineer);
              await _dataContext.SaveChangesAsync();

              return newEngineer.Id;
          }
          catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            throw new Exception("Duplicate entry detected for unique index or constraint.", sqlEx);
        }
        }

        

        public async Task<Engineere?> Get(int Id)
        {
              return await _dataContext.Engineeres.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Engineere>> GetAll()
        {
             return await _dataContext.Engineeres.ToListAsync();
        }


          public bool Update(int Id, EngineerPersonEditDTO engineerPersonEditDTO)
        {
       var databaseEntity= _dataContext.Engineeres.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.EngNumber=engineerPersonEditDTO.EngNumber;
       databaseEntity.SubNumber=engineerPersonEditDTO.SubNumber;
       
       databaseEntity.SpecializationId=engineerPersonEditDTO.SpecializationId;
       databaseEntity.WorkPlaceId=engineerPersonEditDTO.WorkPlaceId;

       return _dataContext.SaveChanges()>0;
      
        }

           public void Delete(int Id){
            var result = _dataContext.Engineeres.Where(x=>x.Id==Id).ToList();
            if (result!=null){
                 _dataContext.Engineeres.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }

    }
}