using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface IRecoveredRepository
    {

         Task<int> Add(Recovered recoveredDto);
          Task<Recovered?> Get(int Id);
         Task<RecoveredSummary> GetAll();
          public void   Delete(int Id);
          public bool Update(int Id, RecoveredDto recoveredDto);
            Task<List<RecoveredDto>> GetByEnsuranceNumber(string ensuranceNumber);
             Task<List<Recovered>> GetRecoveredBetweenDatesAsync(DateTime startDate, DateTime endDate);
        
    }
}