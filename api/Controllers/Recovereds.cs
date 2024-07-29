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
    public class Recovereds : ControllerBase
    {

    private readonly IRecoveredServices _recoveredServices;
          
        
    public Recovereds(IRecoveredServices recoveredServices )
      {
      _recoveredServices= recoveredServices;
         }

         [HttpPost]
        public async Task <ActionResult<Response>> Add([FromBody] RecoveredDto   recoveredDto)
        {
         

                var response=  await _recoveredServices.Add(recoveredDto);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);
        
    
        }
        [HttpPut("{Id}")]
        public  ActionResult<bool> Update(int Id,[FromBody] RecoveredDto reco){
           bool result= _recoveredServices.Update(Id,reco);
            if (result)
            {
return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        }


[HttpGet("{Id}")]
public async Task<ActionResult<Recovereds?>>Get( int Id){
   var data= await _recoveredServices.Get(Id);
   return Ok(data);
}



[HttpGet]
public async Task<ActionResult<RecoveredSummary>> GetAll()
{
    var data = await _recoveredServices.GetAll();
    return Ok(data);
}



   [HttpDelete("{Id}")] 
      public ActionResult Delete(int Id){
      try{
                  _recoveredServices.Delete(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  }


  [HttpGet("GetByEnsuranceNumber/{ensuranceNumber}")]
public async Task<ActionResult<RecoveredSummary>> GetByEnsuranceNumber(string ensuranceNumber)
{
    var data = await _recoveredServices.GetByEnsuranceNumber(ensuranceNumber);
    return Ok(data);
}

        
        
        
        }
}