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
        [HttpGet("{Id}")]
        public async Task<ActionResult<Engineere?>>Get( int Id){
   return await _engineerService.Get(Id);
}



    [HttpGet]
        public async Task<ActionResult<IEnumerable<Engineere>>> GetAll()
    {
         var data=await _engineerService.GetAll();
        
      return Ok(data);
    }



 [HttpPut("{Id}")]
        public  ActionResult<bool> Update(int Id,[FromBody] EngineerPersonEditDTO engineerPersonEditDTO){
           bool result= _engineerService.Update(Id,engineerPersonEditDTO);
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
                  _engineerService.Delete(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  }

     
        
    }


}