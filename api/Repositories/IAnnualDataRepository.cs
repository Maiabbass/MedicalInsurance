using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IAnnualDataRepository
    {
        Task<AnnualData?> get(int Id);

        Task<int>Add_AnnualData (AnnualData annualData);

        Task<int> Add_AnnualDataDetail(AnnualDataDetail annualDataDetail);
    }
}