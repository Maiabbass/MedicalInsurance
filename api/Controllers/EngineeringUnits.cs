using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services;
using api.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EngineeringUnits  : ControllerBase
    {
        
   private readonly IEngineeringUnitsService _engineeringUnitsService;
          
        
 public EngineeringUnits(IEngineeringUnitsService engineeringUnitsService)
{
  _engineeringUnitsService= engineeringUnitsService;
}
        

         [HttpPost]
        public async Task <ActionResult<Response>> AddEngineerUnits([FromBody] EngineeringUnitsEditDTO   engineeringUnitsEditDTO)
        {
           
    
                var response=  await _engineeringUnitsService.Add(engineeringUnitsEditDTO);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);


        }
        
    }
}