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
    public class Hospitals : ControllerBase
    {
        

    private readonly IHospitalService _hospitalService;
          
        
    public Hospitals(IHospitalService hospitalService)
      {
      _hospitalService= hospitalService;
         }
        

         [HttpPost]
        public async Task <ActionResult<Response>> AddHospital([FromBody] HospitalEditDTO   hospitalEditDTO)
        {
           
    
                var response=  await _hospitalService.Add(hospitalEditDTO);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);


        }


        
    }

}