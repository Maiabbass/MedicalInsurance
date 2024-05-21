using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
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
  


  
     public bool Update(int Id, Dictionary<string, object> updateFields)
{
    var databaseEntity = _dataContext.WorkPlaces.FirstOrDefault(x => x.Id == Id);
    if (databaseEntity == null)
    {
        return false;
    }

    foreach (var field in updateFields)
    {
        if (field.Value is JsonElement jsonElement)
                {
                    switch (field.Key)
                    {
                        case "Name":
                            string name;
                            if (jsonElement.ValueKind == JsonValueKind.String && NewMethod(jsonElement, out name))
                            {
                                databaseEntity.Name = name;
                            }
                            break;

                        case "Location":
                            string location;
                            if (jsonElement.ValueKind == JsonValueKind.String && NewMethod(jsonElement, out location))
                            {
                                databaseEntity.Location = location;
                            }
                            break;

                            case "EngineeringUnitsId":
                    if (jsonElement.TryGetInt32(out int engineeringUnitsId))
                    {
                        databaseEntity.EngineeringUnitsId = engineeringUnitsId;
                    }
                    break;
                    }
                } 
            }

    return _dataContext.SaveChanges() > 0;
}



        private bool NewMethod(JsonElement jsonElement, out string name)
        {
            throw new NotImplementedException();
        }
    }
}