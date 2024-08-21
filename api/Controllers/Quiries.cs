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
        
    }
}