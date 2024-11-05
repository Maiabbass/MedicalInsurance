using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{ 
    public class QuiriesRepositories :  IQuiriesRepositories
    {
        
         private readonly DataContext _dataContext;

         public QuiriesRepositories(DataContext dataContext)
         {
            _dataContext=dataContext;
         }




 public async Task<SimpleEngineer> GetEngineerWithRelationsAsync(string EngNumber)
{
    var engineer = await _dataContext.Engineeres
        .Include(e => e.Person)
            .ThenInclude(p => p.Images) // جلب الصور المرتبطة بالشخص
        .Include(e => e.Person)
            .ThenInclude(p => p.Words) // جلب الملفات المرتبطة بالشخص من جدول Words
        .Include(e => e.Relations)
            .ThenInclude(r => r.Person)
                .ThenInclude(p => p.Images) // جلب الصور المرتبطة بالعلاقات
        .Include(e => e.Relations)
            .ThenInclude(r => r.Person)
                .ThenInclude(p => p.Words) // جلب الملفات المرتبطة بالعلاقات من جدول Words
        .FirstOrDefaultAsync(e => e.EngNumber == EngNumber);

    if (engineer == null)
        return null;

    decimal? totalAmount = (engineer.Person.Amount ?? 0) + engineer.Relations.Sum(r => r.Person.Amount ?? 0);

    var simpleEngineer = new SimpleEngineer
    {
        Id = engineer.Id,
        EngNumber = engineer.EngNumber,
        SubNumber = engineer.SubNumber,
        Person = new SimplePerson
        {
            Id = engineer.Person.Id,
            FirstName = engineer.Person.FirstName,
            FatherName = engineer.Person.FatherName,
            LastName = engineer.Person.LastName,
            MotherName = engineer.Person.MotherName,
            NationalId = engineer.Person.NationalId,
            EnsuranceNumber = engineer.Person.EnsuranceNumber,
            Address = engineer.Person.Address,
            Phone = engineer.Person.Phone,
            Mobile = engineer.Person.Mobile,
            Email = engineer.Person.Email,
            StatusId = engineer.Person.StatusId ?? 0, // تعيين قيمة افتراضية إذا كان StatusId فارغًا
            GenderId = engineer.Person.GenderId,
            Amount = engineer.Person.Amount ?? 0, // تعيين قيمة افتراضية إذا كان Amount فارغًا
            BirthDate = engineer.Person.BirthDate, // هنا يمكن تركه كما هو إذا لم يكن هناك حاجة لقيمة افتراضية
            Images = engineer.Person.Images.Select(img => new ImageDTO
            {
                Id = img.Id,
                FileName = img.Image // جلب البيانات على شكل Byte
            }).ToList(),
            Words = engineer.Person.Words.Select(doc => new WordDTO
            {
                Id = doc.Id,
                FileName = doc.Content // جلب البيانات على شكل Byte
            }).ToList()
        },
        Relations = engineer.Relations.Select(r => new SimpleRelation
        {
            Id = r.Id,
            Name = r.Name,
            RelationTypeId = r.RelationTypeId,
            Person = new SimplePerson
            {
                Id = r.Person.Id,
                FirstName = r.Person.FirstName,
                FatherName = r.Person.FatherName,
                LastName = r.Person.LastName,
                MotherName = r.Person.MotherName,
                NationalId = r.Person.NationalId,
                EnsuranceNumber = r.Person.EnsuranceNumber,
                Address = r.Person.Address,
                Phone = r.Person.Phone,
                Mobile = r.Person.Mobile,
                Email = r.Person.Email,
                StatusId = r.Person.StatusId ?? 0, // تعيين قيمة افتراضية إذا كان StatusId فارغًا
                GenderId = r.Person.GenderId,
                Amount = r.Person.Amount ?? 0, // تعيين قيمة افتراضية إذا كان Amount فارغًا
                BirthDate = r.Person.BirthDate, // هنا يمكن تركه كما هو إذا لم يكن هناك حاجة لقيمة افتراضية
                Images = r.Person.Images.Select(img => new ImageDTO
                {
                    Id = img.Id,
                    FileName = img.Image
                }).ToList(),
                Words = r.Person.Words.Select(doc => new WordDTO
                {
                    Id = doc.Id,
                    FileName = doc.Content
                }).ToList()
            }
        }).ToList(),
        TotalRelationsAmount = (decimal)totalAmount
    };

    return simpleEngineer;
}









           public async Task<IEnumerable<SimpleEngineer>> GetEngineers(int? workPlaceId ,int? specializationId , int? engineeringUnitsId , int? PayMethodId  )
        {

            if (workPlaceId != null && specializationId == null && engineeringUnitsId == null  && PayMethodId == null)
            {

            var query = _dataContext.Engineeres
                .Include(e => e.Person)
                .Include(e => e.WorkPlace)
                
                
                .AsQueryable();

      var engineers = await query.ToListAsync();

            // تحويل الكائنات إلى كائنات بسيطة
            var simpleEngineers = engineers.Select(e => new SimpleEngineer
            {
                Id = e.Id,
                EngNumber = e.EngNumber,
                SubNumber = e.SubNumber,
                Person = new SimplePerson
                {
                    Id = e.Person.Id,
                    FirstName = e.Person.FirstName,
                    FatherName = e.Person.FatherName,
                    LastName = e.Person.LastName,
                    MotherName = e.Person.MotherName,
                    NationalId = e.Person.NationalId,
                    EnsuranceNumber = e.Person.EnsuranceNumber,
                    Address = e.Person.Address,
                    Phone = e.Person.Phone,
                    Mobile = e.Person.Mobile,
                    Email = e.Person.Email,
                    
                    StatusId= (int)e.Person.StatusId,
                    GenderId = e.Person.GenderId,
                    Amount= (decimal)e.Person.Amount,
                    BirthDate=e.Person.BirthDate,

                },
            
                
               
            }).ToList();

            return simpleEngineers;
        }
        
        else if (workPlaceId == null && specializationId != null && engineeringUnitsId == null && PayMethodId == null  ){
            var query = _dataContext.Engineeres
                .Include(e => e.Person)
                .Include(e => e.Specialization)
                .AsQueryable();

      var engineers = await query.ToListAsync();

            // تحويل الكائنات إلى كائنات بسيطة
            var simpleEngineers = engineers.Select(e => new SimpleEngineer
            {
                Id = e.Id,
                EngNumber = e.EngNumber,
                SubNumber = e.SubNumber,
                Person = new SimplePerson
                {
                    Id = e.Person.Id,
                    FirstName = e.Person.FirstName,
                    FatherName = e.Person.FatherName,
                    LastName = e.Person.LastName,
                    MotherName = e.Person.MotherName,
                    NationalId = e.Person.NationalId,
                    EnsuranceNumber = e.Person.EnsuranceNumber,
                    Address = e.Person.Address,
                    Phone = e.Person.Phone,
                    Mobile = e.Person.Mobile,
                    Email = e.Person.Email,
                    StatusId= (int)e.Person.StatusId,
                    
                    GenderId = e.Person.GenderId,
                    Amount= (decimal)e.Person.Amount,
                    BirthDate=e.Person.BirthDate,

                },
            
                
               
            }).ToList();

            return simpleEngineers;}

            else if (workPlaceId == null && specializationId == null && engineeringUnitsId != null && PayMethodId == null){
                
                 var query = _dataContext.Engineeres
                .Include(e => e.Person)
                .Where(e => e.AnnualDatas.Any(ad => ad.EngineeringUnitsId == engineeringUnitsId))
                .AsQueryable();

    var engineers = await query.ToListAsync();

    // تحويل الكائنات إلى كائنات بسيطة
    var simpleEngineers = engineers.Select(e => new SimpleEngineer
    {
        Id = e.Id,
        EngNumber = e.EngNumber,
        SubNumber = e.SubNumber,
        Person = new SimplePerson
        {
            Id = e.Person.Id,
            FirstName = e.Person.FirstName,
            FatherName = e.Person.FatherName,
            LastName = e.Person.LastName,
            MotherName = e.Person.MotherName,
            NationalId = e.Person.NationalId,
            EnsuranceNumber = e.Person.EnsuranceNumber,
            Address = e.Person.Address,
            Phone = e.Person.Phone,
            Mobile = e.Person.Mobile,
            Email = e.Person.Email,
            StatusId= (int)e.Person.StatusId,
           
            GenderId = e.Person.GenderId,
            Amount= (decimal)e.Person.Amount,
            BirthDate=e.Person.BirthDate,

        }
    }).ToList();

    return simpleEngineers;
}


          else if (workPlaceId == null && specializationId == null && engineeringUnitsId == null && PayMethodId != null){
                
                 var query = _dataContext.Engineeres
                .Include(e => e.Person)
                .Where(e => e.AnnualDatas.Any(ad => ad.PayMethodId == PayMethodId))
                .AsQueryable();

    var engineers = await query.ToListAsync();

    // تحويل الكائنات إلى كائنات بسيطة
    var simpleEngineers = engineers.Select(e => new SimpleEngineer
    {
        Id = e.Id,
        EngNumber = e.EngNumber,
        SubNumber = e.SubNumber,
        Person = new SimplePerson
        {
            Id = e.Person.Id,
            FirstName = e.Person.FirstName,
            FatherName = e.Person.FatherName,
            LastName = e.Person.LastName,
            MotherName = e.Person.MotherName,
            NationalId = e.Person.NationalId,
            EnsuranceNumber = e.Person.EnsuranceNumber,
            Address = e.Person.Address,
            Phone = e.Person.Phone,
            Mobile = e.Person.Mobile,
            Email = e.Person.Email,
            StatusId= (int)e.Person.StatusId,
           
            GenderId = e.Person.GenderId,
            Amount= (decimal)e.Person.Amount,
            BirthDate=e.Person.BirthDate,
        }
    }).ToList();

    return simpleEngineers;
}

    
     else if (workPlaceId != null && specializationId == null && engineeringUnitsId != null && PayMethodId == null){
                
        var query = _dataContext.Engineeres
                .Include(e => e.Person)
                .Where(e => e.AnnualDatas.Any(ad => ad.EngineeringUnitsId == engineeringUnitsId) && e.WorkPlaceId == workPlaceId)
                .AsQueryable();

    var engineers = await query.ToListAsync();

    // تحويل الكائنات إلى كائنات بسيطة
    var simpleEngineers = engineers.Select(e => new SimpleEngineer
    {
        Id = e.Id,
        EngNumber = e.EngNumber,
        SubNumber = e.SubNumber,
        Person = new SimplePerson
        {
            Id = e.Person.Id,
            FirstName = e.Person.FirstName,
            FatherName = e.Person.FatherName,
            LastName = e.Person.LastName,
            MotherName = e.Person.MotherName,
            NationalId = e.Person.NationalId,
            EnsuranceNumber = e.Person.EnsuranceNumber,
            Address = e.Person.Address,
            Phone = e.Person.Phone,
            Mobile = e.Person.Mobile,
            Email = e.Person.Email,
            StatusId= (int)e.Person.StatusId,
           
            GenderId = e.Person.GenderId,
            Amount= (decimal)e.Person.Amount,
            BirthDate=e.Person.BirthDate,
        },
    
    }).ToList();
 
    return simpleEngineers;
}

 else if (workPlaceId == null && specializationId == null && engineeringUnitsId != null && PayMethodId != null){
                
         var query = _dataContext.Engineeres
                        .Include(e => e.Person)
                        .Include(e => e.AnnualDatas)
                        .ThenInclude(ad => ad.PayMethod)
                        .Where(e => e.AnnualDatas.Any(ad => ad.EngineeringUnitsId == engineeringUnitsId && ad.PayMethodId == PayMethodId))
                        .AsQueryable();

            var engineers = await query.ToListAsync();

            // تحويل الكائنات إلى كائنات بسيطة
            var simpleEngineers = engineers.Select(e => new SimpleEngineer
            {
                Id = e.Id,
                EngNumber = e.EngNumber,
                SubNumber = e.SubNumber,
                Person = new SimplePerson
                {
                    Id = e.Person.Id,
                    FirstName = e.Person.FirstName,
                    FatherName = e.Person.FatherName,
                    LastName = e.Person.LastName,
                    MotherName = e.Person.MotherName,
                    NationalId = e.Person.NationalId,
                    EnsuranceNumber = e.Person.EnsuranceNumber,
                    Address = e.Person.Address,
                    Phone = e.Person.Phone,
                    Mobile = e.Person.Mobile,
                    Email = e.Person.Email,
                    StatusId= (int)e.Person.StatusId,
                    GenderId = e.Person.GenderId,
                    Amount= (decimal)e.Person.Amount,
                    BirthDate=e.Person.BirthDate,
                },
               
            }).ToList();
            return simpleEngineers ;

 }

       else if (workPlaceId != null && specializationId == null && engineeringUnitsId == null && PayMethodId != null){

      var query = _dataContext.Engineeres
                        .Include(e => e.Person)
                        .Include(e => e.WorkPlace)
                        .Include(e => e.AnnualDatas)
                        .ThenInclude(ad => ad.PayMethod)
                        .Where(e => e.WorkPlaceId == workPlaceId && e.AnnualDatas.Any(ad => ad.PayMethodId == PayMethodId))
                        .AsQueryable();

            var engineers = await query.ToListAsync();

            // تحويل الكائنات إلى كائنات بسيطة
            var simpleEngineers = engineers.Select(e => new SimpleEngineer
            {
                Id = e.Id,
                EngNumber = e.EngNumber,
                SubNumber = e.SubNumber,
                Person = new SimplePerson
                {
                    Id = e.Person.Id,
                    FirstName = e.Person.FirstName,
                    FatherName = e.Person.FatherName,
                    LastName = e.Person.LastName,
                    MotherName = e.Person.MotherName,
                    NationalId = e.Person.NationalId,
                    EnsuranceNumber = e.Person.EnsuranceNumber,
                    Address = e.Person.Address,
                    Phone = e.Person.Phone,
                    Mobile = e.Person.Mobile,
                    Email = e.Person.Email,
                    StatusId= (int)e.Person.StatusId,
                    
                    GenderId = e.Person.GenderId,
                    Amount= (decimal)e.Person.Amount,
                    BirthDate=e.Person.BirthDate,
                },
               
               
            }).ToList();

           return simpleEngineers;}

           else if (workPlaceId != null && specializationId != null && engineeringUnitsId != null && PayMethodId != null){
             var query = _dataContext.Engineeres
                        .Include(e => e.Person)
                        .Include(e => e.WorkPlace)
                        .Include(e => e.Specialization)
                        .Include(e => e.AnnualDatas)
                        .ThenInclude(ad => ad.PayMethod)
                        .Where(e => e.AnnualDatas.Any(ad => ad.EngineeringUnitsId == engineeringUnitsId && ad.PayMethodId == PayMethodId)
                                    && e.WorkPlaceId == workPlaceId
                                    && e.SpecializationId == specializationId)
                        .AsQueryable();

            var engineers = await query.ToListAsync();

            // تحويل الكائنات إلى كائنات بسيطة
            var simpleEngineers = engineers.Select(e => new SimpleEngineer
            {
                Id = e.Id,
                EngNumber = e.EngNumber,
                SubNumber = e.SubNumber,
                Person = new SimplePerson
                {
                    Id = e.Person.Id,
                    FirstName = e.Person.FirstName,
                    FatherName = e.Person.FatherName,
                    LastName = e.Person.LastName,
                    MotherName = e.Person.MotherName,
                    NationalId = e.Person.NationalId,
                    EnsuranceNumber = e.Person.EnsuranceNumber,
                    Address = e.Person.Address,
                    Phone = e.Person.Phone,
                    Mobile = e.Person.Mobile,
                    Email = e.Person.Email,
                    StatusId= (int)e.Person.StatusId,
                    
                    GenderId = e.Person.GenderId,
                    Amount= (decimal)e.Person.Amount,
                    BirthDate=e.Person.BirthDate,
                },
               
                                         
            }).ToList();
            return simpleEngineers;
           }


           else{
            return null ;

           }
    }



    public async Task<List<Person>> GetPersonsByAgeSegment(int fromYear, int toYear)
{
    var currentDate = DateTime.Now;

    // حساب تاريخ الميلاد بناءً على الفئة العمرية المطلوبة
    var fromBirthDate = currentDate.AddYears(-toYear); // العمر الأكبر
    var toBirthDate = currentDate.AddYears(-fromYear); // العمر الأصغر

    // استعلام للحصول على الأشخاص الذين يقع تاريخ ميلادهم ضمن النطاق المطلوب
    var personsInAgeSegment = await _dataContext.Persons
        .Where(p => p.BirthDate >= fromBirthDate && p.BirthDate <= toBirthDate)
        .ToListAsync();

    return personsInAgeSegment;
}






  public async Task<EngineerFamilyFullDataDTO> GetEngineerWithFamilyAnnualFullData(string engineerNumber, int year)
{
    var engineerWithFamilyData = new EngineerFamilyFullDataDTO();

    // التحقق من وجود بيانات سنوية للمهندس للعام المحدد
    var engineerData = await _dataContext.AnnualDatas
        .Where(ad => ad.Engineere.EngNumber == engineerNumber && ad.Year == year)
        .Include(ad => ad.Engineere)
        .Include(ad => ad.Engineere.Person)
        .Include(ad => ad.Engineere.Specialization)  // تضمين الاختصاص
        .Include(ad => ad.Engineere.WorkPlace)       // تضمين مكان العمل
        .Include(ad => ad.Engineere.Person.Images)   // تضمين الصور
        .Include(ad => ad.Engineere.Person.Words)    // تضمين الملفات
        .FirstOrDefaultAsync();

    if (engineerData == null)
    {
        return null; // في حال لم يتم العثور على بيانات سنوية للمهندس
    }

    // تعيين كافة البيانات الخاصة بالمهندس
    engineerWithFamilyData.EngineerInfo = new FullPersonDataDTO
    {
        FirstName = engineerData.Engineere.Person.FirstName,
        LastName = engineerData.Engineere.Person.LastName,
        FatherName = engineerData.Engineere.Person.FatherName,
        MotherName = engineerData.Engineere.Person.MotherName,
        BirthDate = engineerData.Engineere.Person.BirthDateFormatted,
        NationalId = engineerData.Engineere.Person.NationalId,
        EnsuranceNumber = engineerData.Engineere.Person.EnsuranceNumber,
        Address = engineerData.Engineere.Person.Address,
        Phone = engineerData.Engineere.Person.Phone,
        Mobile = engineerData.Engineere.Person.Mobile,
        Email = engineerData.Engineere.Person.Email,
        Specialization = engineerData.Engineere.Specialization?.Name,
        WorkPlace = engineerData.Engineere.WorkPlace?.Name,
        Amount = engineerData.Amount,
        ExAmount = engineerData.ExAmount,
        TotalAmount = engineerData.TotalAmount,
        Images = engineerData.Engineere.Person.Images.Select(img => img.Image).ToList(),
        Words = engineerData.Engineere.Person.Words.Select(doc => doc.Content).ToList()
    };


// استعلام عن أفراد العائلة الذين لديهم بيانات سنوية لنفس العام
var familyData = await _dataContext.Relations
    .Where(r => r.Engineere.EngNumber == engineerNumber)
    .Include(r => r.Person.Images)     // تضمين الصور قبل Select
    .Include(r => r.Person.Words)      // تضمين الملفات قبل Select
    .Select(r => r.Person)             // ثم استخدام Select لتحويله إلى شخص
    .Where(p => p.AnnualDataDetails.Any(ad => ad.Year == year))
    .Select(p => new FullPersonDataDTO
    {
        FirstName = p.FirstName,
        LastName = p.LastName,
        FatherName = p.FatherName,
        MotherName = p.MotherName,
        BirthDate = p.BirthDateFormatted,
        NationalId = p.NationalId,
        EnsuranceNumber = p.EnsuranceNumber,
        Address = p.Address,
        Phone = p.Phone,
        Mobile = p.Mobile,
        Email = p.Email,
        Amount = p.AnnualDataDetails.First(ad => ad.Year == year).Amount,
        ExAmount = p.AnnualDataDetails.First(ad => ad.Year == year).ExAmount,
        
        // تحويل الصور والملفات إلى قائمة من byte[]
        Images = p.Images.Select(img => img.Image).ToList(),
        Words = p.Words.Select(word => word.Content).ToList()
    })
    .ToListAsync();




    engineerWithFamilyData.FamilyData = familyData;

    // حساب مجموع قيم Amount
    engineerWithFamilyData.TotalAmount = engineerData.Amount + familyData.Sum(f => f.Amount);

    return engineerWithFamilyData;
}



    } }








        

    
   
