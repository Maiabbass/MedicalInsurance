using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IImageRepository
    {
         Task<int> AddImageAsync(Images image);
         Task<byte[]> ConvertImageToByteArrayAsync(IFormFile image);
         Task<bool> UpdateImagesAsync(int personId, List<string> newImages);
         public void DeleteByPersonId(int PersonId);
    }
}