using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class EngineeringUnitsRepository : IEngineeringUnitsRepository
    { private readonly DataContext _dataContext;
         public EngineeringUnitsRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }
        public async Task<int> Add(EngineeringUnits engineeringUnits)
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            EngineeringUnits newE =new EngineeringUnits()
             {
               Name=engineeringUnits.Name,
               Emailpresident=engineeringUnits.Emailpresident,
               Namepresident=engineeringUnits.Namepresident,
               Phonepresident=engineeringUnits.Phonepresident,


              
             };
#pragma warning restore IDE0090 // Use 'new(...)'


            _dataContext.EngineeringUnits.Add(newE);
              await _dataContext.SaveChangesAsync();

              return newE.Id;
        }

        public async Task<EngineeringUnits?> Get(int Id)
        {
              return await _dataContext.EngineeringUnits.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EngineeringUnits>> GetAll()
        {
             return await _dataContext.EngineeringUnits.ToListAsync();
        }



       


           public void DeleteByEngineeringUnitsId(int EngineeringUnitsId){
              var engineers = _dataContext.Engineeres.Where(x => x.WorkPlace.EngineeringUnitsId == EngineeringUnitsId).ToList();
             foreach (var engineer in engineers){
               
               engineer.WorkPlaceId = null;
    
             }
               _dataContext.SaveChanges();
   
              var rest=   _dataContext.WorkPlaces.Where(x=>x.EngineeringUnitsId==EngineeringUnitsId).ToList();
             if(rest!=null){
            _dataContext.WorkPlaces.RemoveRange(rest);
            _dataContext.SaveChanges();
         }}



           public void DeleteByEngineeringUnitsId2(int EngineeringUnitsId){
         var rest=   _dataContext.AnnualDatas.Where(x=>x.EngineeringUnitsId==EngineeringUnitsId).ToList();
         if(rest!=null){
            _dataContext.AnnualDatas.RemoveRange(rest);
            _dataContext.SaveChanges();
         }}



         public void Delete(int Id){
            var result = _dataContext.EngineeringUnits.Where(x=>x.Id==Id).ToList();
            if (result!=null){
                 _dataContext.EngineeringUnits.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }




        public bool Update(int Id, string Name)
        {
       var databaseEntity= _dataContext.EngineeringUnits.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Name=Name;

       return _dataContext.SaveChanges()>0;
      
        }

        public void DeleteByWorkPlaceId(int WorkPlaceId)
        {
            throw new NotImplementedException();
        }
    }
}