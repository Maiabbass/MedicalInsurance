using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Services;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using api.DTOS;

namespace api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Searchs : ControllerBase
    {

    private readonly ISearchService  _searchService;

    public Searchs(ISearchService  searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    [Route("find/ByEnsuranceNumber/{ensuranceNumber}")]
    public async Task<ActionResult<PersonWithEngineereDTO>> GetByEnsuranceNumberAsync(string ensuranceNumber)
        {
            var personWithEngineere = await _searchService.GetByEnsuranceNumberAsync(ensuranceNumber);

            if (personWithEngineere == null)
            {
                return NotFound();
            }

            return personWithEngineere;
        }



    [HttpGet]
    [Route("find/ByName/{userSearch}")]

    public async Task<ActionResult<PersonWithEngineereDTO?>>GetWithName( string userSearch){
    return Ok( await _searchService.GetWithNameAsync(userSearch));
    }


/*
    [HttpGet]
    [Route("find/ByNationalId/{NationalId}")]   
   
    public async Task<ActionResult<PersonWithEngineereDTO>> GetByNationalIdAsync(string nationalId)
    {
        try
        {
            var result = await _searchService.GetByNationalIdAsync(nationalId);
            if (result == null)
            {
                return NotFound($"No person found with the NationalId: {nationalId}");
            }
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // Log the exception here if you have logging configured
            return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
        }
    }

    */




    [HttpGet("find/Search-By-National-Id")]
public async Task<IActionResult> SearchByNationalId(string nationalId)
{
    var result = await _searchService.GetPersonWithEngineerByNationalIdAsync(nationalId);
    if (result == null)
    {
        return NotFound("Person not found with the provided National ID.");
    }
    return Ok(result);
}




    [HttpGet]
    [Route("find/ByEngNumber/{engNumber}")]   
    public async Task<ActionResult<PersonWithEngineereDTO>> GetByEngNumberAsync(string engNumber){
    return Ok(await _searchService.GetEngNumberAsync(engNumber));
    }



    [HttpGet("find/EngUnits/{name}")]
        public async Task<ActionResult<IEnumerable<EngineeringUnits>>> GetEngUnits(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name parameter is required.");
            }

            var results = await _searchService.GetEngUnits(name);

            if (results == null || !results.Any())
            {
                return NotFound("No engineering units found with the given name.");
            }

            return Ok(results);
        }




        [HttpGet("find/WorkPlace/{name}")]
        public async Task<ActionResult<IEnumerable<WorkPlace>>> GetWorkPlace(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name parameter is required.");
            }

            var results = await _searchService.GetWorkPlace(name);

            if (results == null || !results.Any())
            {
                return NotFound("No engineering units found with the given name.");
            }

            return Ok(results);
        }


         [HttpGet("find/Hospital/{name}")]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospital(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name parameter is required.");
            }

            var results = await _searchService.GetHospital(name);

            if (results == null || !results.Any())
            {
                return NotFound("No engineering units found with the given name.");
            }

            return Ok(results);
        }



        [HttpGet("find/Claim/{ensuranceNumber}")]
        public async Task<ActionResult<IEnumerable<Claims>>> GetClaim(string ensuranceNumber)
        {
            if (string.IsNullOrEmpty(ensuranceNumber))
            {
                return BadRequest("Name parameter is required.");
            }

            var results = await _searchService.GetClaim(ensuranceNumber);

            if (results == null || !results.Any())
            {
                return NotFound("No engineering units found with the given name.");
            }

            return Ok(results);
        }


       [HttpGet]
[Route("find/ByEngNumberAndSupNumber/{engNumber}/{supNumber}")]
public async Task<ActionResult<PersonWithEngineereDTO>> GetByEngNumberAndSupNumberAsync(string engNumber, string supNumber)
{
    return Ok(await _searchService.GetEngNumberAndSupNumber(engNumber, supNumber));
}





       [HttpGet]
    [Route("find/BySubNumber/{subNumber}")]   
    public async Task<ActionResult<PersonWithEngineereDTO>> GetBySubNumber(string subNumber){
    return Ok(await _searchService.GetSubNumberAsync(subNumber));
    }





 


        
    }


    
}