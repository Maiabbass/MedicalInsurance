using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IAgeSegmentsRepository
    {
        Task<IEnumerable<AgeSegments>> Get(int year);
        Task<bool> Add_Age_Segments(List<AgeSegments> ageSegments);
        void Delete_Age_Segments(int year);
    }
}