using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using OfficeOpenXml;

namespace api.Repositories
{
    public class SubscriberRepository : ISubscriberRepository
    {
        private readonly DataContext _dataContext;

        public DataContext DataContext => _dataContext;

        public SubscriberRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Person>> ReadExcelFileAsync(Stream fileStream)
        {
            using var package = new ExcelPackage(fileStream);
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;
            var people = new List<Person>();

            for (int row = 2; row <= rowCount; row++)
            {
                var person = new Person
                {
                    EnsuranceNumber = worksheet.Cells[row, 1].Text,
                    FirstName = worksheet.Cells[row, 4].Text,
                    FatherName = worksheet.Cells[row, 6].Text,
                    LastName = worksheet.Cells[row, 5].Text,
                    BirthDate = DateTime.ParseExact(worksheet.Cells[row, 3].Text, "dd-MMM-yyyy", CultureInfo.InvariantCulture),
                    NationalId = worksheet.Cells[row, 8].Text,
                    Mobile = worksheet.Cells[row, 11].Text,
                    GenderId = worksheet.Cells[row, 7].Text == "Male" ? 1 : 2 // Assuming 1 is Male and 2 is Female
                };


                 var existingEngineere = await _dataContext.Engineeres.FindAsync(person.Id);
                if (existingEngineere == null)
                {
                    // قم بإنشاء المهندس إذا لم يكن موجوداً
                    var engineere = new Engineere
                    {
                        
                    };
                    _dataContext.Engineeres.Add(engineere);
                    await _dataContext.SaveChangesAsync();
                    person.Engineere = engineere;
                }
                else
                {
                    person.Engineere = existingEngineere;
                };

                var annualData = new AnnualData
                {
                    Year = 2023,
                    Amount = decimal.Parse(worksheet.Cells[row, 9].Text),
                    EngineereId = person.Id
                };

                var annualDataDetail = new AnnualDataDetail
                {
                    PersonId = person.Id,
                    Person = person,
                    AnnualDataId = annualData.Id,
                    AnnualData = annualData,
                    IsEngineer = true, // قيمة مبدئية
                    Amount = annualData.Amount
                };

                person.AnnualDataDetails = new List<AnnualDataDetail> { annualDataDetail };

                people.Add(person);
            }

            return people;
        }

        public async Task LoadSubToDatabase(List<Person> list)
        {
            _dataContext.Persons.AddRange(list);
            await _dataContext.SaveChangesAsync();
        }

        public Task LoadSubToDatabase(List<Controllers.Persons> List)
        {
            throw new NotImplementedException();
        }
    }
}
