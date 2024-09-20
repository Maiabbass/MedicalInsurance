using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
   public class ImageRepository : IImageRepository
{
    private readonly DataContext _context;

    public ImageRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> AddImageAsync(Images image)
    {
        _context.Images.Add(image);
        await _context.SaveChangesAsync();
        return image.Id;
    }

    public async Task<byte[]> ConvertImageToByteArrayAsync(IFormFile image)
    {
        if (image == null || image.Length == 0)
            return null;

        using (var memoryStream = new MemoryStream())
        {
            await image.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }


    public async Task<bool> UpdateImagesAsync(int personId, List<string> newImages)
    {
        var person = await _context.Persons.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == personId);
        if (person == null)
        {
            return false;
        }

        // قم بمسح الصور القديمة
        _context.Images.RemoveRange(person.Images);
        
        // قم بإضافة الصور الجديدة
        foreach (var img in newImages)
        {
            person.Images.Add(new Images
            {
                Image = Convert.FromBase64String(img),
                PersonId = personId
            });
        }

        return await _context.SaveChangesAsync() > 0;
    }

    public void DeleteByPersonId(int PersonId){
         var rest=   _context.Images.Where(x=>x.PersonId==PersonId).ToList();
         if(rest!=null){
            _context.Images.RemoveRange(rest);

         }

    
}
}}