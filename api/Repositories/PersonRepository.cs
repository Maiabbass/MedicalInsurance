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


        public PersonRepository(DataContext dataContext , IAnnualDataService nnualDataService)
        {
            _dataContext = dataContext;
            _nnualDataService = nnualDataService;
        }

       
        public async Task<int> Add(Person person)
    {
        try
        {
            var Amount = _nnualDataService.calcualteAmount(person.BirthDate,2024);
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
                StatusId=person.StatusId,
                Amount=Amount,
            };

            _dataContext.Persons.Add(newPerson);
            await _dataContext.SaveChangesAsync();

            return newPerson.Id;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            throw new Exception("Duplicate entry detected for unique index or constraint.", sqlEx);
        }
    }

     
        public async Task<Person?> Get(int Id)
        {
            return await _dataContext.Persons.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }



        public async Task<PagedResult<Person>> GetAll(int pageNumber, int pageSize)
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

    var query = @"
        SELECT *
        FROM (
            SELECT 
                ROW_NUMBER() OVER (ORDER BY Id) AS RowNum,
                *
            FROM Persons
        ) AS Result
        WHERE RowNum > @Skip AND RowNum <= @Skip + @Take
        ORDER BY RowNum";

    var items = await _dataContext.Persons
        .FromSqlRaw(query, new SqlParameter("@Skip", skip), new SqlParameter("@Take", take))
        .ToListAsync();

    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

    return new PagedResult<Person>
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





    
 public bool Update(int Id, PersonEditDTO PersonEditDTO)
{
    // البحث عن الشخص في قاعدة البيانات
    var databaseEntity = _dataContext.Persons.FirstOrDefault(x => x.Id == Id);
    if (databaseEntity == null)
    {
        return false; // في حال عدم وجود الشخص، قم بإرجاع false
    }

    // حساب المبلغ بناءً على تاريخ الميلاد الجديد
    var Amount = _nnualDataService.calcualteAmount(PersonEditDTO.BirthDate, 2024);
    
    // تحديث الحقول الخاصة بالشخص
    databaseEntity.FirstName = PersonEditDTO.FirstName;
    databaseEntity.FatherName = PersonEditDTO.FatherName;
    databaseEntity.LastName = PersonEditDTO.LastName;
    databaseEntity.MotherName = PersonEditDTO.MotherName;
    databaseEntity.BirthDate = PersonEditDTO.BirthDate;
    databaseEntity.NationalId = PersonEditDTO.NationalId;
    databaseEntity.EnsuranceNumber = PersonEditDTO.EnsuranceNumber;
    databaseEntity.Address = PersonEditDTO.Address;
    databaseEntity.Phone = PersonEditDTO.Phone;
   
    databaseEntity.GenderId = PersonEditDTO.GenderId;
    databaseEntity.StatusId = PersonEditDTO.StatusId;
    
    // تحديث المبلغ الجديد
    databaseEntity.Amount = Amount;

    // حفظ التغييرات في قاعدة البيانات
    return _dataContext.SaveChanges() > 0;
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