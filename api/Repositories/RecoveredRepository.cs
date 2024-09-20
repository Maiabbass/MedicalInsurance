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
    

    Recovered newRE = new Recovered()
    {
        EnsuranceNumber = recovered.EnsuranceNumber,
        FullName = recovered.FullName,
        TotalPrice = recovered.TotalPrice,
        Company_fees = recovered.Company_fees,
        ApprovedPrice = recovered.ApprovedPrice,
        non_Add = recovered.non_Add,
        non_AddForPerson = recovered.non_AddForPerson,
        EnduranceRatio = recovered.EnduranceRatio,
        HospitalId = recovered.HospitalId,
        Status = recovered.Status,
        LoginDate = recovered.LoginDate,
        ExitDate = recovered.ExitDate,
        PersonId = recovered.PersonId,
        SurgicalProceduresId = recovered.SurgicalProceduresId,
        RecoDate = recovered.RecoDate,
        Number = recovered.Number, 
        DateSurgicalProcedures=recovered.DateSurgicalProcedures,
        NameHospital_Out=recovered.NameHospital_Out,
        NumberBOK=recovered.NumberBOK,
        Phone=recovered.Phone,


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
       databaseEntity.Status=recoveredDto.Status;
       databaseEntity.HospitalId=recoveredDto.HospitalId;
       databaseEntity.SurgicalProceduresId=recoveredDto.SurgicalProceduresId;
       databaseEntity.PersonId=recoveredDto.PersonId;
       databaseEntity.LoginDate=recoveredDto.LoginDate;
       databaseEntity.ExitDate=recoveredDto.ExitDate;
       databaseEntity.RecoDate=recoveredDto.RecoDate;
       databaseEntity.Number=recoveredDto.Number;
       databaseEntity.DateSurgicalProcedures=recoveredDto.DateSurgicalProcedures;
       databaseEntity.NameHospital_Out=recoveredDto.NameHospital_Out;
       databaseEntity.NumberBOK=recoveredDto.NumberBOK;
       databaseEntity.Phone=recoveredDto.Phone;


       return _dataContext.SaveChanges()>0;
      
        }



        public async Task<List<RecoveredDto>> GetByEnsuranceNumber(string ensuranceNumber)
{
    var recovereds = await _dataContext.Recovereds
                                         .Include(r => r.Hospital) // ربط مع Hospital
                                         .Include(r => r.SurgicalProcedures) // ربط مع SurgicalProcedures
                                         .Where(r => r.EnsuranceNumber == ensuranceNumber)
                                         .ToListAsync();

    var recoveredDtos = recovereds.Select(r => new RecoveredDto
    {
        //Id = r.Id,
        EnsuranceNumber = r.EnsuranceNumber,
        FullName = r.FullName,
        TotalPrice = r.TotalPrice,
        Company_fees = r.Company_fees,
        ApprovedPrice = r.ApprovedPrice,
        non_Add = r.non_Add,
        non_AddForPerson = r.non_AddForPerson,
        EnduranceRatio = r.EnduranceRatio,
        DateSurgicalProcedures=r.DateSurgicalProcedures,
        NameHospital_Out=r.NameHospital_Out,
        NumberBOK=r.NumberBOK,
        Phone=r.Phone,
        HospitalName = r.Hospital != null ? r.Hospital.Name : null, // اسم المستشفى
        SurgicalProcedures = r.SurgicalProcedures != null ? new SurgicalProceduresEditDTO
        {
            Id = r.SurgicalProcedures.Id,
            Name = r.SurgicalProcedures.Name,
          
            Pathological_specialization = r.SurgicalProcedures.Pathological_specialization
        } : null // بيانات SurgicalProcedures
    }).ToList();

    return recoveredDtos;
}


public async Task<List<Recovered>> GetRecoveredBetweenDatesAsync(DateTime startDate, DateTime endDate)
{
    return await _dataContext.Recovereds
                         .Where(r => r.RecoDate >= startDate && r.RecoDate <= endDate)
                         .ToListAsync();
}


         
    
}


        
    }
