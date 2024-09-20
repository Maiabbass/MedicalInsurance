using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class EnduranceRatioRepository : IEnduranceRatioRepository
    {
        
       private readonly DataContext _dataContext;
         public EnduranceRatioRepository(DataContext dataContext)
         {
            _dataContext =dataContext;
         }

           public async Task<bool> Add_EnduranceRatio(List<EnduranceRatio> Ratio)
        {
            
            await _dataContext.EnduranceRatios.AddRangeAsync(Ratio);
           return  await _dataContext.SaveChangesAsync()>0;
        } 

        public async Task Update_EnduranceRatios(List<EnduranceRatio> enduranceRatios)
{
    if (enduranceRatios == null || enduranceRatios.Count == 0)
        throw new ArgumentException("The list of endurance ratios cannot be null or empty.");

    // افتراض أن جميع النسب الجديدة لها نفس السنة
    int year = enduranceRatios.First().Year;

    // حذف النسب الموجودة للسنة المحددة
    var existingEnduranceRatios = await _dataContext.EnduranceRatios
        .Where(er => er.Year == year)
        .ToListAsync();
    _dataContext.EnduranceRatios.RemoveRange(existingEnduranceRatios);

    // إضافة النسب الجديدة
    foreach (var ratio in enduranceRatios)
    {
        var newEnduranceRatio = new EnduranceRatio
        {
            Name = ratio.Name,
            Year = ratio.Year, // استخدام السنة من النسبة الجديدة
            Value = ratio.Value // تعيين القيمة من النسبة الجديدة
        };

        await _dataContext.EnduranceRatios.AddAsync(newEnduranceRatio);
    }

    await _dataContext.SaveChangesAsync();
}



     public void Delete_Ratio(int year)
        {
             var Items=_dataContext.EnduranceRatios.Where(x=>x.Year==year).ToList();
             if (Items!=null)
             {
                _dataContext.EnduranceRatios.RemoveRange(Items);
                _dataContext.SaveChanges();
             }    
    }
}}