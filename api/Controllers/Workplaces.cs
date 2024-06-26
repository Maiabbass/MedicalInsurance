using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
          try{

                var response=  await _workplaceService.Add(workplaceEditDTO);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);
          }
             catch (Exception ex) when (ex is DbUpdateException dbUpdateEx && dbUpdateEx.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                return Conflict(new Response { ErrorMessage = "Duplicate entry detected for unique index or constraint." });
            }
            catch (Exception ex)
            {
                string Details = System.Text.Json.JsonSerializer.Serialize(workplaceEditDTO);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. Person details: {Details}" });
            }


        }

        [HttpGet("{Id}")]
         public async Task<ActionResult<Workplaces?>>Get( int Id){
            var data= await _workplaceService.Get(Id);
            return Ok(data);
}



[HttpGet]
public async Task<ActionResult<Workplaces?>>GetAll(){
   
   var  data= await _workplaceService.GetAll();
     return Ok(data);
}

    [HttpDelete("{Id}")] 
      public ActionResult Delete(int Id){
      try{
                  _workplaceService.Delete(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  }


     [HttpPut("{Id}")]
         
        public  ActionResult<bool> Update(int Id, WorkplaceEditDTO workplaceEditDTO){
           bool result= _workplaceService.Update(Id,workplaceEditDTO);
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