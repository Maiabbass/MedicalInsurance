using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
            try{
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
                   // var fullName = worksheet.Cells[row, 3]?.Text?.Trim();
                    var totalPriceText = worksheet.Cells[row, 4]?.Text?.Trim();
                    var approvedPriceText = worksheet.Cells[row, 5]?.Text?.Trim();
                    var enduranceRatioText = worksheet.Cells[row, 6]?.Text?.Trim();
                    var nonAddText = worksheet.Cells[row, 7]?.Text?.Trim();
                    var companyFeesText = worksheet.Cells[row, 8]?.Text?.Trim();
                    var nonAddForPersonText = worksheet.Cells[row, 9]?.Text?.Trim();

                    if (string.IsNullOrEmpty(hospitalName) ||
                        string.IsNullOrEmpty(ensuranceNumber) ||
                        
                        string.IsNullOrEmpty(totalPriceText) ||
                        string.IsNullOrEmpty(approvedPriceText) ||
                        string.IsNullOrEmpty(enduranceRatioText) ||
                        string.IsNullOrEmpty(nonAddText) ||
                        string.IsNullOrEmpty(companyFeesText) ||
                        string.IsNullOrEmpty(nonAddForPersonText))
                    {
                        continue; // Skip rows with missing data
                    }

                    if (!decimal.TryParse(totalPriceText, out var totalPrice) ||
                        !decimal.TryParse(approvedPriceText, out var approvedPrice) ||
                        !decimal.TryParse(enduranceRatioText, out var enduranceRatio) ||
                        !decimal.TryParse(nonAddText, out var nonAdd) ||
                        !decimal.TryParse(companyFeesText, out var companyFees) ||
                        !decimal.TryParse(nonAddForPersonText, out var nonAddForPerson))
                    {
                        continue; // Skip rows with invalid decimal data
                    }

                    var newClaim = new Claims()
                    {
                        HospitalId = GetHospitalIdByName(hospitalName),
                        PersonId =  GetpersonIdByinsuranceNumber(ensuranceNumber),
                        EnsuranceNumber = ensuranceNumber,
                        FullName="",
                        TotalPrice = totalPrice,
                        ApprovedPrice = approvedPrice,
                        EnduranceRatio = enduranceRatio,
                        non_Add = nonAdd,
                        Company_fees = companyFees,
                        non_AddForPerson = nonAddForPerson,
                        Trust=false,
                        LoginDate=null,
                        ExitDate=null,
                        SurgicalProceduresId=null
                    };

        /*
                    var newC = new  Claims(0,"",0,null,null,0,0,0,0,0,0,true)
             {
      
             PersonId = engineereeId,
                 SurgicalProceduresId = surgicalProceduresId
                };
            */

                    claims.Add(newClaim);
                    // test....
                  //  claims.Add(newC);
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

       

        
    }
}
