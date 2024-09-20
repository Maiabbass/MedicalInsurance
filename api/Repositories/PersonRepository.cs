using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using api.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PersonRepository : IPersonRepository
    {

        private readonly DataContext _dataContext;
        private readonly IAnnualDataService _nnualDataService;
      


        public PersonRepository(DataContext dataContext , IAnnualDataService nnualDataService ) 
        {
            _dataContext = dataContext;
            _nnualDataService = nnualDataService;
           
        }

       
       public async Task<int> AddPerson(Person person, IFormFile[] imageFiles, IFormFile[] wordFiles)
{
    try
    {
        var amount = _nnualDataService.calcualteAmount(person.BirthDate, 2024);

        // إنشاء كائن جديد من نوع Person
        Person newPerson = new Person()
        {
            FirstName = person.FirstName,
            FatherName = person.FatherName,
            MotherName = person.MotherName,
            LastName = person.LastName,
            BirthDate = person.BirthDate,
            NationalId = person.NationalId,
            EnsuranceNumber = person.EnsuranceNumber,
            Address = person.Address,
            Phone = person.Phone,
            Mobile = person.Mobile,
            Email = person.Email,
            GenderId = person.GenderId,
            StatusId = person.StatusId,
            Amount = amount,
        };

        // إضافة الشخص الجديد إلى قاعدة البيانات
        _dataContext.Persons.Add(newPerson);
        await _dataContext.SaveChangesAsync();

        // حفظ الصور إذا كانت موجودة
        if (imageFiles != null && imageFiles.Length > 0)
        {
            foreach (var imageFile in imageFiles)
            {
                if (imageFile.Length > 0)
                {
                    var image = new Images
                    {
                        Image = await ConvertFileToByteArray(imageFile),
                        PersonId = newPerson.Id
                    };
                    _dataContext.Images.Add(image);
                }
            }
        }

        // حفظ ملفات Word إذا كانت موجودة
        if (wordFiles != null && wordFiles.Length > 0)
        {
            foreach (var wordFile in wordFiles)
            {
                if (wordFile.Length > 0)
                {
                    var word = new Words
                    {
                        Content = await ConvertFileToByteArray(wordFile),
                        PersonId = newPerson.Id
                    };
                    _dataContext.Words.Add(word);
                }
            }
        }

        // حفظ التغييرات في قاعدة البيانات
        await _dataContext.SaveChangesAsync();

        return newPerson.Id;
    }
    catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
    {
        throw new Exception("Duplicate entry detected for unique index or constraint.", sqlEx);
    }
}





    public async Task<byte[]> ConvertFileToByteArray(IFormFile file)
{
    if (file == null || file.Length == 0)
    {
        return null;
    }

    using (var memoryStream = new MemoryStream())
    {
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}


     
        public async Task<Person?> Get(int Id)
{
    return await _dataContext.Persons
        .Include(x => x.Images) // تضمين الصور
        .Include(x => x.Words) // تضمين ملفات Word
        .Where(x => x.Id == Id)
        .FirstOrDefaultAsync();
}



      public async Task<PagedResult<PersonForView>> GetAll(int pageNumber, int pageSize)
{
    // Ensure pageNumber and pageSize are valid
    if (pageNumber <= 0)
        pageNumber = 1;
    if (pageSize <= 0)
        pageSize = 10;

    var totalCount = await _dataContext.Persons.CountAsync();

    // Calculate the range of rows to fetch
    int skip = (pageNumber - 1) * pageSize;
    int take = pageSize;

    var persons = await _dataContext.Persons
        .Skip(skip)
        .Take(take)
        .Include(p => p.Images)
        .Include(p => p.Words)
        .ToListAsync();

    var items = persons.Select(person => new PersonForView
    {
        Id = person.Id,
        FirstName = person.FirstName,
        FatherName = person.FatherName,
        LastName = person.LastName,
        MotherName = person.MotherName,
        BirthDate = person.BirthDate,
        NationalId = person.NationalId,
        EnsuranceNumber = person.EnsuranceNumber,
        Address = person.Address,
        Phone = person.Phone,
        Mobile = person.Mobile,
        Email = person.Email,
        StatusId = person.StatusId,
        GenderId = person.GenderId,
        Images = person.Images.Select(img => Convert.ToBase64String(img.Image)).ToList(), // تحويل كل ImageData إلى Base64
        WordFiles = person.Words.Select(wf => Convert.ToBase64String(wf.Content)).ToList() // تحويل كل Base64Data إلى Base64
    }).ToList();

    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

    return new PagedResult<PersonForView>
    {
        Items = items,
        TotalCount = totalCount,
        TotalPages = totalPages,
        CurrentPage = pageNumber,
        PageSize = pageSize
    };
}



        public void   Delete(int Id)
        {
            
            var rest = _dataContext.Persons.FirstOrDefault(x=>x.Id==Id);
            if(rest!=null)
            {
                _dataContext.Persons.Remove(rest);
                _dataContext.SaveChanges();
            }
        }




        public Task<int> Add(City city)
        {
            throw new NotImplementedException();
        }







 public async Task<bool> UpdatePersonDetails(int id, PersonEditDTO personEditDTO)
{
    var databaseEntity = await _dataContext.Persons
        .FirstOrDefaultAsync(x => x.Id == id);

    if (databaseEntity == null)
    {
        return false; // في حال عدم وجود الشخص، قم بإرجاع false
    }

    // تحديث الحقول الخاصة بالشخص
    databaseEntity.FirstName = personEditDTO.FirstName;
    databaseEntity.FatherName = personEditDTO.FatherName;
    databaseEntity.LastName = personEditDTO.LastName;
    databaseEntity.MotherName = personEditDTO.MotherName;
    databaseEntity.BirthDate = personEditDTO.BirthDate;
    databaseEntity.NationalId = personEditDTO.NationalId;
    databaseEntity.EnsuranceNumber = personEditDTO.EnsuranceNumber;
    databaseEntity.Address = personEditDTO.Address;
    databaseEntity.Phone = personEditDTO.Phone;
    databaseEntity.Mobile = personEditDTO.Mobile;
    databaseEntity.Email = personEditDTO.Email;
    databaseEntity.GenderId = personEditDTO.GenderId;
    databaseEntity.StatusId = personEditDTO.StatusId;

    // حفظ التغييرات الخاصة بالشخص
    return await _dataContext.SaveChangesAsync() > 0;
}





 



       public async Task<AnnualData?> GetEngId(int EngineereId) {
        return await _dataContext.AnnualDatas.Where(x=>x.Id==EngineereId).FirstOrDefaultAsync();

       }


      public async Task<bool> IsEnsuranceNumberInClaimsAsync(string ensuranceNumber)
{
    return await _dataContext.Claims.AnyAsync(c => c.EnsuranceNumber == ensuranceNumber);
}




public class PersonConverter
{
    public static Person ConvertToPerson(PersonWithEngineereDTO dto)
    {
        if (dto == null)
            return null;

        return new Person
        {
            EnsuranceNumber = dto.EnsuranceNumber,
            Amount = dto.Amount,
            // تحويل الخصائص الأخرى إذا كانت موجودة
        };
    }

    public static PersonWithEngineereDTO ConvertToDTO(Person person)
    {
        if (person == null)
            return null;

        return new PersonWithEngineereDTO
        {
            EnsuranceNumber = person.EnsuranceNumber,
            Amount = person.Amount,
            // تحويل الخصائص الأخرى إذا كانت موجودة
        };
    }
}



 
public async Task SavePerson(PersonWithEngineereDTO dto)
{
    try
    {
        var person = PersonConverter.ConvertToPerson(dto);

        var existingPerson = await _dataContext.Persons
            .FirstOrDefaultAsync(p => p.EnsuranceNumber == person.EnsuranceNumber);

        if (existingPerson != null)
        {
            // تحديث السجل الحالي
            existingPerson.Amount = person.Amount;
            _dataContext.Persons.Update(existingPerson);
        }
        else
        {
        
            _dataContext.Persons.Add(person);
        }

        await _dataContext.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        // تسجيل الاستثناء أو تنفيذ أي تنظيف ضروري
        string errorMessage = "An error occurred while saving the entity changes.";
        if (ex.InnerException != null)
        {
            errorMessage += " Inner Exception: " + ex.InnerException.Message;
        }

        throw new Exception(errorMessage, ex);
    }
}


  

    }
}