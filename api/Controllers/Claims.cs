using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Claims : ControllerBase

    {

    private readonly IClimsRepository _climsRepository;
          
        
    public Claims(IClimsRepository climsRepository)
      {
      _climsRepository= climsRepository;
         }

      [HttpPost("upload")]
        public async Task<IActionResult> UploadClaims(IFormFile file ,[FromForm] int EngineereeId, [FromForm] int SurgicalProceduresId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid Excel file.");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;
                var claimsList = _climsRepository.ReadDataFromExcel(stream,EngineereeId ,SurgicalProceduresId);
                await _climsRepository.LoadClaimsToDatabase(claimsList);
            }

            return Ok("Operation accomplished successfully");
        }
    }
}