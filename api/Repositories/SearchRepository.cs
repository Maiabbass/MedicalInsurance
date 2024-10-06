using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class SearchRepository : ISearchRepository
    {
         private readonly DataContext _dataContext;

         public SearchRepository(DataContext dataContext)
         {
            _dataContext=dataContext;
         }




    public async Task<PersonWithEngineereDTO> GetByEnsuranceNumberAsync(string ensuranceNumber)
{
    var person = await _dataContext.Persons
        .Where(p => p.EnsuranceNumber == ensuranceNumber)
        .Include(p => p.Engineere)
        .Include(p => p.Words)
        .Include(p => p.Images)
        .FirstOrDefaultAsync();

    if (person == null)
    {
        throw new KeyNotFoundException("No person found with the specified insurance number.");
    }

    var result = new PersonWithEngineereDTO
    {
        // البيانات الشخصية
        PersonId = person.Id,
        FirstName = person.FirstName,
        FatherName = person.FatherName,
        LastName = person.LastName,
        MotherName = person.MotherName,
        BirthDate = person.BirthDate,
        Address = person.Address,
        Mobile = person.Mobile,
        Phone = person.Phone,
        Email = person.Email,
        NationalId = person.NationalId,
        EnsuranceNumber = person.EnsuranceNumber,
        StatusId = person.StatusId,
        GenderId = person.GenderId,

        // بيانات المهندس إذا كان الشخص مهندسًا
        EngNumber = person.Engineere != null ? person.Engineere.EngNumber : null,
        SubNumber = person.Engineere != null ? person.Engineere.SubNumber : null,
        SpecializationId = person.Engineere != null ? person.Engineere.SpecializationId : null,
        WorkPlaceId = person.Engineere != null ? person.Engineere.WorkPlaceId : null,

        // تضمين الملفات المرتبطة
        Words = person.Words?.Select(w => new WordDTO 
        { 
            Id = w.Id, 
            FileName = w.Content
        }).ToList() ?? new List<WordDTO>(),

        Images = person.Images?.Select(i => new ImageDTO 
        { 
            Id = i.Id, 
            FileName = i.Image 
        }).ToList() ?? new List<ImageDTO>()
    };

    return result;
}








         public async Task<IEnumerable<PersonWithEngineereDTO>> GetWithNameAsync(string userSearch)
{
    var persons = await _dataContext.Persons
        .Where(p => p.FirstName.Contains(userSearch) ||
                     p.FatherName.Contains(userSearch) ||
                     p.LastName.Contains(userSearch))
        .Include(p => p.Engineere) // تضمين بيانات المهندس
        .Include(p => p.Images) // تضمين بيانات الصور
        .Include(p => p.Words) // تضمين بيانات ملفات Word
        .ToListAsync();

    // تحويل النتائج إلى DTO
    var results = persons.Select(p => new PersonWithEngineereDTO
    {
        PersonId = p.Id,
        FirstName = p.FirstName,
        FatherName = p.FatherName,
        LastName = p.LastName,
        MotherName = p.MotherName,
        BirthDate = p.BirthDate,
        Address = p.Address,
        Mobile = p.Mobile,
        Phone = p.Phone,
        Email = p.Email,
        NationalId = p.NationalId,
        EnsuranceNumber = p.EnsuranceNumber,
        StatusId = p.StatusId,
        GenderId = p.GenderId,

        // بيانات المهندس إذا كانت موجودة
        EngNumber = p.Engineere != null ? p.Engineere.EngNumber : null,
        SubNumber = p.Engineere != null ? p.Engineere.SubNumber : null,
        SpecializationId = p.Engineere != null ? p.Engineere.SpecializationId : null,
        WorkPlaceId = p.Engineere != null ? p.Engineere.WorkPlaceId : null,

        // جلب بيانات الصور
        Images = p.Images.Select(img => new ImageDTO
        {
            Id = img.Id,
            FileName = img.Image
        }).ToList(),

        // جلب بيانات ملفات Word
        Words = p.Words.Select(wf => new WordDTO
        {
            Id = wf.Id,
            FileName = wf.Content
        }).ToList()
    }).ToList(); // تحويل النتائج إلى قائمة

    return results;
}




    


  public async Task<PersonWithEngineereDTO> GetByNationalIdAsync(string nationalId)
    {
        // Normalize input
        nationalId = nationalId.Trim();

        var person = await _dataContext.Persons
            .Include(p => p.Engineere)
            .FirstOrDefaultAsync(p => EF.Functions.Like(p.NationalId, nationalId));

        if (person == null)
        {
            return null;
        }

        return new PersonWithEngineereDTO
        {
            PersonId = person.Id,
            FirstName = person.FirstName,
            FatherName = person.FatherName,
            LastName = person.LastName,
            MotherName = person.MotherName,
            BirthDate = person.BirthDate,
            Address = person.Address,
            Mobile = person.Mobile,
            Phone = person.Phone,
            Email = person.Email,
            NationalId = person.NationalId,
            EnsuranceNumber = person.EnsuranceNumber,
            StatusId = person.StatusId,
            GenderId = person.GenderId,
            EngNumber = person.Engineere?.EngNumber,
            SubNumber = person.Engineere?.SubNumber,
            SpecializationId = person.Engineere?.SpecializationId,
            WorkPlaceId = person.Engineere?.WorkPlaceId,
        };
    }





  public async Task<PersonWithEngineereDTO> GetEngNumberAsync(string engNumber)
{
    var person = await _dataContext.Engineeres
        .Where(e => e.EngNumber == engNumber)
        .Include(e => e.Person) // تضمين بيانات الشخص
        .ThenInclude(p => p.Images) // تضمين الصور
        .Include(e => e.Person) // تضمين بيانات الشخص مرة أخرى
        .ThenInclude(p => p.Words) // تضمين ملفات Word
        .Select(e => new PersonWithEngineereDTO
        {
            // خصائص جدول Person
            PersonId = e.Person.Id,
            FirstName = e.Person.FirstName,
            FatherName = e.Person.FatherName,
            LastName = e.Person.LastName,
            MotherName = e.Person.MotherName,
            BirthDate = e.Person.BirthDate,
            Address = e.Person.Address,
            Mobile = e.Person.Mobile,
            Phone = e.Person.Phone,
            Email = e.Person.Email,
            NationalId = e.Person.NationalId,
            EnsuranceNumber = e.Person.EnsuranceNumber,
            StatusId = e.Person.StatusId,
            GenderId = e.Person.GenderId,

            // خصائص جدول Engineer
            EngNumber = e.EngNumber,
            SubNumber = e.SubNumber,
            SpecializationId = e.SpecializationId,
            WorkPlaceId = e.WorkPlaceId,

            // تضمين الصور المرتبطة
            Images = e.Person.Images.Select(img => new ImageDTO
            {
                Id = img.Id,
                FileName = img.Image
            }).ToList(),

            // تضمين ملفات Word المرتبطة
            Words = e.Person.Words.Select(wf => new WordDTO
            {
                Id = wf.Id,
                FileName = wf.Content
            }).ToList()
        })
        .FirstOrDefaultAsync();

    if (person == null)
    {
        throw new KeyNotFoundException("No person found with the specified engineering number.");
    }

    return person;
}






    public async Task<IEnumerable<EngineeringUnits>> GetEngUnits(string name)
        {
            return await _dataContext.EngineeringUnits
                .Where(e => e.Name.Contains(name))
                .ToListAsync();
        }

         public async Task<IEnumerable<WorkPlace>> GetWorkPlace(string name)
        {
            return await _dataContext.WorkPlaces
                .Where(e => e.Name.Contains(name))
                .ToListAsync();
        }


        public async Task<IEnumerable<Hospital>> GetHospital(string name)
        {
            return await _dataContext.Hospitals
                .Where(e => e.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Claims>> GetClaim(string ensuranceNumber)
        {
            return await _dataContext.Claims
                .Where(e => e.EnsuranceNumber.Contains(ensuranceNumber))
                .ToListAsync();
        }


        public async Task<IEnumerable<Claims>> GetClaimsByDateRange(DateTime startDate, DateTime endDate)
    {
        return await _dataContext.Claims
            .Where(c => c.LoginDate.HasValue && c.LoginDate.Value.Date >= startDate.Date &&
                        c.LoginDate.Value.Date <= endDate.Date)
            .ToListAsync();
    }


     public async Task<IEnumerable<Claims>> GetAllClaims()
    {
        return await _dataContext.Claims
            
            .ToListAsync();
    }







 public async Task<PersonWithEngineereDTO> GetEngNumberAndSupNumber(string engNumber, string supNumber)
{
    var person = await _dataContext.Engineeres
        .Where(e => e.EngNumber == engNumber && e.SubNumber == supNumber)
        .Include(e => e.Person) // تضمين بيانات الشخص
        .ThenInclude(p => p.Images) // تضمين الصور المرتبطة
        .Include(e => e.Person) // تضمين بيانات الشخص مرة أخرى
        .ThenInclude(p => p.Words) // تضمين ملفات Word المرتبطة
        .Select(e => new PersonWithEngineereDTO
        {
            // خصائص جدول Person
            PersonId = e.Person.Id,
            FirstName = e.Person.FirstName,
            FatherName = e.Person.FatherName,
            LastName = e.Person.LastName,
            MotherName = e.Person.MotherName,
            BirthDate = e.Person.BirthDate,
            Address = e.Person.Address,
            Mobile = e.Person.Mobile,
            Phone = e.Person.Phone,
            Email = e.Person.Email,
            NationalId = e.Person.NationalId,
            EnsuranceNumber = e.Person.EnsuranceNumber,
            StatusId = e.Person.StatusId,
            GenderId = e.Person.GenderId,

            // خصائص جدول Engineer
            EngNumber = e.EngNumber,
            SubNumber = e.SubNumber,
            SpecializationId = e.SpecializationId,
            WorkPlaceId = e.WorkPlaceId,

            // تضمين الصور المرتبطة
            Images = e.Person.Images.Select(img => new ImageDTO
            {
                Id = img.Id,
                FileName = img.Image
            }).ToList(),

            // تضمين ملفات Word المرتبطة
            Words = e.Person.Words.Select(wf => new WordDTO
            {
                Id = wf.Id,
                FileName = wf.Content
            }).ToList()
        })
        .FirstOrDefaultAsync();

    if (person == null)
    {
        throw new KeyNotFoundException("No person found with the specified engineering number and sub number.");
    }

    return person;
}






 public async Task<PersonWithEngineereDTO> GetSubNumberAsync(string subNumber)
{
    var person = await _dataContext.Engineeres
        .Where(e => e.SubNumber == subNumber)
        .Include(e => e.Person) // تضمين بيانات الشخص
        .ThenInclude(p => p.Images) // تضمين الصور المرتبطة
        .Include(e => e.Person) // تضمين بيانات الشخص مرة أخرى
        .ThenInclude(p => p.Words) // تضمين ملفات Word المرتبطة
        .Select(e => new PersonWithEngineereDTO
        {
            // خصائص جدول Person
            PersonId = e.Person.Id,
            FirstName = e.Person.FirstName,
            FatherName = e.Person.FatherName,
            LastName = e.Person.LastName,
            MotherName = e.Person.MotherName,
            BirthDate = e.Person.BirthDate,
            Address = e.Person.Address,
            Mobile = e.Person.Mobile,
            Phone = e.Person.Phone,
            Email = e.Person.Email,
            NationalId = e.Person.NationalId,
            EnsuranceNumber = e.Person.EnsuranceNumber,
            StatusId = e.Person.StatusId,
            GenderId = e.Person.GenderId,

            // خصائص جدول Engineer
            EngNumber = e.EngNumber,
            SubNumber = e.SubNumber,
            SpecializationId = e.SpecializationId,
            WorkPlaceId = e.WorkPlaceId,

            // تضمين الصور المرتبطة
            Images = e.Person.Images.Select(img => new ImageDTO
            {
                Id = img.Id,
                FileName = img.Image
            }).ToList(),

            // تضمين ملفات Word المرتبطة
            Words = e.Person.Words.Select(wf => new WordDTO
            {
                Id = wf.Id,
                FileName = wf.Content
            }).ToList()
        })
        .FirstOrDefaultAsync();

    if (person == null)
    {
        throw new KeyNotFoundException("No person found with the specified Sub number.");
    }

    return person;
}


     


 public async Task<Person?> GetPersonWithEngineerByNationalIdAsync(string nationalId)
    {
        return await _dataContext.Persons
            .Include(p => p.Engineere)
            .Include(p => p.Images)    // Assuming Images is a collection of image objects
            .Include(p => p.Words)     // Assuming Words is a collection of word files
            .FirstOrDefaultAsync(p => p.NationalId == nationalId);
    }



        
    }}
    
