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
using static api.DTOS.PersonWithEngineereDTO;

namespace api.Repositories
{
    public class EngineerRepository : IEngineerRepository
    {
         private readonly DataContext _dataContext;

         private readonly IAnnualDataService _annualDataService;
         public EngineerRepository(DataContext dataContext, IAnnualDataService annualDataService)
         {
            _dataContext =dataContext;
            _annualDataService =annualDataService;
         }



        public async Task<int> Add(Engineere engineere)
{
    try
    {
        Engineere newEngineer = new Engineere()
        {
            EngNumber = engineere.EngNumber,
            SubNumber = engineere.SubNumber,
            Id = engineere.Id,
            SpecializationId = engineere.SpecializationId,
            WorkPlaceId = engineere.WorkPlaceId
        };

        _dataContext.Engineeres.Add(newEngineer);
        await _dataContext.SaveChangesAsync();

        return newEngineer.Id;
    }
    catch (DbUpdateException ex)
    {
        var sqlException = ex.InnerException as SqlException;
        if (sqlException != null)
        {
            // سجل معلومات إضافية حول استثناء SQL
            var errorMessage = $"SQL Error Number: {sqlException.Number}, Message: {sqlException.Message}, StackTrace: {sqlException.StackTrace}";
            throw new Exception($"Database update error: {errorMessage}", ex);
        }

        // سجل معلومات حول استثناء قاعدة البيانات
        throw new Exception("Database update error. See inner exception for details.", ex);
    }
    catch (Exception ex)
    {
        // سجل أي استثناء آخر
        throw new Exception("An unexpected error occurred. See inner exception for details.", ex);
    }
}


        



      public async Task<PersonWithEngineereDTO?> Get(int Id)
{
    return await _dataContext.Engineeres
        .Where(x => x.Id == Id)
        .Select(e => new PersonWithEngineereDTO
        {
            // خصائص جدول Person
            PersonId = e.Person.Id,
            FirstName = e.Person.FirstName,
            FatherName = e.Person.FatherName,
            LastName = e.Person.LastName,
            MotherName = e.Person.MotherName,
            NationalId = e.Person.NationalId,
            EnsuranceNumber = e.Person.EnsuranceNumber,
            BirthDate = e.Person.BirthDate,
            Address = e.Person.Address,
            Phone = e.Person.Phone,
            Mobile = e.Person.Mobile,
            Email = e.Person.Email,
            StatusId = e.Person.StatusId,
            GenderId = e.Person.GenderId,

            // خصائص جدول Engineere
            EngNumber = e.EngNumber,
            SubNumber = e.SubNumber,
            SpecializationId = e.SpecializationId,
            WorkPlaceId = e.WorkPlaceId,
            Amount = e.Person.Amount,

            // تضمين الصور المرتبطة
            Images = e.Person.Images.Select(i => Convert.ToBase64String(i.Image)).ToList(),

            // تضمين ملفات Word المرتبطة
            WordFiles = e.Person.Words.Select(w => Convert.ToBase64String(w.Content)).ToList()

        })
        .FirstOrDefaultAsync();
}









  public async Task<PagedResult<PersonWithEngineereDTO>> GetAll(int pageNumber, int pageSize)
{
    int skip = (pageNumber - 1) * pageSize;

    var query = @"
        WITH PagedData AS (
            SELECT e.Id, e.EngNumber, e.SubNumber, e.SpecializationId, e.WorkPlaceId, e.UserId,
                   ROW_NUMBER() OVER (ORDER BY e.Id) AS RowNumber
            FROM dbo.Engineeres e
        )
        SELECT * FROM PagedData
        WHERE RowNumber BETWEEN @startRow AND @endRow;
    ";

    var startRow = skip + 1;
    var endRow = skip + pageSize;

    var rawData = await _dataContext.Engineeres
        .FromSqlRaw(query, 
            new SqlParameter("@startRow", startRow), 
            new SqlParameter("@endRow", endRow))
        .ToListAsync();

    var items = new List<PersonWithEngineereDTO>();

    foreach (var item in rawData)
    {
        var person = await _dataContext.Persons.FirstOrDefaultAsync(p => p.Id == item.Id);

        var images = await _dataContext.Images
            .Where(img => img.PersonId == item.Id)
            .Select(img => Convert.ToBase64String(img.Image)) 
            .ToListAsync();


        var wordFiles = await _dataContext.Words
            .Where(wf => wf.PersonId == item.Id)
            .Select(wf => Convert.ToBase64String(wf.Content))  
            .ToListAsync();

        if (person != null)
        {
            items.Add(new PersonWithEngineereDTO
            {
                PersonId = person.Id,
                FirstName = person.FirstName,
                FatherName = person.FatherName,
                LastName = person.LastName,
                MotherName = person.MotherName,
                NationalId = person.NationalId,
                EnsuranceNumber = person.EnsuranceNumber,
                BirthDate = person.BirthDate,
                Address = person.Address,
                Phone = person.Phone,
                Mobile = person.Mobile,
                Email = person.Email,
                StatusId = person.StatusId,
                GenderId = person.GenderId,
                EngNumber = item.EngNumber,
                SubNumber = item.SubNumber,
                SpecializationId = item.SpecializationId,
                WorkPlaceId = item.WorkPlaceId,
                Amount = person.Amount,
                Images = images,
                WordFiles = wordFiles
            });
        }
    }

    var totalCount = await _dataContext.Engineeres.CountAsync();

    return new PagedResult<PersonWithEngineereDTO>
    {
        CurrentPage = pageNumber,
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
        PageSize = pageSize,
        TotalCount = totalCount,
        Items = items
    };
}











          public bool Update(int Id, EngineerPersonEditDTO engineerPersonEditDTO)
{
    // حساب المبلغ الجديد بناءً على تاريخ الميلاد الجديد
    var amount = _annualDataService.calcualteAmount(engineerPersonEditDTO.BirthDate, 2024);

    // البحث عن المهندس في قاعدة البيانات
    var engineerEntity = _dataContext.Engineeres.FirstOrDefault(x => x.Id == Id);
    if (engineerEntity == null)
    {
        return false;
    }

    // تحديث الحقول الخاصة بالمهندس
    engineerEntity.EngNumber = engineerPersonEditDTO.EngNumber;
    engineerEntity.SubNumber = engineerPersonEditDTO.SubNumber;
    engineerEntity.SpecializationId = engineerPersonEditDTO.SpecializationId;
    engineerEntity.WorkPlaceId = engineerPersonEditDTO.WorkPlaceId;
    

    // البحث عن الشخص في قاعدة البيانات باستخدام نفس المعرف
    var personEntity = _dataContext.Persons.FirstOrDefault(x => x.Id == Id);
    if (personEntity == null)
    {
        return false;
    }

    // تحديث الحقول الخاصة بالشخص
    personEntity.FirstName = engineerPersonEditDTO.FirstName;
    personEntity.FatherName = engineerPersonEditDTO.FatherName;
    personEntity.LastName = engineerPersonEditDTO.LastName;
    personEntity.MotherName = engineerPersonEditDTO.MotherName;
    personEntity.BirthDate = engineerPersonEditDTO.BirthDate;
    personEntity.NationalId = engineerPersonEditDTO.NationalId;
    personEntity.EnsuranceNumber = engineerPersonEditDTO.EnsuranceNumber;
    personEntity.Address = engineerPersonEditDTO.Address;
    personEntity.Phone = engineerPersonEditDTO.Phone;
   
    personEntity.GenderId = engineerPersonEditDTO.GenderId;
    personEntity.StatusId = engineerPersonEditDTO.statusId;
    personEntity.Amount = amount; // تحديث المبلغ الجديد

    // حفظ التغييرات في قاعدة البيانات
    return _dataContext.SaveChanges() > 0;
}




              public void DeleteByEngId(int EngineereId){
         var rest=   _dataContext.Relations.Where(x=>x.EngineereId==EngineereId).ToList();
         if(rest!=null){
            _dataContext.Relations.RemoveRange(rest);
            _dataContext.SaveChanges();
         }}



            public void DeleteByEngId2(int Id){
         var rest=   _dataContext.Persons.Where(x=>x.Id==Id).ToList();
         if(rest!=null){
            _dataContext.Persons.RemoveRange(rest);
            _dataContext.SaveChanges();
         }}
           


           public void Delete(int Id){
            var result = _dataContext.Engineeres.Where(x=>x.Id==Id).ToList();
            if (result!=null){
                 _dataContext.Engineeres.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }

    }
}