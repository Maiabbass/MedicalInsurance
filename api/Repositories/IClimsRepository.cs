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
        Task<bool> ExistsAsync(int engineerId, int year);
       public bool UpdateClaim(int id, ClaimEditDTO claimEditDTO);

        Task<List<ClaimDetailsDTO>> GetClaimsAsync();
        Task<List<ClaimDetailsDTO>> GetClaimsByEnsuranceNumberAsync(string ensuranceNumber);
        Task<int> Add(Claims claims);
        public void   Delete(int Id);
        Task<List<Claims>> GetClaimsBetweenDatesAsync(DateTime startDate, DateTime endDate);

        Task<bool> CheckClaimExistsAsync(int id, int year);
    }
}