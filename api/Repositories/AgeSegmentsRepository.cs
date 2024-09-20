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



       public async Task Delete_AgeSegmentsByYear(int year)
            {
                var ageSegments = await _dataContext.AgeSegments
                    .Where(a => a.Year == year)
                    .ToListAsync();

                _dataContext.AgeSegments.RemoveRange(ageSegments);
               _dataContext.SaveChanges();
            }



  public async Task Update_Age_Segment(AgeSegments ageSegment)
{
    var existingSegment = await _dataContext.AgeSegments
        .FirstOrDefaultAsync(a => a.Id == ageSegment.Id);

    if (existingSegment != null)
    {
        existingSegment.FromYear = ageSegment.FromYear;
        existingSegment.ToYear = ageSegment.ToYear;
        existingSegment.TheAmount = ageSegment.TheAmount;
        existingSegment.EnduranceRatio = ageSegment.EnduranceRatio;
        existingSegment.Year = ageSegment.Year; // Update the year if necessary

        _dataContext.AgeSegments.Update(existingSegment);
        await _dataContext.SaveChangesAsync();
    }
    else
    {
        // Handle the case where the AgeSegment with the given Id doesn't exist
        throw new ArgumentException("Age segment with the provided Id does not exist.");
    }
}



    }
}