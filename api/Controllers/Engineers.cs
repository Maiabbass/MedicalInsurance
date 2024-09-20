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

   private readonly IWordRepository _wordRepository;
   private readonly IImageRepository _imageRepository;
          
        
 public Engineers(IEngineerService engineerService , IWordRepository wordRepository, IImageRepository imageRepository)
{
  _engineerService= engineerService;
  _wordRepository=wordRepository;
  _imageRepository=imageRepository;

}
        




    [HttpPost]
public async Task<ActionResult<Response>> AddEngineer([FromForm] EngineerPersonEditDTO engineerPersonEditDTO, IFormFile[]? ContentImage , IFormFile[]? ContentFile)
{
    try
    {
        var response = await _engineerService.Add(engineerPersonEditDTO, ContentImage, ContentFile);
        
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
        string details = System.Text.Json.JsonSerializer.Serialize(engineerPersonEditDTO);
        return StatusCode(StatusCodes.Status500InternalServerError,
            new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. Person details: {details}" });
    }
}
    

        

        
[HttpGet("{Id}")]
public async Task<ActionResult<PersonWithEngineereDTO?>> Get(int Id)
{
    var engineer = await _engineerService.Get(Id);

    if (engineer == null)
    {
        return NotFound();
    }

    return Ok(engineer);
}







[HttpGet]
public async Task<ActionResult<PagedResult<PersonWithEngineereDTO>>> GetEngineersWithPersons([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
{
    var pagedResult = await _engineerService.GetAll(pageNumber, pageSize);

    if (!pagedResult.Items.Any())
    {
        return NotFound();
    }

    return Ok(pagedResult);
}








 [HttpPut("{Id}/update-Eng-details")]
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
                 _imageRepository.DeleteByPersonId(Id);
                 _wordRepository.DeleteByPersonId(Id);
                  _engineerService.Delete(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  }

     

 [HttpPut("{id}/update-imagesAndWords")]
public async Task<IActionResult> UpdateImagesANDWords(int id, [FromForm] ImageAndWordUpdateDTO DTO)
{
    if (DTO.Images == null || !DTO.Images.Any())
    {
        return BadRequest("No images provided.");
    }

    var newImageFilesBase64 = new List<string>();
    foreach (var imageFile in DTO.Images)
    {
        using (var ms = new MemoryStream())
        {
            await imageFile.CopyToAsync(ms);
            var fileBytes = ms.ToArray();
            newImageFilesBase64.Add(Convert.ToBase64String(fileBytes));
        }
    }

    await _imageRepository.UpdateImagesAsync(id, newImageFilesBase64);

    if (DTO.Words != null && DTO.Words.Any())
    {
        var newWordFilesBase64 = new List<string>();

        foreach (var wordFile in DTO.Words)
        {
            using (var ms = new MemoryStream())
            {
                await wordFile.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                newWordFilesBase64.Add(Convert.ToBase64String(fileBytes));
            }
        }

        await _wordRepository.UpdateWordFilesAsync(id, newWordFilesBase64);
    }

    return Ok();
}


        
    }


}