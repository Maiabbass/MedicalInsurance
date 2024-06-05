using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IClimsRepository
    {
         List<Claims> ReadDataFromExcel(Stream fileStream,int engineereeId, int surgicalProceduresId);
        Task LoadClaimsToDatabase(List<Claims> claimsList);
       
    }
}