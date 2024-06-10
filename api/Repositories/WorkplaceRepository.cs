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
   public class WorkplaceRepository : IWorkplaceRepository
    {
         private readonly DataContext _dataContext;
         public WorkplaceRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }
        public async Task<int> Add(WorkPlace workPlace)
        {
#pragma warning disable IDE0090 // Use 'new(...)'
            WorkPlace newWorkPlace =new WorkPlace()
             {
               Name=workPlace.Name,
               Location=workPlace.Location,
               EngineeringUnitsId=workPlace.EngineeringUnitsId,
               Phone=workPlace.Phone,
              
             };
#pragma warning restore IDE0090 // Use 'new(...)'


            _dataContext.WorkPlaces.Add(newWorkPlace);
              await _dataContext.SaveChangesAsync();

              return newWorkPlace.Id;
        }

        public async Task<WorkPlace?> Get(int Id)
        {
              return await _dataContext.WorkPlaces.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<WorkPlace>> GetAll()
        {
             return await _dataContext.WorkPlaces.ToListAsync();
        }

        /*
        Task<IEnumerable<WorkPlace>> IWorkplaceRepository.GetAll()
        {
            throw new NotImplementedException();
        }
        */

           public void DeleteByWorkPlaceId(int WorkPlaceId){
         var rest=   _dataContext.AnnualDatas.Where(x=>x.WorkPlaceId==WorkPlaceId).ToList();
         if(rest!=null){
            _dataContext.AnnualDatas.RemoveRange(rest);
            _dataContext.SaveChanges();
         }}

         public void Delete(int Id){
            var result = _dataContext.WorkPlaces.Where(x=>x.Id==Id).ToList();
            if (result!=null){
                 _dataContext.WorkPlaces.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }
  
public bool Update(int Id, WorkplaceEditDTO workplaceEditDTO)
        {
       var databaseEntity= _dataContext.WorkPlaces.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Name=workplaceEditDTO.Name;
       databaseEntity.Location=workplaceEditDTO.Location;
       databaseEntity.EngineeringUnitsId=workplaceEditDTO.EngineeringUnitsId;

       return _dataContext.SaveChanges()>0;
      
        }

  
 

      
    }
}