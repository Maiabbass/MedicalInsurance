using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using api.DTOS;
using api.Entities;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
          try{


              var response=  await _engineerService.Add(engineerPersonEditDTO);
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
                string Details = System.Text.Json.JsonSerializer.Serialize(engineerPersonEditDTO);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. Person details: {Details}" });
            }




        }

        
        [HttpGet("{Id}")]
public async Task<ActionResult<Engineere?>> Get(int Id)
{
    var engineer = await _engineerService.Get(Id);

    if (engineer == null)
    {
        return NotFound();
    }

    var options = new JsonSerializerOptions
    {
        ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
        WriteIndented = true // This is optional, for better readability of JSON output
    };

    var jsonResult = new JsonResult(engineer, options);

    return jsonResult;
}







 [HttpGet]
public async Task<ActionResult<PagedResult<PersonWithEngineereDTO>>> GetEngineersWithPersons([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
{
    var engineers = await _engineerService.GetAll(pageNumber, pageSize);
    
    // تحويل البيانات إلى PersonWithEngineereDTO
    var items = engineers.Select(e => new PersonWithEngineereDTO
    {
        PersonId = e.Persons.FirstOrDefault()?.Id ?? 0,
        FirstName = e.Persons.FirstOrDefault()?.FirstName,
        FatherName = e.Persons.FirstOrDefault()?.FatherName,
        LastName = e.Persons.FirstOrDefault()?.LastName,
        MotherName = e.Persons.FirstOrDefault()?.MotherName,
        NationalId = e.Persons.FirstOrDefault()?.NationalId,
        EnsuranceNumber = e.Persons.FirstOrDefault()?.EnsuranceNumber,
        BirthDate = e.Persons.FirstOrDefault()?.BirthDate,
        Address = e.Persons.FirstOrDefault()?.Address,
        Phone = e.Persons.FirstOrDefault()?.Phone,
        Mobile = e.Persons.FirstOrDefault()?.Mobile,
        Email = e.Persons.FirstOrDefault()?.Email,
        StatusId = e.Persons.FirstOrDefault()?.StatusId,
        GenderId = e.Persons.FirstOrDefault()?.GenderId,
        EngNumber = e.EngNumber,
        SubNumber = e.SubNumber,
        SpecializationId = e.SpecializationId,
        WorkPlaceId = e.WorkPlaceId,
        Amount = e.Persons.FirstOrDefault()?.Amount
    }).ToList();

    // إعداد بيانات النتيجة
    var pagedResult = new PagedResult<PersonWithEngineereDTO>
    {
        CurrentPage = pageNumber,
        TotalPages = (int)Math.Ceiling(engineers.Count() / (double)pageSize),
        PageSize = pageSize,
        TotalCount = engineers.Count(),
        Items = items
    };

    if (!pagedResult.Items.Any())
    {
        return NotFound();
    }

    return Ok(pagedResult);
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