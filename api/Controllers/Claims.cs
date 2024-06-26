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
    public class Claims : ControllerBase

    {

    private readonly IClimsRepository _climsRepository;
          
        
    public Claims(IClimsRepository climsRepository)
      {
      _climsRepository= climsRepository;
         }

      [HttpPost("upload")]
        public async Task<IActionResult> UploadClaims(IFormFile file ,[FromForm] int EngineereeId, [FromForm] int SurgicalProceduresId)
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
}}