using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Services
{
    public interface IBlockService
    {

        Task<bool> BlockPerson(string ensuranceNumber, int year , string not);
         Task<bool> UnblockPerson(string ensuranceNumber);
         Task<List<Person>> GetAllBlockedPersons();

          Task<bool> IsPersonBlocked(string ensuranceNumber);
        
    }
}