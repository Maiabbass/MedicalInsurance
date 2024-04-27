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

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Persons : ControllerBase
    {
        private readonly IPersonService  _personService;

        public Persons(IPersonService  personService)
        {
            _personService = personService;
        }



        [HttpPost]
        public async Task <ActionResult<Response>> AddPerson([FromBody] PersonEditDTO  personEditDTO)
        {
         
               
                var response = await _personService.Add(personEditDTO);
                 if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);

        }

       
          
        


    }
}