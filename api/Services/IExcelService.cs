using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.IO;

namespace api.Services
{
    public interface IExcelService
    {
        Task<Stream> GenerateExcelReportAsync();
        Task<Stream> GenerateExcelReportAsync2();
        Task<Stream> GenerateExcelReportAsync3();
        Task<Stream> GenerateExcelReportSpecializationsAndDepartmentsAsync();
        Task<Stream> GenerateExcelReportForClaimsEng(string insuranceNumber);

        Task<Stream> GenerateExcelReportAllClaimsAsync();

        
        

    }
}