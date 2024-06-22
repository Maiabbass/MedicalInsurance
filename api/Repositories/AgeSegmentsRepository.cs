using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class AgeSegmentsRepository : IAgeSegmentsRepository
    {
         private readonly DataContext _dataContext;

         public AgeSegmentsRepository(DataContext dataContext)
         {
            _dataContext=dataContext;
         }
        public async Task<IEnumerable<AgeSegments>> Get(int year)
        {
        return  await _dataContext.AgeSegments.Where(x=>x.Year==year).ToListAsync();
        }
        public async Task<bool> Add_Age_Segments(List<AgeSegments> ageSegments)
        {
            
            await _dataContext.AgeSegments.AddRangeAsync(ageSegments);
           return  await _dataContext.SaveChangesAsync()>0;
        }

        public void Delete_Age_Segments(int year)
        {
             var segmentsDBItems=_dataContext.AgeSegments.Where(x=>x.Year==year).ToList();
             if (segmentsDBItems!=null)
             {
                _dataContext.AgeSegments.RemoveRange(segmentsDBItems);
                _dataContext.SaveChanges();
             }
             
        }
    }
}