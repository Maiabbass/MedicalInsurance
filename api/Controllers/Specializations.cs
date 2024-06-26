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
    public class Specializations : ControllerBase
    {

    private readonly ISpecializationService  _spcializationService;
   

    public Specializations(ISpecializationService  spcializationService)
    {
        _spcializationService = spcializationService;
    }

    [HttpPost]
        public async Task<ActionResult<Response>> AddPerson([FromBody] SpecializationEditDto specializationEditDto)
        {
            try
            {
              
                var response = await _spcializationService.Add(specializationEditDto);

                if (response.ErrorMessage != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response { ErrorMessage = response.ErrorMessage });
                }

                return Ok(response);
            }
            catch (Exception ex) when (ex is DbUpdateException dbUpdateEx && dbUpdateEx.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                return Conflict(new Response { ErrorMessage = "Duplicate entry detected for unique index or constraint." });
            }
            catch (Exception ex)
            {
                string personDetails = System.Text.Json.JsonSerializer.Serialize(specializationEditDto);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. Person details: {personDetails}" });
            }
        }

        [HttpGet("{Id}")]
    public async Task<ActionResult<Specializations?>>Get( int Id){
      var data= await _spcializationService.GetWithId(Id);
      return Ok(data);
}



    [HttpGet()]
    public async Task<ActionResult<Specializations?>>GetAll(){
   
    var  data= await _spcializationService.GetAll();
     return Ok(data);
}



   [HttpDelete("{Id}")] 
   public ActionResult Delete(int Id){
  try{
     _spcializationService.Delete(Id);
    return NoContent();

  }
  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,
                      new Response { Status = "Error", ErrorMessage = ex.Message }) ;}}



        [HttpPut("{Id}")]
        public  ActionResult<bool> Update(int Id,[FromBody] SpecializationEditDto specializationEditDto){
           bool result= _spcializationService.Update(Id,specializationEditDto);
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