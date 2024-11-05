using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface IQuiriesRepositories
    {

    Task<SimpleEngineer> GetEngineerWithRelationsAsync(string EngNumber);
    Task<IEnumerable<SimpleEngineer>> GetEngineers(int? workPlaceId ,int? specializationId , int? engineeringUnitsId , int? PayMethodId  );

    Task<List<Person>> GetPersonsByAgeSegment(int fromYear, int toYear);

    Task<EngineerFamilyFullDataDTO> GetEngineerWithFamilyAnnualFullData(string engineerNumber, int year);
        
    }
}