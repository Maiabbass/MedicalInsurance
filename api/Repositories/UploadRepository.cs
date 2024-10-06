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
using System.Text;
using System.Data;


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
    
    // Define possible date formats
    var dateFormats = new[] { "yyyy", "M/d/yyyy", "d/M/yyyy", "MMMM d, yyyy", "dd-MMM-yyyy" };

    for (int row = 2; row <= rowCount; row++)
    {
        var genderText = worksheet.Cells[row, 9].Text.ToLower(); // Read gender from the appropriate column
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
            GenderId = genderId, // Assign GenderId
            PayMethodId = 1 // Set PayMethodId to 1 as per requirement
        };

        // Handle birth date with multiple formats
        var birthDateText = worksheet.Cells[row, 13].Text;
        if (!string.IsNullOrEmpty(birthDateText))
        {
            if (DateTime.TryParseExact(birthDateText, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDate))
            {
                // Format birth date as dd-MMM-yyyy
                person.BirthDate = birthDate;
            }
            else
            {
                // Handle invalid or unrecognized date formats, setting birth date to null or default
                person.BirthDate = null;
            }
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

    for (int row = 2; row <= rowCount; row++) // Assuming row 1 is the header
    {
        if (true) // تحقق من الحالة المطلوبة
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
                Mobile = worksheet.Cells[row, 15].Text,
                GenderId = genderId, // تعيين GenderId هنا
                PayMethodId = 3 // تعيين PayMethodId إلى 3
            };

            // Parse the birth date with multiple formats, including "dd-MMM-yyyy" (e.g., "20-Mar-1965")
            var birthDateText = worksheet.Cells[row, 13].Text.Trim();
            var dateFormats = new[] { "dd/MM/yyyy", "M/d/yyyy", "yyyy", "MMMM d, yyyy", "d/M/yyyy", "dd-MMM-yyyy" }; // Date formats including "dd-MMM-yyyy"

            if (DateTime.TryParseExact(birthDateText, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDate))
            {
                person.BirthDate = birthDate; // Store DateTime directly
            }
            else
            {
                person.BirthDate = null; // Handle invalid or missing dates
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

    var rowCount = worksheet.Dimension.Rows;

    for (int row = 2; row <= rowCount; row++)
    {
        var genderText = GetCellTextOrNull(worksheet.Cells[row, 8]);
        int genderId = (genderText?.Trim() == "ذكر" || genderText?.Trim() == "Male") ? 1 : 2;

        if (worksheet.Cells[row, 4] == null || worksheet.Cells[row, 5] == null || worksheet.Cells[row, 6] == null || worksheet.Cells[row, 7] == null || worksheet.Cells[row, 11] == null || worksheet.Cells[row, 10] == null || worksheet.Cells[row, 14] == null || worksheet.Cells[row, 2] == null || worksheet.Cells[row, 3] == null)
        {
            throw new CustomException($"Data missing in row {row}.");
        }

        // Attempt to parse the date in cell number 12
        var dateOfBirthText = GetCellTextOrNull(worksheet.Cells[row, 12]);
        DateTime? dateOfBirth = ParseDate(dateOfBirthText);

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
            BirthDate = dateOfBirth,
            PayMethodId=2
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

    return people;
}



// Helper method to parse different date formats
private DateTime? ParseDate(string dateText)
{
    if (string.IsNullOrWhiteSpace(dateText)) return null;

     string[] formats = { 
        "dd-MMM-yyyy",    // e.g., 06-Aug-1962
        "d-M-yyyy",       // e.g., 10-8-1968
        "dd-M-yyyy",      // e.g., 10-08-1968
        "M/d/yyyy",       // e.g., 1/17/1968
        "yyyy",           // e.g., 2004
        "MM/dd/yyyy"      // e.g., 10/08/1968
    };
    DateTime parsedDate;

    
    if (DateTime.TryParseExact(dateText, formats, null, System.Globalization.DateTimeStyles.None, out parsedDate))
    {
        return parsedDate;
    }
    
    
    if (int.TryParse(dateText, out int year))
    {
        return new DateTime(year, 1, 1);
    }

    
    throw new CustomException($"Invalid date format: {dateText}");
}



private string GetCellTextOrNull(ExcelRange cell)
{
    return cell?.Text?.Trim();
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




           public async Task<List<Subscribers2024>> ImportSubscribersAsync(Stream stream)
    {
        List<Subscribers2024> subscribers = new List<Subscribers2024>();

        try
        {
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets["Sheet1"];
                if (worksheet == null)
                {
                    throw new CustomException("Worksheet not found in the Excel file.");
                }

                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // Assuming the first row is the header
                for (int row = 2; row <= rowCount; row++)
                {
                    var subscriber = new Subscribers2024
                    {
                        EnsuranceNumber = GetCellValue(worksheet, row, "الرقم التأميني"),
                        FullName = GetCellValue(worksheet, row, "الاسم"),
                        NationalId = GetCellValue(worksheet, row, "الرقم الوطني"),
                        BirthDate = ConvertToFullDate(GetCellValue(worksheet, row, "المواليد"))
                    };

                    subscribers.Add(subscriber);
                }
            }
        }
        catch (Exception ex)
        {
            throw new CustomException($"Error processing Excel file: {ex.Message}", ex);
        }

        return subscribers;
    }

    private string GetCellValue(ExcelWorksheet worksheet, int row, string columnName)
    {
        var columnIndex = worksheet.Cells["1:1"].FirstOrDefault(c => c.Value?.ToString().Equals(columnName, StringComparison.OrdinalIgnoreCase) == true)?.Start.Column;
        
        if (!columnIndex.HasValue)
        {
            throw new CustomException($"Column '{columnName}' not found in the Excel file.");
        }

        return worksheet.Cells[row, columnIndex.Value].Value?.ToString();
    }

    private DateTime? ConvertToFullDate(string yearString)
    {
        if (int.TryParse(yearString, out int year))
        {
            return new DateTime(year, 1, 1);
        }
        return null;
    }




    public async Task LoadSubToDatabase2024(List<Subscribers2024> list)
        {
            try
            {
                _dataContext.subscribers2024s.AddRange(list);
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






      public async Task<List<Person>> ReadExcelFileUnits(Stream fileStream)
{
    using var package = new ExcelPackage(fileStream);
    var worksheet = package.Workbook.Worksheets[0];
    var rowCount = worksheet.Dimension.Rows;
    var people = new List<Person>();

    for (int row = 2; row <= rowCount; row++) // Assuming row 1 is the header
    {
        try
        {
            // Read gender and convert to GenderId
            var genderText = worksheet.Cells[row, 9].Text.ToLower();
            int genderId = (genderText.Trim() == "ذكر" || genderText.Trim() == "male") ? 1 : 2;

            // Create Person object with basic data
            var person = new Person
            {
                FirstName = worksheet.Cells[row, 5].Text,
                FatherName = worksheet.Cells[row, 6].Text,
                LastName = worksheet.Cells[row, 7].Text,
                MotherName = worksheet.Cells[row, 8].Text,
                NationalId = worksheet.Cells[row, 12].Text.Length > 11 ? worksheet.Cells[row, 12].Text.Substring(0, 11) : worksheet.Cells[row, 12].Text,
                EnsuranceNumber = worksheet.Cells[row, 11].Text,
                Mobile = worksheet.Cells[row, 13].Text,
                GenderId = genderId, // Assign GenderId here
                //EngineeringUnitsId = 10
            };
    
           /*
            // Check for missing critical data (NationalId and EnsuranceNumber)
            if (string.IsNullOrWhiteSpace(person.NationalId) || string.IsNullOrWhiteSpace(person.EnsuranceNumber))
            {
                Console.WriteLine($"Skipping row {row} due to missing National ID or Insurance Number.");
                continue; // Skip this row
            }
           */


            // Parse birth date with multiple formats
            var birthDateText = worksheet.Cells[row, 14].Text.Trim();
            var dateFormats = new[] { "dd/MM/yyyy", "M/d/yyyy", "yyyy", "MMMM d, yyyy", "d/M/yyyy", "dd-MMM-yyyy" }; // Multiple formats

            if (DateTime.TryParseExact(birthDateText, dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var birthDate))
            {
                person.BirthDate = birthDate; // Store valid date
            }
            else
            {
                person.BirthDate = null; // Handle invalid or missing dates
            }

            // Create Engineer object
            var engineere = new Engineere
            {
                EngNumber = worksheet.Cells[row, 3].Text,
                SubNumber = worksheet.Cells[row, 4].Text,
                Person = person
            };

            // Log row data for debugging
            Console.WriteLine($"Processing row {row}: EngNumber = {engineere.EngNumber}, SubNumber = {engineere.SubNumber}, FirstName = {person.FirstName}, NationalId = {person.NationalId}");

            // Save person and engineer entities to the database
            _dataContext.Persons.Add(person);
            await _dataContext.SaveChangesAsync(); // Save person first

            _dataContext.Engineeres.Add(engineere);
            await _dataContext.SaveChangesAsync(); // Save engineer

            // Add to people list
            people.Add(person);
        }
        catch (DbUpdateException ex)
        {
            var innerExceptionMessage = ex.InnerException?.Message ?? "No additional details";
            throw new CustomException($"Database update error occurred while processing row {row}. Details: {innerExceptionMessage}", ex);
        }
        catch (Exception ex)
        {
            throw new CustomException($"Error occurred while processing row {row}", ex);
        }
    }

    return people;
}









 public async Task<List<Person>> ReadExcelFileUnits2(Stream fileStream)
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

    var rowCount = worksheet.Dimension.Rows;

    for (int row = 2; row <= rowCount; row++)
    {
        var genderText = GetCellTextOrNull(worksheet.Cells[row, 9]);
        int genderId = (genderText?.Trim() == "ذكر" || genderText?.Trim() == "Male") ? 1 : 2;

        
        // Attempt to parse the date in cell number 12
        var dateOfBirthText = GetCellTextOrNull(worksheet.Cells[row, 14]);
        DateTime? dateOfBirth = ParseDate2(dateOfBirthText);

        var person = new Person
        {
            FirstName = GetCellTextOrNull(worksheet.Cells[row, 5]),
            FatherName = GetCellTextOrNull(worksheet.Cells[row, 6]),
            LastName = GetCellTextOrNull(worksheet.Cells[row, 7]),
            MotherName = GetCellTextOrNull(worksheet.Cells[row, 8]),
            NationalId = GetCellTextOrNull(worksheet.Cells[row, 12])?.Length > 11 
                ? GetCellTextOrNull(worksheet.Cells[row, 12])?.Substring(0, 11) 
                : GetCellTextOrNull(worksheet.Cells[row, 12]),
            EnsuranceNumber = GetCellTextOrNull(worksheet.Cells[row, 11]),
            //Mobile = GetCellTextOrNull(worksheet.Cells[row, 15]),
            GenderId = genderId,
            BirthDate = dateOfBirth,
           // PayMethodId=2
        };

        var engineere = new Engineere
        {
            EngNumber = GetCellTextOrNull(worksheet.Cells[row, 3]),
            SubNumber = GetCellTextOrNull(worksheet.Cells[row, 4]),
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





private DateTime? ParseDate2(string dateText)
{
    if (string.IsNullOrWhiteSpace(dateText)) return null;

    // Add formats for parsing date and time
    string[] formats = { 
        "dd-MMM-yyyy",       // e.g., 06-Aug-1962
        "d-M-yyyy",          // e.g., 10-8-1968
        "dd-M-yyyy",         // e.g., 10-08-1968
        "M/d/yyyy",          // e.g., 1/17/1968
        "yyyy",              // e.g., 2004
        "MM/dd/yyyy",        // e.g., 10/08/1968
        "dd-MMM-yyyy HH:mm", // e.g., 17-Mar-1972 00:00
        "dd-MM-yyyy HH:mm"   // For potential other formats like 17-03-1972 00:00
    };
    
    DateTime parsedDate;

    // Try to parse the exact date and time using the specified formats
    if (DateTime.TryParseExact(dateText, formats, null, System.Globalization.DateTimeStyles.None, out parsedDate))
    {
        return parsedDate;
    }

    // Check if the input is a year and construct a date (January 1st of the given year)
    if (int.TryParse(dateText, out int year) && year >= 1900 && year <= DateTime.Now.Year)
    {
        return new DateTime(year, 1, 1);
    }

    // Log the invalid date format for debugging
    Console.WriteLine($"Invalid date format in the input: {dateText}. Row will be skipped or handled.");
    
    // Return null if the date is invalid
    return null;
}








        }
    
    }
