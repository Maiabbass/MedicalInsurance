using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Cities : ControllerBase
    
    {
        

    private readonly ICityService _cityService;
          
        
    public Cities(ICityService cityService)
      {
      _cityService= cityService;
         }
        

         [HttpPost]
        public async Task <ActionResult<Response>> AddCity([FromBody] CityEditDTO   cityEditDTO)
        {
           
    
                var response=  await _cityService.Add(cityEditDTO);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);


        }


        
    }

}
        
 