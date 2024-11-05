using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers;

namespace api.Repositories
{
    public interface IWordRepository
    {
        Task<int> AddWordAsync(Entities.Words word);
        Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file);

        Task<bool> UpdateWordFilesAsync(int personId, List<string> newWordFiles);

        Task DeleteByPersonIdAsync(int personId);
    }
}