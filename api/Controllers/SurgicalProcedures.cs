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
    public class SurgicalProcedures : ControllerBase
    
    {
        

    private readonly ISurgicalProceduresServices _SurgicalProceduresServices;
          
        
    public SurgicalProcedures(ISurgicalProceduresServices SurgicalProceduresServices)
      {
      _SurgicalProceduresServices= SurgicalProceduresServices;
         }
        

         [HttpPost]
        public async Task <ActionResult<Response>> AddSur([FromBody] SurgicalProceduresEditDTO   surgicalProceduresEditDTO)
        {
           
    
                var response=  await _SurgicalProceduresServices.Add(surgicalProceduresEditDTO);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);


        }
        [HttpPut("{Id}")]
        public  ActionResult<bool> Update(int Id,[FromBody] SurgicalProceduresEditDTO surgicalProceduresEditDTO){
           bool result= _SurgicalProceduresServices.Update(Id,surgicalProceduresEditDTO);
            if (result)
            {
return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        }


[HttpGet("{Id}")]
public async Task<ActionResult<SurgicalProcedures?>>Get( int Id){
   var data = await _SurgicalProceduresServices.Get(Id);
   return Ok(data);
}



[HttpGet]
public async Task<ActionResult<IEnumerable<SurgicalProcedures>>> GetAll()
    {
         var data=await _SurgicalProceduresServices.GetAll();
       
      return Ok(data);
    }



    
  [HttpDelete("{Id}")]
public async Task<IActionResult> Delete(int Id)
{
    try
    {
        var result = await _SurgicalProceduresServices.DeleteAsync(Id);

        if (result)
        {
            return Ok(new { Status = "Success", Message = "Deleted successfully" });
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { Status = "Error", Message = "Failed to delete the surgical procedure" });
        }
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError,
            new { Status = "Error", Message = ex.Message });
    }
}

    }
}