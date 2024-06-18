using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Services
{
    public interface ISearchService
    {
    Task<Person?>GetWithEN(string EnsuranceNumber) ;
    Task<IEnumerable<Person>?>GetWithName(string Name);

    Task<Person?> GetWithNationalId(string NationalId);


    }
}