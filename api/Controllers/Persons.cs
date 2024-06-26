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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Persons : ControllerBase
{
    private readonly IPersonService  _personService;
   

    public Persons(IPersonService  personService)
    {
        _personService = personService;
    }

   




     [HttpPost]
        public async Task<ActionResult<Response>> AddPerson([FromBody] PersonEditDTO personEditDTO)
        {
            try
            {
                var existingPerson = await _personService.GetEngId(personEditDTO.EngineereId);

                if (existingPerson == null)
                {
                    personEditDTO.Subscrib = true;
                }
                else
                {
                    personEditDTO.Affiliate = true;
                }

                bool isInClaims = await _personService.IsEnsuranceNumberInClaimsAsync(personEditDTO.EnsuranceNumber);
                if (isInClaims)
                {
                    personEditDTO.Beneficiary = true;
                }

                var response = await _personService.Add(personEditDTO);

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
                string personDetails = System.Text.Json.JsonSerializer.Serialize(personEditDTO);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. Person details: {personDetails}" });
            }
        }


        
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PersonForView>>> GetAll()
    {
         var Persons=await _personService.GetAll();
        List<PersonForView> personForViews=new List<PersonForView> ();
        foreach(var item in Persons)
        {
            PersonForView personForViewnewitem=new  PersonForView()
            {
              Id=item.Id,
              FirstName=item.FirstName,
              FatherName=item.FatherName,
              LastName=item.LastName,
              MotherName=item.MotherName,
              BirthDate=item.BirthDate,
              EnsuranceNumber=item.EnsuranceNumber,
              NationalId=item.NationalId,
              Address=item.Address,
              Phone=item.Phone,
              GenderId=item.GenderId,
              Mobile=item.Mobile,
              Email=item.Email,
            
            };
            personForViews.Add(personForViewnewitem);
        }
      return Ok(personForViews);
    }

[HttpGet("{Id}")]
public async Task<ActionResult<Person?>>GetWithId( int Id){
   return await _personService.GetWithId(Id);
}



[HttpDelete("{Id}")] 
public ActionResult Delete(int Id){
  try{
     _personService.Delete(Id);
    return NoContent();

  }
  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,
                      new Response { Status = "Error", ErrorMessage = ex.Message }) ;}}






      
     [HttpPut("{Id}")]


     public  ActionResult<bool> Update(int Id,[FromBody]  PersonEditDTO PersonEditDTO){
           bool result= _personService.Update(Id,PersonEditDTO);
            if (result)
            {
return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        }
    
  }

  



