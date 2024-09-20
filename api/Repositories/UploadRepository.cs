using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore;
using api.Services;


namespace api.Repositories
{
    public class UploadRepository : IUploadRepository
    {
        private readonly DataContext _dataContext;

        private readonly ICityService _cityService;
        

        public UploadRepository(DataContext dataContext , ICityService cityService )
        {
            _dataContext = dataContext;
            _cityService= cityService;
              
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



       public async Task<List<Person>> ReadExcelFileCash(Stream fileStream)
{
    using var package = new ExcelPackage(fileStream);
    var worksheet = package.Workbook.Worksheets[0];
    var rowCount = worksheet.Dimension.Rows;
    var people = new List<Person>();

    for (int row = 2; row <= rowCount; row++)
    {
        var genderText = worksheet.Cells[row, 9].Text.ToLower(); // اقرأ الجنس من العمود المناسب
        int genderId = (genderText.Trim() == "ذكر" || genderText.Trim() == "male") ? 1 : 2;

        var person = new Person
        {
            FirstName = worksheet.Cells[row, 5].Text,
            FatherName = worksheet.Cells[row, 6].Text,
            LastName = worksheet.Cells[row, 7].Text,
            MotherName = worksheet.Cells[row, 8].Text,
            NationalId = worksheet.Cells[row, 12].Text.Length > 11 ? worksheet.Cells[row, 12].Text.Substring(0, 11) : worksheet.Cells[row, 12].Text,
            EnsuranceNumber = worksheet.Cells[row, 11].Text,
            Mobile = worksheet.Cells[row, 14].Text,
            GenderId = genderId // تعيين GenderId هنا
        };

        // Handle birth date from cell 13
        var birthDateText = worksheet.Cells[row, 13].Text;
        if (!string.IsNullOrEmpty(birthDateText) && DateTime.TryParseExact(birthDateText, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDate))
        {
            person.BirthDate = birthDate;
        }
        else
        {
            // If parsing fails or birth date is missing, set it to null or handle accordingly
            person.BirthDate = null;
        }

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

    return people;
}
    



 public async Task<List<Person>> ReadExcelFileRetirement(Stream fileStream)
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
        







   public async Task<List<Person>> ReadExcelFileBox(Stream fileStream)
{
    var people = new List<Person>();

    
        using var package = new ExcelPackage(fileStream);
        var worksheet = package.Workbook.Worksheets[0]; // تحديد الورقة الأولى

        if (worksheet == null)
        {
            throw new CustomException("Worksheet not found in the Excel file.");
        }
 if (worksheet.Dimension == null || worksheet.Dimension.Rows < 2)
    {
        throw new CustomException("Worksheet is empty or does not contain data.");
    }
        var rowCount = worksheet.Dimension.Rows ;
        
  
   
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
private string GetCellTextOrNull(ExcelRange cell)
{
    return string.IsNullOrWhiteSpace(cell?.Text) ? null : cell.Text.Trim();
}



 public async Task<List<Hospital>> ReadExcelFileHospital(Stream fileStream)
{
    using var package = new ExcelPackage(fileStream);
    var worksheet = package.Workbook.Worksheets[0];
    var rowCount = worksheet.Dimension.Rows;
    var hospitals = new List<Hospital>();

    for (int row = 2; row <= rowCount; row++)
    {
        var hospitalName = worksheet.Cells[row, 1].Text;
        var phone = worksheet.Cells[row, 2].Text;
        var address = worksheet.Cells[row, 3].Text;
        var cityName = worksheet.Cells[row, 4].Text;


        if (string.IsNullOrEmpty(hospitalName) && string.IsNullOrEmpty(phone) &&
            string.IsNullOrEmpty(address) && string.IsNullOrEmpty(cityName))
        {
            break; // الخروج من الحلقة عند الوصول إلى صف فارغ
        }


        var cityId = await _cityService.GetCityIdByName(cityName);
        if (cityId == null)
        {
            throw new CustomException($"City not found for name '{cityName}' in row {row}");
        }



        var hospital = new Hospital
        {
            Name = hospitalName,
            Phone = phone,
            Address = address,
            CityId = cityId.Value,
        };

        hospitals.Add(hospital);
    }

    return hospitals; // إرجاع قائمة المستشفيات التي تم قراءتها
}








public async Task<List<SurgicalProcedures>> ReadExcelFileSurgical(Stream fileStream)
{
    using var package = new ExcelPackage(fileStream);
    var worksheet = package.Workbook.Worksheets[0];
    var rowCount = worksheet.Dimension.Rows;
    var surgicals = new List<SurgicalProcedures>();

    for (int row = 2; row <= rowCount; row++)
    {
        var name = worksheet.Cells[row, 1].Text;
        var priceText = worksheet.Cells[row, 2].Text;


        if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(priceText))
        {
            break; // الخروج من الحلقة عند الوصول  
        }

        if (!decimal.TryParse(priceText, out var price))
        {
            throw new CustomException($"Invalid price format in row {row}");
        }

        var surgical = new SurgicalProcedures
        {
            Name = name,
            Price = price, // تخزين القيمة كـ decimal
        };

        surgicals.Add(surgical);
    }

    return surgicals; 

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





         public async Task LoadSubToHospital(List<Hospital> list)
        {
            try
            {
                _dataContext.Hospitals.AddRange(list);
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
    
    

         public async Task LoadSubToSurgical(List<SurgicalProcedures> list)
        {
            try
            {
                _dataContext.SurgicalProcedures.AddRange(list);
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
