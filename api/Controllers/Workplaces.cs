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
    public class Workplaces : ControllerBase
    {
        
    private readonly IWorkplaceService _workplaceService;
          
        
    public Workplaces(IWorkplaceService workplaceService)
      {
      _workplaceService= workplaceService;
         }
        

         [HttpPost]
        public async Task <ActionResult<Response>> AddWorkplace([FromBody] WorkplaceEditDTO   workplaceEditDTO)
        {
           
    
                var response=  await _workplaceService.Add(workplaceEditDTO);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);


        }

    }
}