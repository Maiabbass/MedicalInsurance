using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class WordRepository : IWordRepository
    {

     private readonly DataContext _context;

    public WordRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> AddWordAsync(Words word)
    {
        _context.Words.Add(word);
        await _context.SaveChangesAsync();
        return word.Id;
    }

      

        public async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }



     public async Task<bool> UpdateWordFilesAsync(int personId, List<string> newWordFiles)
    {
        var person = await _context.Persons.Include(p => p.Words).FirstOrDefaultAsync(p => p.Id == personId);
        if (person == null)
        {
            return false;
        }

        // قم بمسح الملفات القديمة
        _context.Words.RemoveRange(person.Words);

        // قم بإضافة الملفات الجديدة
        foreach (var wordFile in newWordFiles)
        {
            person.Words.Add(new Words
            {
                Content = Convert.FromBase64String(wordFile),
                PersonId = personId
            });
        }

        return await _context.SaveChangesAsync() > 0;
    }




  public async Task DeleteByPersonIdAsync(int personId) {
    var words = await _context.Words
        .Where(x => x.PersonId == personId)
        .ToListAsync();

    if (words.Any()) {
        _context.Words.RemoveRange(words);
        await _context.SaveChangesAsync();
    }
}
}}