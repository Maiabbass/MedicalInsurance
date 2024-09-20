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
        Task Delete_AgeSegmentsByYear(int year);

        Task Update_Age_Segment(AgeSegments ageSegment);
    }
}