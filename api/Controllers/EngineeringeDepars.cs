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
        

    private readonly IEngineeringeDeparServices  _engineeringeDeparServices;
          
        
    public EngineeringeDepars(IEngineeringeDeparServices engineeringeDeparServices)
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


        }
        [HttpPut("{Id}")]
        public  ActionResult<bool> Update(int Id,[FromBody] EngineeringeDeparEditDTO engineeringeDeparEditDTO){
           bool result= _engineeringeDeparServices.Update(Id,engineeringeDeparEditDTO);
            if (result)
            {
return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        }


[HttpGet("{Id}")]
public async Task<ActionResult<EngineeringeDepar?>>Get( int Id){
   return await _engineeringeDeparServices.Get(Id);
}



[HttpGet]
public async Task<ActionResult<IEnumerable<EngineeringeDeparEditDTO>>> GetAll()
    {
         var data=await _engineeringeDeparServices.GetAll();
        
      return Ok(data);
    }

    
    
   [HttpDelete("{Id}")] 
      public ActionResult Delete(int Id){
      try{
                  _engineeringeDeparServices.Delete(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  }
  

        
    }

}
        
 