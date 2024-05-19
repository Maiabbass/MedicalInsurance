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
        [HttpPut("{Id}")]
        public  ActionResult<bool> Update(int Id,[FromBody] CityEditDTO city){
           bool result= _cityService.Update(Id,city);
            if (result)
            {
return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        }


[HttpGet("{Id}")]
public async Task<ActionResult<City?>>Get( int Id){
   return await _cityService.Get(Id);
}



[HttpGet]
public async Task<ActionResult<City?>>GetAll(){
   
   var  data= await _cityService.GetAll();
     return Ok(data);
}


   [HttpDelete("{Id}")] 
      public ActionResult Delete(int Id){
      try{
                  _cityService.Delete(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  }

        
    }

}
        
 