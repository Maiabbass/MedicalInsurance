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
#pragma warning disable CS8603 // Possible null reference return.
    var result = await _dataContext.Persons
        .Where(p => p.EnsuranceNumber == ensuranceNumber)
        .Select(p => new PersonWithEngineereDTO
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

            EngNumber = p.Engineere != null ? p.Engineere.EngNumber : null,
            SubNumber = p.Engineere != null ? p.Engineere.SubNumber : null,
            SpecializationId = p.Engineere != null ? p.Engineere.SpecializationId : null,
            WorkPlaceId = p.Engineere != null ? p.Engineere.WorkPlaceId : null,
        })
        .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.

    if (result == null)
    {
        throw new KeyNotFoundException("No person found with the specified insurance number.");
    }

    return result;
}



         public async Task<IEnumerable<PersonWithEngineereDTO>> GetWithNameAsync(string userSearch)
{
    var results = await _dataContext.Persons
        .Where(p => p.FirstName.Contains(userSearch) ||
                    p.FatherName.Contains(userSearch) ||
                    p.LastName.Contains(userSearch))
        .Select(p => new PersonWithEngineereDTO
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
            EngNumber = p.Engineere != null ? p.Engineere.EngNumber : null,
            SubNumber = p.Engineere != null ? p.Engineere.SubNumber : null,
            SpecializationId = p.Engineere != null ? p.Engineere.SpecializationId : null,
            WorkPlaceId = p.Engineere != null ? p.Engineere.WorkPlaceId : null,
        })
        .ToListAsync();

    return results;
}


    

         public async Task<PersonWithEngineereDTO> GetByNationalIdAsync(string nationalId)
{
#pragma warning disable CS8603 // Possible null reference return.
    var result = await _dataContext.Persons
        .Where(p => p.NationalId == nationalId)
        .Select(p => new PersonWithEngineereDTO
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

            EngNumber = p.Engineere != null ? p.Engineere.EngNumber : null,
            SubNumber = p.Engineere != null ? p.Engineere.SubNumber : null,
            SpecializationId = p.Engineere != null ? p.Engineere.SpecializationId : null,
            WorkPlaceId = p.Engineere != null ? p.Engineere.WorkPlaceId : null,
        })
        .FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.

    if (result == null)
    {
        throw new KeyNotFoundException("No person found with the specified insurance number.");
    }

    return result;
}


     public async Task<PersonWithEngineereDTO> GetEngNumberAsync(string engNumber)
{
    var person = await _dataContext.Engineeres
        .Where(e => e.EngNumber == engNumber)
        .Include(e => e.Person)
        .Select(e => new PersonWithEngineereDTO
        {
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

            EngNumber = e.EngNumber,
            SubNumber = e.SubNumber,
            SpecializationId = e.SpecializationId,
            WorkPlaceId = e.WorkPlaceId,
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

    

     




        
    }}
    
