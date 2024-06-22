using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface ISearchRepository
    {
         Task<Person>? GetWithEN(string EnsuranceNumber);
        
         Task<IEnumerable<Person>>? GetWithName(string FirstName) ;

          Task<Person?> GetWithNationalId(string NationalId);
       
    }
}