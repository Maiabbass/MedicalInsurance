using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Quiries : ControllerBase
    {

    private readonly IQuiriesServices _quirieService;
          
        
    public Quiries(IQuiriesServices quirieService)
      {
      _quirieService= quirieService;
         }



        [HttpGet("engineer with HisFamily/{EngNumber}")]
public async Task<ActionResult<SimpleEngineer>> GetEngineerWithRelations(string EngNumber)
{
    var engineer = await _quirieService.GetEngineerWithRelationsAsync(EngNumber);

    if (engineer == null)
    {
        return NotFound("Engineer not found.");
    }

    return Ok(engineer);
}






         [HttpGet("GetEngineer")]
        public async Task<ActionResult<IEnumerable<SimpleEngineer>>> GetEngineersWithDetails(
           [FromQuery] int? workPlaceId,
           [FromQuery] int? specializationId ,
           [FromQuery] int? engineeringUnitsId ,
           [FromQuery] int? PayMethodId
            
            )
        {
            var engineers = await _quirieService.GetEngineers( workPlaceId, specializationId , engineeringUnitsId,PayMethodId);

            if (engineers == null || !engineers.Any())
            {
                return NotFound("No engineers found matching the criteria.");
            }

            return Ok(engineers);
        }




         [HttpGet("by-age-segment")]
        public async Task<IActionResult> GetPersonsByAgeSegment(int fromYear, int toYear)
        {
            try
            {
                var personsInAgeSegment = await _quirieService.GetPersonsByAgeSegment(fromYear, toYear);

                if (personsInAgeSegment == null || personsInAgeSegment.Count == 0)
                {
                    return NotFound("No persons found in the specified age segment.");
                }

                return Ok(personsInAgeSegment);
            }
            catch (Exception ex)
            {
                // التعامل مع الاستثناءات غير المتوقعة
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpGet("GetEngineerWithFamilyData")]
        public async Task<IActionResult> GetEngineerWithFamilyData([FromQuery] string engineerNumber, [FromQuery] int year)
        {
            if (string.IsNullOrEmpty(engineerNumber) || year <= 0)
            {
                return BadRequest("يرجى تقديم رقم هندسي وسنة صالحين.");
            }

            var engineerWithFamilyData = await _quirieService.GetEngineerWithFamilyAnnualFullData(engineerNumber, year);

            if (engineerWithFamilyData == null)
            {
                return NotFound("لم يتم العثور على بيانات سنوية لهذا المهندس أو لعائلته للسنة المحددة.");
            }

            return Ok(engineerWithFamilyData);
        }
        
    }
}