using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class RecoveredRepository : IRecoveredRepository
    {

        private readonly DataContext _dataContext;



        public RecoveredRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<int> Add(Recovered recovered)
{

         
            Recovered newRE =new Recovered()
             {
               EnsuranceNumber=recovered.EnsuranceNumber,
               FullName=recovered.FullName,
               TotalPrice=recovered.TotalPrice,
               Company_fees=recovered.Company_fees,
               ApprovedPrice=recovered.ApprovedPrice,
               non_Add=recovered.non_Add,
               non_AddForPerson=recovered.non_AddForPerson,
               EnduranceRatio=recovered.EnduranceRatio,
               HospitalId=recovered.HospitalId,
               Send=recovered.Send,
               LoginDate=recovered.LoginDate,
               ExitDate=recovered.ExitDate,
               PersonId=recovered.PersonId,
               SurgicalProceduresId=recovered.SurgicalProceduresId,

               
             };
#pragma warning restore IDE0090 // Use 'new(...)'


            _dataContext.Recovereds.Add(newRE);
              await _dataContext.SaveChangesAsync();

              return newRE.Id;
          }

          public async Task<Recovered?> Get(int Id)
        {
            return await _dataContext.Recovereds.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

       public async Task<RecoveredSummary> GetAll()
{
    var recovereds = await _dataContext.Recovereds.ToListAsync();
    
    var summary = new RecoveredSummary
    {
        Recovereds = recovereds,
        TotalPriceSum = recovereds.Sum(r => r.TotalPrice),
        CompanyFeesSum = recovereds.Sum(r => r.Company_fees),
        ApprovedPriceSum = recovereds.Sum(r => r.ApprovedPrice),
        NonAddSum = recovereds.Sum(r => r.non_Add),
        NonAddForPersonSum = recovereds.Sum(r => r.non_AddForPerson),
        Count = recovereds.Count
    };

    return summary;
}

         public void   Delete(int Id)
        {
            
            var rest = _dataContext.Recovereds.FirstOrDefault(x=>x.Id==Id);
            if(rest!=null)
            {
                _dataContext.Recovereds.Remove(rest);
                _dataContext.SaveChanges();
            }
        }

         public bool Update(int Id, RecoveredDto recoveredDto)
        {
       var databaseEntity= _dataContext.Recovereds.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.EnsuranceNumber=recoveredDto.EnsuranceNumber;
       databaseEntity.FullName=recoveredDto.FullName;
       databaseEntity.TotalPrice=recoveredDto.TotalPrice;
       databaseEntity.Company_fees=recoveredDto.Company_fees;
       databaseEntity.ApprovedPrice=recoveredDto.ApprovedPrice;
       databaseEntity.non_Add=recoveredDto.non_Add;
       databaseEntity.non_AddForPerson=recoveredDto.non_AddForPerson;
       databaseEntity.EnduranceRatio=recoveredDto.EnduranceRatio;
       databaseEntity.Send=recoveredDto.Send;
       databaseEntity.HospitalId=recoveredDto.HospitalId;
       databaseEntity.SurgicalProceduresId=recoveredDto.SurgicalProceduresId;
       databaseEntity.PersonId=recoveredDto.PersonId;
       databaseEntity.LoginDate=recoveredDto.LoginDate;
       databaseEntity.ExitDate=recoveredDto.ExitDate;


       return _dataContext.SaveChanges()>0;
      
        }



        public async Task<RecoveredSummary> GetByEnsuranceNumber(string ensuranceNumber)
{
    var recovereds = await _dataContext.Recovereds
                                         .Where(r => r.EnsuranceNumber == ensuranceNumber)
                                         .ToListAsync();

    var summary = new RecoveredSummary
    {
        Recovereds = recovereds,
        TotalPriceSum = recovereds.Sum(r => r.TotalPrice),
        CompanyFeesSum = recovereds.Sum(r => r.Company_fees),
        ApprovedPriceSum = recovereds.Sum(r => r.ApprovedPrice),
        NonAddSum = recovereds.Sum(r => r.non_Add),
        NonAddForPersonSum = recovereds.Sum(r => r.non_AddForPerson),
        Count = recovereds.Count
    };

    return summary;
}


         
    
}


        
    }
