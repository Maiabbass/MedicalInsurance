using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface IClimsRepository
    {
         List<Claims> ReadDataFromExcel(Stream fileStream);
        Task LoadClaimsToDatabase(List<Claims> claimsList);
        Task<bool> ExistsAsync(int engineerId);
        Task<bool> UpdateSurgicalProcedureAsync(int claimId, string surgicalProcedureName, DateTime? newClaimDate = null);

        Task<List<ClaimDetailsDTO>> GetClaimsAsync();
        Task<List<ClaimDetailsDTO>> GetClaimsByEnsuranceNumberAsync(string ensuranceNumber);
    }
}