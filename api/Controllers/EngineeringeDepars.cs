using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
                try{
           
    
                var response=  await _engineeringeDeparServices.Add(engineeringeDeparEditDTO);
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
                string Details = System.Text.Json.JsonSerializer.Serialize(engineeringeDeparEditDTO);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. Person details: {Details}" });
            }

        }        

        [HttpGet("{Id}")]
     public async Task<ActionResult<EngineeringeDepar?>>Get( int Id){
   var data= await _engineeringeDeparServices.Get(Id);
   return Ok(data);
}



       [HttpGet()]
        public async Task<ActionResult<Hospitals?>>GetAll(){
   
        var  data= await _engineeringeDeparServices.GetAll();
        return Ok(data);
}        

            [HttpPut("{Id}")]
        public  ActionResult<bool> Update(int Id,[FromBody] EngineeringeDeparEditDTO eng){
           bool result= _engineeringeDeparServices.Update(Id,eng);
            if (result)
            {
return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        }


        [HttpDelete("{id}")]
public ActionResult Delete(int id)
{
    try
    {
        bool result = _engineeringeDeparServices.Delete(id);
       
            return Ok("Deleted Successfully");
        
    }
    catch (Exception ex)
    {
        // تسجيل الخطأ
        Console.WriteLine($"Exception: {ex.Message}");
        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", ErrorMessage = ex.Message });
    }
}
        
        }

        }