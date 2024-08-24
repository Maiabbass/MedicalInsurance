using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;


namespace api.Repositories
{
    public class UploadRepository : IUploadRepository
    {
        private readonly DataContext _dataContext;
        

        public UploadRepository(DataContext dataContext )
        {
            _dataContext = dataContext;
              
        }

        public class CustomException : Exception
        {
            public CustomException(string? message) : base(message)
            {
            }

            public CustomException(string message, Exception innerException) : base(message, innerException)
            {
            }

            public string GetFullMessage()
            {
                var messages = new List<string>();
                var currentException = this;
                while (currentException != null)
                {
                    messages.Add(currentException.Message);
                    currentException = currentException.InnerException as CustomException;
                }
                return string.Join(" --> ", messages);
            }
        }

        public async Task<List<Person>> ReadExcelFileAsync(Stream fileStream)
{
    using var package = new ExcelPackage(fileStream);
    var worksheet = package.Workbook.Worksheets[0];
    var rowCount = worksheet.Dimension.Rows;
    var people = new List<Person>();

    for (int row = 2; row <= rowCount; row++)
    {
        if (worksheet.Cells[row, 10].Text == "م") // تحقق من الحالة المطلوبة
        {
            var genderText = worksheet.Cells[row, 9].Text.ToLower(); // اقرأ الجنس من العمود المناسب
            int genderId =( genderText.Trim() == "ذكر" || genderText.Trim() == "male") ? 1 : 2;

            var person = new Person
            {
                FirstName = worksheet.Cells[row, 5].Text,
                FatherName = worksheet.Cells[row, 6].Text,
                LastName = worksheet.Cells[row, 7].Text,
                MotherName = worksheet.Cells[row, 8].Text,
                NationalId = worksheet.Cells[row, 12].Text.Length > 11 ? worksheet.Cells[row, 12].Text.Substring(0, 11) : worksheet.Cells[row, 12].Text,
                EnsuranceNumber = worksheet.Cells[row, 11].Text,
                Mobile = worksheet.Cells[row, 14].Text,
                GenderId = genderId, // تعيين GenderId هنا
            };
/*
            var birthDateText = worksheet.Cells[row, 13].Text;
            if (DateTime.TryParseExact(birthDateText, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDate))
            {
                person.BirthDate = birthDate;
            }
            else
            {
                person.BirthDate = null;
            }
*/
            var engineere = new Engineere
            {
                EngNumber = worksheet.Cells[row, 3].Text,
                SubNumber = worksheet.Cells[row, 4].Text,
                Person = person
            };

            try
            {
                _dataContext.Persons.Add(person);
                await _dataContext.SaveChangesAsync();

                _dataContext.Engineeres.Add(engineere);
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException($"Database update error occurred while processing row {row}", ex);
            }
            catch (Exception ex)
            {
                throw new CustomException($"Error occurred while processing row {row}", ex);
            }

            people.Add(person);
        }
    }

    return people;
}
      
    


 public async Task<List<Person>> ReadExcelFileAsync2(Stream fileStream)
{
    using var package = new ExcelPackage(fileStream);
    var worksheet = package.Workbook.Worksheets[0];
    var rowCount = worksheet.Dimension.Rows;
    var people = new List<Person>();

    for (int row = 2; row <= rowCount; row++)
    {
        if (worksheet.Cells[row, 10].Text == "م") // تحقق من الحالة المطلوبة
        {
            var genderText = worksheet.Cells[row, 9].Text.ToLower(); // اقرأ الجنس من العمود المناسب
            int genderId =( genderText.Trim() == "ذكر" || genderText.Trim() == "Male") ? 1 : 2;

            var person = new Person
            {
                FirstName = worksheet.Cells[row, 5].Text,
                FatherName = worksheet.Cells[row, 6].Text,
                LastName = worksheet.Cells[row, 7].Text,
                MotherName = worksheet.Cells[row, 8].Text,
                NationalId = worksheet.Cells[row, 12].Text.Length > 11 ? worksheet.Cells[row, 12].Text.Substring(0, 11) : worksheet.Cells[row, 12].Text,
                EnsuranceNumber = worksheet.Cells[row, 11].Text,
                Mobile = worksheet.Cells[row, 15].Text,
                GenderId = genderId, // تعيين GenderId هنا
            };
/*
            var birthDateText = worksheet.Cells[row, 13].Text;
            if (DateTime.TryParseExact(birthDateText, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDate))
            {
                person.BirthDate = birthDate;
            }
            else
            {
                person.BirthDate = null;
            }
*/
            var engineere = new Engineere
            {
                EngNumber = worksheet.Cells[row, 3].Text,
                SubNumber = worksheet.Cells[row, 4].Text,
                Person = person
            };

            try
            {
                _dataContext.Persons.Add(person);
                await _dataContext.SaveChangesAsync();

                _dataContext.Engineeres.Add(engineere);
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException($"Database update error occurred while processing row {row}", ex);
            }
            catch (Exception ex)
            {
                throw new CustomException($"Error occurred while processing row {row}", ex);
            }

            people.Add(person);
        }
    }

    return people;
}
        







   public async Task<List<Person>> ReadExcelFileAsync3(Stream fileStream)
{
    var people = new List<Person>();

    
        using var package = new ExcelPackage(fileStream);
        var worksheet = package.Workbook.Worksheets[0]; // تحديد الورقة الأولى

        if (worksheet == null)
        {
            throw new CustomException("Worksheet not found in the Excel file.");
        }

        var rowCount = worksheet.Dimension.Rows ;
        
         if (worksheet.Dimension == null)
{
    throw new CustomException("Worksheet is empty.");
}
   
        for (int row = 2; row <= rowCount; row++)
{
    if (worksheet.Cells[row, 9] != null && worksheet.Cells[row, 9]?.Text == "م")
    {
        var genderText = GetCellTextOrNull(worksheet.Cells[row, 8]);
        int genderId = (genderText?.Trim() == "ذكر" || genderText?.Trim() == "Male") ? 1 : 2;

        if (worksheet.Cells[row, 4] == null || worksheet.Cells[row, 5] == null || worksheet.Cells[row, 6] == null || worksheet.Cells[row, 7] == null || worksheet.Cells[row, 11] == null || worksheet.Cells[row, 10] == null || worksheet.Cells[row, 14] == null || worksheet.Cells[row, 2] == null || worksheet.Cells[row, 3] == null)
        {
            throw new CustomException($"Data missing in row {row}.");
        }

        var person = new Person
        {
            FirstName = GetCellTextOrNull(worksheet.Cells[row, 4]),
            FatherName = GetCellTextOrNull(worksheet.Cells[row, 5]),
            LastName = GetCellTextOrNull(worksheet.Cells[row, 6]),
            MotherName = GetCellTextOrNull(worksheet.Cells[row, 7]),
            NationalId = GetCellTextOrNull(worksheet.Cells[row, 11])?.Length > 11 
                ? GetCellTextOrNull(worksheet.Cells[row, 11])?.Substring(0, 11) 
                : GetCellTextOrNull(worksheet.Cells[row, 11]),
            EnsuranceNumber = GetCellTextOrNull(worksheet.Cells[row, 10]),
            Mobile = GetCellTextOrNull(worksheet.Cells[row, 14]),
            GenderId = genderId,
        };

        var engineere = new Engineere
        {
            EngNumber = GetCellTextOrNull(worksheet.Cells[row, 2]),
            SubNumber = GetCellTextOrNull(worksheet.Cells[row, 3]),
            Person = person
        };

        _dataContext.Persons.Add(person);
        await _dataContext.SaveChangesAsync();

        _dataContext.Engineeres.Add(engineere);
        await _dataContext.SaveChangesAsync();

        people.Add(person);
    }
}


return people;

}
private string GetCellTextOrNull(ExcelRange cell)
{
    return string.IsNullOrWhiteSpace(cell?.Text) ? null : cell.Text.Trim();
}




public async Task LoadSubToDatabase(List<Person> list)
        {
            try
            {
                _dataContext.Persons.AddRange(list);
                await _dataContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException("An error occurred while saving the data to the database", ex);
            }
            catch (Exception ex)
            {
                throw new CustomException("An unexpected error occurred while saving the data", ex);
            }
        }
    
        }
    
    }
