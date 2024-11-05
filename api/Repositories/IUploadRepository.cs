using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IUploadRepository
    {
        Task<List<Person>> ReadExcelFileCash(Stream fileStream);
        Task LoadSubToDatabase(List<Person> list);

        Task<List<Person>> ReadExcelFileRetirement(Stream fileStream);
       

        Task<List<Person>> ReadExcelFileBox(Stream fileStream);

        Task<List<Hospital>> ReadExcelFileHospital(Stream fileStream);
        Task LoadSubToHospital(List<Hospital> list);
        Task<List<SurgicalProcedures>> ReadExcelFileSurgical(Stream fileStream);
        Task LoadSubToSurgical(List<SurgicalProcedures> list);
        Task<List<Subscribers2024>> ImportSubscribersAsync(Stream stream);
        Task LoadSubToDatabase2024(List<Subscribers2024> list);

          Task<List<Person>> ReadExcelFileUnits(Stream fileStream);

          Task<List<Person>> ReadExcelFileUnits2(Stream fileStream);

          Task ReadAndStoreSpecializations(string filePath);
          
        
    }
}
