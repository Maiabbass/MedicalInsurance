using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimsExcel : ControllerBase

    {

    private readonly IClimsRepository _climsRepository;
    private readonly IAnnualDataService _annualDataService;
          
        
    public ClaimsExcel(IClimsRepository climsRepository , IAnnualDataService annualDataService)
      {
      _climsRepository= climsRepository;
      _annualDataService=annualDataService;
         }



     [HttpPost("upload")]
public async Task<IActionResult> UploadClaims(IFormFile file, [FromQuery] int year)
{
    try
    {
        if (file == null || file.Length == 0)
            return BadRequest("Please upload a valid Excel file.");

        if (year <= 0)
            return BadRequest("Please provide a valid year.");

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            stream.Position = 0;

            // Step 1: Process and load claims from Excel
            var claimsList = _climsRepository.ReadDataFromExcel(stream, year);
            await _climsRepository.LoadClaimsToDatabase(claimsList);
        }

        // Step 2: Update Beneficiary status for all persons in the claims for the given year
        await _annualDataService.UpdateBeneficiaryStatus(year);

        return Ok(new Response { Message = "Claims uploaded, processed, and beneficiary status updated successfully." });
    }
    catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
    {
        return Conflict(new Response { ErrorMessage = "Duplicate entry detected for unique index or constraint." });
    }
    catch (Exception ex)
    {
        string details = System.Text.Json.JsonSerializer.Serialize(new { FileName = file.FileName, FileSize = file.Length, Year = year });
        return StatusCode(StatusCodes.Status500InternalServerError,
            new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. Upload details: {details}" });
    }
}





    [HttpPut("{id}")]
    public IActionResult UpdateClaim(int id, [FromBody] ClaimEditDTO claimEditDTO)
    {
        if (claimEditDTO == null)
        {
            return BadRequest("Invalid data.");
        }

        var isUpdated = _climsRepository.UpdateClaim(id, claimEditDTO);

        if (!isUpdated)
        {
            return NotFound($"Claim with ID {id} was not found.");
        }

        return Ok("Claim updated successfully.");
    }



     [HttpGet]
    public async Task<ActionResult<List<ClaimDetailsDTO>>> GetClaims()
    {
        var claims = await _climsRepository.GetClaimsAsync();

        if (claims == null || claims.Count == 0)
        {
            return NotFound("No claims found.");
        }

        return Ok(claims);
    }



[HttpGet("by-ensurance-number/{ensuranceNumber}")]
public async Task<ActionResult<List<ClaimDetailsDTO>>> GetClaimsByEnsuranceNumber(string ensuranceNumber)
{
    var claims = await _climsRepository.GetClaimsByEnsuranceNumberAsync(ensuranceNumber);

    if (claims == null || claims.Count == 0)
    {
        return NotFound($"No claims found with Ensurance Number '{ensuranceNumber}'.");
    }

    return Ok(claims);
}



   [HttpPost]
   [Route("AddClaim")]
public async Task<IActionResult> AddClaim([FromBody] ClaimsDto claimsDto)
{
    if (claimsDto == null)
    {
        return BadRequest("Invalid claim data.");
    }

    // قم بتحويل DTO إلى الكلاس الرئيسي إذا لزم الأمر
    var claims = new Claims
    {
        EnsuranceNumber = claimsDto.EnsuranceNumber,
        FullName = claimsDto.FullName,
        TotalPrice = claimsDto.TotalPrice,
        Company_fees = claimsDto.Company_fees,
        ApprovedPrice = claimsDto.ApprovedPrice,
        non_Add = claimsDto.Non_Add,
        non_AddForPerson = claimsDto.Non_AddForPerson,
        EnduranceRatio = claimsDto.EnduranceRatio,
        HospitalId = claimsDto.HospitalId,
        LoginDate = claimsDto.LoginDate,
        ExitDate = claimsDto.ExitDate,
        PersonId = claimsDto.PersonId,
        SurgicalProceduresId = claimsDto.SurgicalProceduresId,
        ClimeData = claimsDto.ClimeData,
        Number = claimsDto.Number,
        Year=claimsDto.Year,

    };

    int newClaimId = await _climsRepository.Add(claims);
    return Ok(new { Id = newClaimId });
}


       [HttpDelete("{Id}")] 
      public ActionResult Delete(int Id){
      try{
                  _climsRepository.Delete(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  }



    [HttpGet("claims-between-dates")]
    public async Task<IActionResult> GetClaimsBetweenDates([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (startDate > endDate)
        {
            return BadRequest("Start date must be earlier than or equal to the end date.");
        }

        var claims = await _climsRepository.GetClaimsBetweenDatesAsync(startDate, endDate);

        if (claims == null || claims.Count == 0)
        {
            return NotFound("No claims found between the specified dates.");
        }

        return Ok(claims);
    }



}}