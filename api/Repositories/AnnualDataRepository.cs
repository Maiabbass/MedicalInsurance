using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class AnnualDataRepository : IAnnualDataRepository
    {

        private readonly DataContext _dataContext;

        public AnnualDataRepository(DataContext dataContext)
        {
            _dataContext=dataContext;
        }
        public async Task<int> Add_AnnualData(AnnualData annualData)
        {
             AnnualData newitem=new AnnualData ()
             {
                EngineereId = annualData.EngineereId,
                Year = annualData.Year,
                Amount = annualData.Amount,
                ExAmount = annualData.ExAmount,
                TotalAmount = annualData.TotalAmount,
                PayMethodId = annualData.PayMethodId,
                WorkPlaceId = annualData.WorkPlaceId,
                EngineeringUnitsId = annualData.EngineeringUnitsId
             };
             _dataContext.AnnualDatas.Add(newitem);
            await _dataContext.SaveChangesAsync();
            return newitem.Id;
        }

        public async Task<int> Add_AnnualDataDetail(AnnualDataDetail annualDataDetail)
        {
            AnnualDataDetail newitem =new AnnualDataDetail ()
            {
                    PersonId  = annualDataDetail.PersonId,
                    AnnualDataId = annualDataDetail.AnnualDataId,
                    IsEngineer = annualDataDetail.IsEngineer,
                    Amount = annualDataDetail.Amount
            } ;

             _dataContext.AnnualDataDetails.Add(newitem);
              await _dataContext.SaveChangesAsync();

              return newitem.Id;
        }

        public async Task<AnnualData?> get(int Id)
        {
           return await _dataContext.AnnualDatas.FirstOrDefaultAsync(x=>x.Id==Id);
        }
    }
}