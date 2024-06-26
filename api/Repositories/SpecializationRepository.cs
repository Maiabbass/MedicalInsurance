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
    public class SpecializationRepository : ISpecializationRepository
    {
         private readonly DataContext _dataContext;
         public SpecializationRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }

           public async Task<int> Add(Specialization specialization)
        {
          try{

          
#pragma warning disable IDE0090 // Use 'new(...)'
            
            Specialization newSp =new Specialization()
             {
               Name=specialization.Name,
               EngineeringeDeparId=specialization.EngineeringeDeparId,
               
             };
#pragma warning restore IDE0090 // Use 'new(...)'


            _dataContext.Specializations.Add(newSp);
              await _dataContext.SaveChangesAsync();

              return newSp.Id;
          }
          catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            throw new Exception("Duplicate entry detected for unique index or constraint.", sqlEx);
        }
        }

        public async Task<Specialization?> Get(int Id)
        {
            return await _dataContext.Specializations.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Specialization>> GetAll()
        {
            return await _dataContext.Specializations.ToListAsync();
        }

        public void   Delete(int Id)
        {
            
            var rest = _dataContext.Specializations.FirstOrDefault(x=>x.Id==Id);
            if(rest!=null)
            {
                _dataContext.Specializations.Remove(rest);
                _dataContext.SaveChanges();
            }
        }

         public bool Update(int Id, SpecializationEditDto specializationEditDto)
        {
       var databaseEntity= _dataContext.Specializations.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Name=specializationEditDto.Name;
       databaseEntity.EngineeringeDeparId=specializationEditDto.EngineeringeDeparId;

       return _dataContext.SaveChanges()>0;
      
        }
        
    }
}