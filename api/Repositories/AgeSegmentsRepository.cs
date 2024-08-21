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


  public async Task Update_Age_Segments(List<AgeSegments> ageSegments)
{
    if (ageSegments == null || ageSegments.Count == 0)
        throw new ArgumentException("The list of age segments cannot be null or empty.");

    // افتراض أن جميع الشرائح العمرية الجديدة لها نفس السنة
    int year = ageSegments.First().Year;

    // حذف الشرائح العمرية الموجودة للسنة المحددة
    var existingSegments = await _dataContext.AgeSegments
        .Where(a => a.Year == year)
        .ToListAsync();
    _dataContext.AgeSegments.RemoveRange(existingSegments);

    // إضافة الشرائح العمرية الجديدة
    foreach (var segment in ageSegments)
    {
        var newSegment = new AgeSegments
        {
            FromYear = segment.FromYear,
            ToYear = segment.ToYear,
            TheAmount = segment.TheAmount,
            EnduranceRatio = segment.EnduranceRatio,
            Year = segment.Year // استخدام السنة من الشريحة الجديدة
        };

        await _dataContext.AgeSegments.AddAsync(newSegment);
    }

    await _dataContext.SaveChangesAsync();
}



    }
}