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
    }
}