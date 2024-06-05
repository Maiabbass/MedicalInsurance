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
    public class EngineeringeDepars : ControllerBase
    
    {
        

    private readonly IEngineeringeDeparService  _engineeringeDeparServices;
          
        
    public EngineeringeDepars(IEngineeringeDeparService engineeringeDeparServices)
      {
      _engineeringeDeparServices= engineeringeDeparServices;
         }
        

         [HttpPost]
        public async Task <ActionResult<Response>> AddDepar([FromBody] EngineeringeDeparEditDTO   engineeringeDeparEditDTO)
        {
           
    
                var response=  await _engineeringeDeparServices.Add(engineeringeDeparEditDTO);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);

        }}
        }