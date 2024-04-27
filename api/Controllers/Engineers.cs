using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.DTOS;
using api.Entities;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Engineers : ControllerBase
    {



   private readonly IEngineerService _engineerService;
          
        
 public Engineers(IEngineerService engineerService)
{
  _engineerService= engineerService;
}
        

         [HttpPost]
        public async Task <ActionResult<Response>> AddEngineer([FromBody] EngineerPersonEditDTO   engineerPersonEditDTO)
        {
           
    
                var response=  await _engineerService.Add(engineerPersonEditDTO);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);


        }
        
    }
}