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

namespace api.Controllers
{


[ApiController]
[Route("api/[controller]")]
public class Persons : ControllerBase
{
    private readonly IPersonService  _personService;

    private readonly IWordRepository _wordRepository;

    private readonly IImageRepository _imageRepository;

    private readonly INoteRepository _noteRepository;
   

    public Persons(IPersonService  personService , IWordRepository wordRepository , IImageRepository imageRepository, INoteRepository noteRepository)
    {
        _personService = personService;
        _imageRepository=imageRepository;
        _wordRepository=wordRepository;
        _noteRepository=noteRepository;
    }

   


   [HttpPost]
public async Task<IActionResult> CreatePerson([FromForm] PersonEditDTO personEditDTO, IFormFile[]? ImageFiles, IFormFile[]? WordFiles)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    var response = await _personService.Add(personEditDTO, ImageFiles, WordFiles);

    if (!string.IsNullOrEmpty(response.ErrorMessage))
    {
        return StatusCode(500, new { message = response.ErrorMessage });
    }

    return Ok(new { Id = response.InsertedId });
}




        
  [HttpGet]
public async Task<ActionResult<PagedResult<PersonForView>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
{
    var pagedPersons = await _personService.GetAll(pageNumber, pageSize);

    if (!pagedPersons.Items.Any())
    {
        return NotFound();
    }

    return Ok(pagedPersons);
}




[HttpGet("{Id}")]
public async Task<ActionResult<Person?>> GetWithId(int Id)
{
    var person = await _personService.GetWithId(Id);

    if (person == null)
    {
        return NotFound();
    }

    var personForView  = new PersonForView
    {
        Id = person.Id,
        FirstName = person.FirstName,
        FatherName = person.FatherName,
        MotherName = person.MotherName,
        LastName = person.LastName,
        BirthDate = person.BirthDate,
        NationalId = person.NationalId,
        EnsuranceNumber = person.EnsuranceNumber,
        Address = person.Address,
        Phone = person.Phone,
        Mobile = person.Mobile,
        Email = person.Email,
        GenderId = person.GenderId,
        StatusId = person.StatusId,
        Amount = (decimal)person.Amount,
        Images = person.Images.Select(i => Convert.ToBase64String(i.Image)).ToList(),
        WordFiles = person.Words.Select(w => Convert.ToBase64String(w.Content)).ToList()
    };

    return Ok(personForView);
}



 
 [HttpDelete("{Id}")] 
public ActionResult Delete(int Id){
  try{
   
    _imageRepository.DeleteByPersonId(Id);
    _wordRepository.DeleteByPersonId(Id);
     _personService.Delete(Id);
    return NoContent();

  }
  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,
                      new Response { Status = "Error", ErrorMessage = ex.Message }) ;}}






   [HttpPut("{id}/update-person-details")]
public async Task<IActionResult> UpdatePersonDetails(int id, [FromBody] PersonEditDTO personDetailsDTO)
{
    var updatePersonResult = await _personService.UpdatePersonDetails(id, personDetailsDTO);
    
    if (!updatePersonResult)
    {
        return NotFound(); // إذا لم يتم العثور على الشخص
    }
    
    return Ok(); // في حال نجاح التحديث
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

  



