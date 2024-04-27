using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnualData : ControllerBase
    {
        private readonly IAnnualDataService _AnnualDataService;

        public AnnualData(IAnnualDataService AnnualDataService)
        {
            _AnnualDataService =AnnualDataService;
        }

        [HttpPost]
        public async Task<ActionResult< Response>> RegisterAnnualData([FromBody] RegisterAnnualDataDTO registerAnnualDataDTO)
        {
                var response =await _AnnualDataService.Add(registerAnnualDataDTO);
                  if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
                return Ok(response);
        }
    }
}