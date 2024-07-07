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
        .Include(e => e.Relations)
            .ThenInclude(r => r.Person)
        .FirstOrDefaultAsync(e => e.EngNumber == EngNumber);

    if (engineer == null)
        return null;

    // تحويل الكائن إلى كائن بسيط
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
            Subscrib = engineer.Person.Subscrib,
            Affiliate = engineer.Person.Affiliate,
            Beneficiary = engineer.Person.Beneficiary,
            GenderId = engineer.Person.GenderId
            
        },
        Relations = engineer.Relations.Select(r => new SimpleRelation
        {
            Id = r.Id,
            Name = r.Name,
            RelationTypeId = r.RelationTypeId, // توضيح نوع العلاقة
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
                Subscrib = r.Person.Subscrib,
                Affiliate = r.Person.Affiliate,
                Beneficiary = r.Person.Beneficiary,
                GenderId = r.Person.GenderId
            }
        }).ToList()
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
                    Subscrib = e.Person.Subscrib,
                    Affiliate = e.Person.Affiliate,
                    Beneficiary = e.Person.Beneficiary,
                    GenderId = e.Person.GenderId
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
                    Subscrib = e.Person.Subscrib,
                    Affiliate = e.Person.Affiliate,
                    Beneficiary = e.Person.Beneficiary,
                    GenderId = e.Person.GenderId
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
            Subscrib = e.Person.Subscrib,
            Affiliate = e.Person.Affiliate,
            Beneficiary = e.Person.Beneficiary,
            GenderId = e.Person.GenderId
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
            Subscrib = e.Person.Subscrib,
            Affiliate = e.Person.Affiliate,
            Beneficiary = e.Person.Beneficiary,
            GenderId = e.Person.GenderId
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
            Subscrib = e.Person.Subscrib,
            Affiliate = e.Person.Affiliate,
            Beneficiary = e.Person.Beneficiary,
            GenderId = e.Person.GenderId
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
                    Subscrib = e.Person.Subscrib,
                    Affiliate = e.Person.Affiliate,
                    Beneficiary = e.Person.Beneficiary,
                    GenderId = e.Person.GenderId
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
                    Subscrib = e.Person.Subscrib,
                    Affiliate = e.Person.Affiliate,
                    Beneficiary = e.Person.Beneficiary,
                    GenderId = e.Person.GenderId
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
                    Subscrib = e.Person.Subscrib,
                    Affiliate = e.Person.Affiliate,
                    Beneficiary = e.Person.Beneficiary,
                    GenderId = e.Person.GenderId
                },
               
                                         
            }).ToList();
            return simpleEngineers;
           }


           else{
            return null ;

           }
    }


    } }








        

    
   
