using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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

         public async Task<Person?> GetWithEN(string EnsuranceNumber)
        {
            return await _dataContext.Persons.Where(x=>x.EnsuranceNumber==EnsuranceNumber).FirstOrDefaultAsync();
        }
         public async Task<IEnumerable<Person>>? GetWithName(string userSearch)
        {

            return await _dataContext.Persons.Where(
                x=>x.FirstName.Contains(userSearch)
                ||x.FatherName.Contains(userSearch)
                ||x.LastName.Contains(userSearch)
                ).ToListAsync();
        }

         public async Task<Person?> GetWithNationalId(string NationalId)
        {
            return await _dataContext.Persons.Where(x=>x.NationalId==NationalId).FirstOrDefaultAsync();
        }

       
    }
    }
