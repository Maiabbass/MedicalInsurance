using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IUploadRepository
    {
        Task<List<Person>> ReadExcelFileAsync(Stream fileStream);
        Task LoadSubToDatabase(List<Person> list);

        Task<List<Person>> ReadExcelFileAsync2(Stream fileStream);
       

        Task<List<Person>> ReadExcelFileAsync3(Stream fileStream);
        
    }
}
