using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface ISubscriberRepository
    {
        Task<List<Person>> ReadExcelFileAsync(Stream fileStream);
        Task LoadSubToDatabase(List<Person> list);
    }
}
