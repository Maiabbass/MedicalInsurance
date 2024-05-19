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
[HttpGet("{Id}")]
public async Task<ActionResult<Hospitals?>>Get( int Id){
   var data= await _hospitalService.Get(Id);
   return Ok(data);
}



[HttpGet()]
public async Task<ActionResult<Hospitals?>>GetAll(){
   
   var  data= await _hospitalService.GetAll();
     return Ok(data);
}

 [HttpPut("{Id}")]
        public  ActionResult<bool> Update(int Id,[FromBody] HospitalEditDTO hospital){
           bool result= _hospitalService.Update(Id,hospital);
            if (result)
            {
return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        }


         [HttpDelete("{Id}")] 
      public ActionResult Delete(int Id){
      try{
                  _hospitalService.Delete(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  }

        
    }

}