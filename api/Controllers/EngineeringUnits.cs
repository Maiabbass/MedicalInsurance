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

        [HttpGet]
        public async Task<ActionResult<EngineeringUnits?>>GetAll(){
   
          var  data= await _engineeringUnitsService.GetAll();
     return Ok(data);
}

[HttpGet("{id}")]
        public async Task<ActionResult<EngineeringUnits>>Get(int Id){
   
          var  data= await _engineeringUnitsService.Get(Id);
     return Ok(data);
}

      [HttpDelete("{Id}")] 
      public ActionResult Delete(int Id){
      try{
                  _engineeringUnitsService.Delete(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  }


   [HttpPut("{Id}")]
         
        public  ActionResult<bool> Update(int Id,[FromBody] EngineeringUnitsEditDTO engineeringUnitsEditDTO){
           bool result= _engineeringUnitsService.Update(Id,engineeringUnitsEditDTO.Name);
            if (result)
            {
            return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        }  


        
    }
}