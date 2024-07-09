using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Services;
using api.DTOS;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PDFController : ControllerBase
    {
        private readonly IPdfService _pdfService;
        private readonly IEngineeringUnitsService _engineeringUnitService;

        private readonly IWorkplaceService _workplaceService;

        private readonly IQuiriesServices _quiriesServices;

        public PDFController(IPdfService pdfService, IEngineeringUnitsService engineeringUnitService, IWorkplaceService workplaceService ,IQuiriesServices quiriesServices)
        {
            _pdfService = pdfService;
            _engineeringUnitService = engineeringUnitService;
            _workplaceService = workplaceService;
            _quiriesServices=quiriesServices;
        }

        [HttpGet("reportEngUints")]
        public async Task<IActionResult> GetPdfReport1()
        {
    
            var units = await _engineeringUnitService.GetAll();

            byte[] pdfBytes = _pdfService.GeneratePdfEngUnits(units);
  
            return File(pdfBytes, "application/pdf", "report.pdf");
        }


        [HttpGet("reportWorkPlace")]
public async Task<IActionResult> GetPdfReport2()
{
    
    var workPlaces = await _workplaceService.GetAll();
    
    
    var engineeringUnits = await _engineeringUnitService.GetAll();
    
    
    byte[] pdfBytes = _pdfService.GeneratePdfWorkPlace(workPlaces, engineeringUnits);
    
    
    return File(pdfBytes, "application/pdf", "report.pdf");
}


[HttpGet("reportEngWithFamily/{engNumber}")]
public async Task<IActionResult> GetPdfReport3(string engNumber)
{
    var engineer = await _quiriesServices.GetEngineerWithRelationsAsync(engNumber);

    if (engineer == null)
    {
        return NotFound("Engineer not found.");
    }

    byte[] pdfBytes = _pdfService.GeneratePdfEngineerReport(new List<SimpleEngineer> { engineer });

    return File(pdfBytes, "application/pdf", "report.pdf");
}
    }
}
