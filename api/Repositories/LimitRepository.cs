using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class LimitRepository : ILimitRepository
    {
         private readonly DataContext _dataContext;
         public LimitRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }

           public async Task<bool> Add_Limits(List<Limits> limits)
        {
            
            await _dataContext.limits.AddRangeAsync(limits);
           return  await _dataContext.SaveChangesAsync()>0;
        }




        public async Task Update_Limits(List<Limits> limits)
{
    if (limits == null || limits.Count == 0)
        throw new ArgumentException("The list of limits cannot be null or empty.");

    int year = limits.First().Year;

    var existingLimits = await _dataContext.limits
        .Where(l => l.Year == year)
        .ToListAsync();
    _dataContext.limits.RemoveRange(existingLimits);

    foreach (var limit in limits)
    {
        var newLimit = new Limits
        {
            Name = limit.Name,
            Year = limit.Year, // استخدام السنة من القيد الجديد
            Value = limit.Value // تعيين القيمة من القيد الجديد
        };

        await _dataContext.limits.AddAsync(newLimit);
    }

    await _dataContext.SaveChangesAsync();
}


 public void Delete_Limite(int year)
        {
             var Items=_dataContext.limits.Where(x=>x.Year==year).ToList();
             if (Items!=null)
             {
                _dataContext.limits.RemoveRange(Items);
                _dataContext.SaveChanges();
             }

    }
}}