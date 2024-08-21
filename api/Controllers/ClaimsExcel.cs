using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Repositories;
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
          
        
    public ClaimsExcel(IClimsRepository climsRepository)
      {
      _climsRepository= climsRepository;
         }

      [HttpPost("upload")]
        public async Task<IActionResult> UploadClaims(IFormFile file)
        {
            try{

            
            if (file == null || file.Length == 0)
                return BadRequest("Please upload a valid Excel file.");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;
                var claimsList = _climsRepository.ReadDataFromExcel(stream);
                await _climsRepository.LoadClaimsToDatabase(claimsList);
            }

            return Ok("Operation accomplished successfully");
        }
        
        catch (Exception ex) when (ex is DbUpdateException dbUpdateEx && dbUpdateEx.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
            {
                return Conflict(new Response { ErrorMessage = "Duplicate entry detected for unique index or constraint." });
            }
            catch (Exception ex)
            {
                string Details = System.Text.Json.JsonSerializer.Serialize(file);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. Person details: {Details}" });
            }
    }



     [HttpPut("{claimId}/surgical-procedure")]
public async Task<IActionResult> EditClaimSurgicalProcedure(int claimId, [FromBody] string surgicalProcedureName, [FromQuery] DateTime? newClaimDate = null)
{
    var result = await _climsRepository.UpdateSurgicalProcedureAsync(claimId, surgicalProcedureName, newClaimDate);

    if (!result)
    {
        return NotFound($"Either the claim with Id '{claimId}' or the surgical procedure '{surgicalProcedureName}' was not found.");
    }

    return Ok($"Claim with Id '{claimId}' has been updated with Surgical Procedure '{surgicalProcedureName}' and new claim date '{newClaimDate?.ToString("yyyy-MM-dd") ?? "unchanged"}'.");
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

}}