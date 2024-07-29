using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using api.Services;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EXCEL : ControllerBase
    {
        private readonly IExcelService _excelService;

        public EXCEL(IExcelService excelService)
        {
            _excelService = excelService;
        }

        [HttpGet("EngUnitsExcel")]
        public async Task<IActionResult> GenerateExcel()
        {
            var stream = await _excelService.GenerateExcelReportAsync();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EngineeringUnits.xlsx");
        }

        [HttpGet("WorkPlaces")]
        public async Task<IActionResult> GenerateExcel2()
        {
            var stream = await _excelService.GenerateExcelReportAsync2();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "WorkPlaces.xlsx");
        }

        [HttpGet("Hospitals")]
        public async Task<IActionResult> GenerateExcel3()
        {
            var stream = await _excelService.GenerateExcelReportAsync3();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Hospitals.xlsx");
        }

        [HttpGet("EngDepart&&Splaztion")]
        public async Task<IActionResult> GenerateExcel4()
        {
            var stream = await _excelService.GenerateExcelReportSpecializationsAndDepartmentsAsync();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EngDepart&&Splaztion.xlsx");
        }




        [HttpGet("EngineerClaims/{insuranceNumber}")]
        public async Task<IActionResult> GenerateExcelForEngineerAsync(string insuranceNumber)
        {
            var stream = await _excelService.GenerateExcelReportForClaimsEng(insuranceNumber);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EngineerClaims.xlsx");
        }
/*
        [HttpGet("ClaimsByDateRange")]
        public async Task<IActionResult> GenerateExcelForDateRangeAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var stream = await _excelService.GenerateExcelReportForDateRangeAsync(startDate, endDate);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ClaimsByDateRange.xlsx");
        }
        */

        [HttpGet("AllClaims")]
        public async Task<IActionResult> GenerateExcelAllClaimsAsync()
        {
            var stream = await _excelService.GenerateExcelReportAllClaimsAsync();
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AllClaims.xlsx");
        }

    }

}
