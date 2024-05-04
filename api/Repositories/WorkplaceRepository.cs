using System;
using System.Collections.Generic;
using System.Linq;
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

        Task<IEnumerable<WorkPlace>> IWorkplaceRepository.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}