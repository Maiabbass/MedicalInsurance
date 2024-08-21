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


        



        public async Task<Engineere?> Get(int Id)
{
    return await _dataContext.Engineeres
        .Include(e => e.Person)  // تضمين بيانات الـ Person المرتبطة
        .FirstOrDefaultAsync(x => x.Id == Id);
}






        
    public async Task<IEnumerable<EngineerFull>> GetAll(int pageNumber, int pageSize)
{
    int skip = (pageNumber - 1) * pageSize;
    int take = pageSize;

    // استعلام SQL لاستخدام ROW_NUMBER لتطبيق Paging على Engineers فقط
    var query = @"
        WITH PagedData AS (
            SELECT e.Id, e.EngNumber, e.SubNumber, e.SpecializationId, e.WorkPlaceId, e.UserId,
                   ROW_NUMBER() OVER (ORDER BY e.Id) AS RowNumber
            FROM dbo.Engineeres e  -- تأكد من استخدام الاسم الكامل للجدول
        )
        SELECT * FROM PagedData
        WHERE RowNumber BETWEEN @startRow AND @endRow;
    ";

    var startRow = skip + 1;
    var endRow = skip + take;

    var rawData = await _dataContext.Engineeres
        .FromSqlRaw(query, 
            new SqlParameter("@startRow", startRow), 
            new SqlParameter("@endRow", endRow))
        .ToListAsync();

    // تحويل البيانات إلى النموذج المطلوب (PersonWithEngineerDTO) مع تحميل التفاصيل لاحقًا
    var result = new List<EngineerFull>();

    foreach (var item in rawData)
    {
        var persons = await _dataContext.Persons
            .Where(p => p.Id == item.Id)
            .ToListAsync();

        result.Add(new EngineerFull
        {
            Id = item.Id,
            EngNumber = item.EngNumber,
            SubNumber = item.SubNumber,
            SpecializationId = item.SpecializationId.HasValue ? item.SpecializationId.Value : 0, // تحقق من القيمة nullable
            WorkPlaceId = item.WorkPlaceId.HasValue ? item.WorkPlaceId.Value : 0,               // تحقق من القيمة nullable
            
            Persons = persons.Select(p => new Person
            {
                Id = p.Id,
                FirstName = p.FirstName,
                FatherName = p.FatherName,
                LastName = p.LastName,
                MotherName = p.MotherName,
                BirthDate = p.BirthDate,
                NationalId = p.NationalId,
                EnsuranceNumber = p.EnsuranceNumber,
                Address = p.Address,
                Phone = p.Phone,
                Mobile = p.Mobile,
                Email = p.Email,
                GenderId = p.GenderId,
                Gender = p.Gender,
                Amount = p.Amount
                // يجب إدراج الخصائص الأخرى إذا كانت موجودة
            }).ToList()
        });
    }

    return result;
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