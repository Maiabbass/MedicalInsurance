using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace api.Repositories
{
    public class ClimsRepository : IClimsRepository
    {
        private readonly DataContext _dataContext;



        public ClimsRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

     public List<Claims> ReadDataFromExcel(Stream fileStream)
{
    try
    {
        var claims = new List<Claims>();

        using (var package = new ExcelPackage(fileStream))
        {
            var worksheet = package.Workbook.Worksheets.FirstOrDefault(); // Get the first worksheet
            if (worksheet == null)
            {
                throw new Exception("The Excel file does not contain any worksheets.");
            }

            if (worksheet.Dimension == null)
            {
                throw new Exception("The worksheet is empty.");
            }

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var hospitalName = worksheet.Cells[row, 1]?.Text?.Trim();
                var ensuranceNumber = worksheet.Cells[row, 2]?.Text?.Trim();
                var fullName = worksheet.Cells[row, 3]?.Text?.Trim();
                var totalPriceText = worksheet.Cells[row, 4]?.Text?.Trim();
                var approvedPriceText = worksheet.Cells[row, 5]?.Text?.Trim();
                var enduranceRatioText = worksheet.Cells[row, 6]?.Text?.Trim();
                var nonAddText = worksheet.Cells[row, 7]?.Text?.Trim();
                var companyFeesText = worksheet.Cells[row, 8]?.Text?.Trim();
                var nonAddForPersonText = worksheet.Cells[row, 9]?.Text?.Trim();

                // Check for missing mandatory data
                if (string.IsNullOrEmpty(hospitalName) ||
                    string.IsNullOrEmpty(ensuranceNumber) ||
                    string.IsNullOrEmpty(totalPriceText) ||
                    string.IsNullOrEmpty(approvedPriceText) ||
                    string.IsNullOrEmpty(enduranceRatioText) ||
                    string.IsNullOrEmpty(nonAddText) ||
                    string.IsNullOrEmpty(companyFeesText))
                {
                    continue; // Skip rows with missing mandatory data
                }

                // Parse decimal values
                if (!decimal.TryParse(totalPriceText, out var totalPrice) ||
                    !decimal.TryParse(approvedPriceText, out var approvedPrice) ||
                    !decimal.TryParse(enduranceRatioText, out var enduranceRatio) ||
                    !decimal.TryParse(nonAddText, out var nonAdd) ||
                    !decimal.TryParse(companyFeesText, out var companyFees))
                {
                    continue; // Skip rows with invalid decimal data
                }

                // Parse optional value for nonAddForPerson
                decimal? nonAddForPerson = null;
                if (decimal.TryParse(nonAddForPersonText, out var nonAddForPersonValue))
                {
                    nonAddForPerson = nonAddForPersonValue;
                }

                var newClaim = new Claims()
                {
                    HospitalId = GetHospitalIdByName(hospitalName),
                    PersonId = GetpersonIdByinsuranceNumber(ensuranceNumber),
                    EnsuranceNumber = ensuranceNumber,
                    FullName = fullName,
                    TotalPrice = totalPrice,
                    ApprovedPrice = approvedPrice,
                    EnduranceRatio = enduranceRatio,
                    non_Add = nonAdd,
                    Company_fees = companyFees,
                    non_AddForPerson = nonAddForPersonValue,
                    Trust = true,
                    LoginDate = null,
                    ExitDate = null,
                    SurgicalProceduresId = null,
                    DateSurgicalProcedures = null,

                };

                claims.Add(newClaim);
            }
        }

        return claims;
    }
    catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
    {
        throw new Exception("Duplicate entry detected for unique index or constraint.", sqlEx);
    }
}





        public int GetHospitalIdByName(string name)
        {
            var hospital = _dataContext.Hospitals.FirstOrDefault(h => h.Name == name);
            if (hospital != null)
            {
                return hospital.Id;
            }
            else
            {
                throw new ArgumentException("لا يوجد مشفى بهذا الاسم", nameof(name));
            }
        }

        public int GetpersonIdByinsuranceNumber(string num)
        {
            var item = _dataContext.Persons.FirstOrDefault(h => h.EnsuranceNumber == num);
            if (item != null)
            {
                return item.Id;
            }
            else
            {
                throw new ArgumentException("لا يوجد عنصر بهذا الرقم", nameof(num));
            }
        }

        public async Task LoadClaimsToDatabase(List<Claims> claimsList)
        {
            _dataContext.Claims.AddRange(claimsList);
            await _dataContext.SaveChangesAsync();
            
        }

public async Task<bool> ExistsAsync(int engineerId, int year)
{
    return await _dataContext.Claims.AnyAsync(c => c.PersonId == engineerId && c.Year == year);
}


public bool UpdateClaim(int id, ClaimEditDTO claimEditDTO)
{
    // البحث عن المطالبة في قاعدة البيانات
    var databaseEntity = _dataContext.Claims.FirstOrDefault(x => x.Id == id);
    if (databaseEntity == null)
    {
        return false; // في حال عدم وجود المطالبة، قم بإرجاع false
    }

    // تحديث الحقول الخاصة بالمطالبة
    databaseEntity.HospitalId = claimEditDTO.HospitalId;
    databaseEntity.PersonId = (int)claimEditDTO.PersonId;
    databaseEntity.EnsuranceNumber = claimEditDTO.EnsuranceNumber;
    databaseEntity.FullName = claimEditDTO.FullName;
    databaseEntity.TotalPrice = claimEditDTO.TotalPrice;
    databaseEntity.Company_fees = claimEditDTO.Company_fees;
    databaseEntity.ApprovedPrice = claimEditDTO.ApprovedPrice;
    databaseEntity.non_Add = claimEditDTO.NonAdd;
    databaseEntity.non_AddForPerson = claimEditDTO.NonAddForPerson;
    databaseEntity.EnduranceRatio = claimEditDTO.EnduranceRatio;
    databaseEntity.Trust = claimEditDTO.Trust;
    databaseEntity.LoginDate = claimEditDTO.LoginDate;
    databaseEntity.ExitDate = claimEditDTO.ExitDate;
    databaseEntity.SurgicalProceduresId = claimEditDTO.SurgicalProceduresId;
    databaseEntity.DateSurgicalProcedures = claimEditDTO.DateSurgicalProcedures;
    databaseEntity.Year = claimEditDTO.Year;
    databaseEntity.ClimeData = claimEditDTO.ClimeData;
    databaseEntity.Number=claimEditDTO.Number;


    // حفظ التغييرات في قاعدة البيانات
    return _dataContext.SaveChanges() > 0;
}



    public async Task<List<ClaimDetailsDTO>> GetClaimsAsync()
{
    var claims = await _dataContext.Claims
        .Include(c => c.Hospital)
        .Include(c => c.SurgicalProcedures)
        .Select(static c => new ClaimDetailsDTO
        {
            Id = c.Id,
            EnsuranceNumber = c.EnsuranceNumber,
            FullName = c.FullName,
            TotalPrice = c.TotalPrice,
            Company_fees = c.Company_fees,
            ApprovedPrice = c.ApprovedPrice,
            non_Add = c.non_Add,
            non_AddForPerson = c.non_AddForPerson,
            EnduranceRatio = c.EnduranceRatio,
            HospitalName = c.Hospital.Name,
            Trust = c.Trust,
            LoginDate = c.LoginDate,
            ExitDate = c.ExitDate,
            PersonId = c.PersonId,
            DateSurgicalProcedures=c.DateSurgicalProcedures,
            ClimeData=c.ClimeData,
            Number= c.Number,

            SurgicalProcedures = c.SurgicalProcedures != null ? new SurgicalProceduresEditDTO
            {
                Id = c.SurgicalProcedures.Id,
                Name = c.SurgicalProcedures.Name,
               
                
                Pathological_specialization = c.SurgicalProcedures.Pathological_specialization
            } : null ,// إذا كانت `SurgicalProcedures` null، يتم تعيين `SurgicalProcedures` في الـ DTO كـ null
            Year= (int)c.Year,
        })
        .ToListAsync();

    return claims;
}




  public async Task<List<ClaimDetailsDTO>> GetClaimsByEnsuranceNumberAsync(string ensuranceNumber)
{
    var claims = await _dataContext.Claims
        .Include(c => c.Hospital)
        .Include(c => c.SurgicalProcedures)
        .Where(c => c.EnsuranceNumber == ensuranceNumber)
        .Select(c => new ClaimDetailsDTO
        {
            Id = c.Id,
            EnsuranceNumber = c.EnsuranceNumber,
            FullName = c.FullName,
            TotalPrice = c.TotalPrice,
            Company_fees = c.Company_fees,
            ApprovedPrice = c.ApprovedPrice,
            non_Add = c.non_Add,
            non_AddForPerson = c.non_AddForPerson,
            EnduranceRatio = c.EnduranceRatio,
            HospitalName = c.Hospital.Name,
            Trust = c.Trust,
            LoginDate = c.LoginDate,
            ExitDate = c.ExitDate,
            PersonId = c.PersonId,
            DateSurgicalProcedures = c.DateSurgicalProcedures,
            ClimeData=c.ClimeData,
            Number= c.Number,
            SurgicalProcedures = c.SurgicalProcedures != null ? new SurgicalProceduresEditDTO
            {
                Id = c.SurgicalProcedures.Id,
                Name = c.SurgicalProcedures.Name,
                
               
                Pathological_specialization = c.SurgicalProcedures.Pathological_specialization
            } : null ,
            Year= (int)c.Year,
        })
        .ToListAsync();

    return claims;
}



      public async Task<int> Add(Claims claims)
{
    

    Claims newCl = new Claims()
    {
        EnsuranceNumber =claims.EnsuranceNumber,
        FullName = claims.FullName,
        TotalPrice = claims.TotalPrice,
        Company_fees = claims.Company_fees,
        ApprovedPrice = claims.ApprovedPrice,
        non_Add = claims.non_Add,
        non_AddForPerson = claims.non_AddForPerson,
        EnduranceRatio = claims.EnduranceRatio,
        HospitalId = claims.HospitalId,
        
        LoginDate = claims.LoginDate,
        ExitDate = claims.ExitDate,
        PersonId = claims.PersonId,
        SurgicalProceduresId = claims.SurgicalProceduresId,
        ClimeData= claims.ClimeData,
        Number = claims.Number, 
        Year=claims.Year,
    


    };

#pragma warning restore IDE0090 // Use 'new(...)'

    _dataContext.Claims.Add(newCl);
    await _dataContext.SaveChangesAsync();

    return newCl.Id;
}



         public void   Delete(int Id)
        {
            
            var rest = _dataContext.Claims.FirstOrDefault(x=>x.Id==Id);
            if(rest!=null)
            {
                _dataContext.Claims.Remove(rest);
                _dataContext.SaveChanges();
            }
        }



        public async Task<List<Claims>> GetClaimsBetweenDatesAsync(DateTime startDate, DateTime endDate)
{
    return await _dataContext.Claims
                         .Where(c => c.ClimeData >= startDate && c.ClimeData <= endDate)
                         .ToListAsync();
}


public async Task<bool> CheckClaimExistsAsync(int id, int year)
{
    var claim = await _dataContext.Claims
        .FirstOrDefaultAsync(c => c.Id == id && c.Year == year);
    
    return claim != null;
}

     
    }
       
    } 

