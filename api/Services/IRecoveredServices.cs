using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface IRecoveredServices
    {
        Task<Response> Add(RecoveredDto recoveredDto);
        public bool Update(int Id, RecoveredDto reco);
        Task<Recovered?>Get(int Id);

       Task<RecoveredSummary> GetAll();
        public bool Delete(int Id);

        Task<RecoveredSummary> GetByEnsuranceNumber(string ensuranceNumber);
        
        
    }
}