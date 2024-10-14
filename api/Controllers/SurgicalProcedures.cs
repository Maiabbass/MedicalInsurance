using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  

    [ApiController]
    [Route("api/[controller]")]
    public class SurgicalProcedures : ControllerBase
    
    {
        

    private readonly ISurgicalProceduresServices _SurgicalProceduresServices;
    private readonly DataContext _dataContext;
          
        
    public SurgicalProcedures(ISurgicalProceduresServices SurgicalProceduresServices , DataContext dataContext)
      {
      _SurgicalProceduresServices= SurgicalProceduresServices;
      _dataContext= dataContext;
         }
        

         [HttpPost]
        public async Task <ActionResult<Response>> AddSur([FromBody] SurgicalProceduresEditDTO   surgicalProceduresEditDTO)
        {
           
    
                var response=  await _SurgicalProceduresServices.Add(surgicalProceduresEditDTO);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);


        }
        [HttpPut("{Id}")]
        public  ActionResult<bool> Update(int Id,[FromBody] SurgicalProceduresEditDTO surgicalProceduresEditDTO){
           bool result= _SurgicalProceduresServices.Update(Id,surgicalProceduresEditDTO);
            if (result)
            {
return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        }


[HttpGet("{Id}")]
public async Task<ActionResult<SurgicalProcedures?>>Get( int Id){
   var data = await _SurgicalProceduresServices.Get(Id);
   return Ok(data);
}



[HttpGet]
public async Task<ActionResult<IEnumerable<SurgicalProcedures>>> GetAll()
    {
         var data=await _SurgicalProceduresServices.GetAll();
       
      return Ok(data);
    }



    
  [HttpDelete("{Id}")]
public async Task<IActionResult> Delete(int Id)
{
    try
    {
        var result = await _SurgicalProceduresServices.DeleteAsync(Id);

        if (result)
        {
            return Ok(new { Status = "Success", Message = "Deleted successfully" });
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new { Status = "Error", Message = "Failed to delete the surgical procedure" });
        }
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError,
            new { Status = "Error", Message = ex.Message });
    }
}


 [HttpGet("GetSurgicals-With-Year/{year}")]
public async Task<IActionResult> GetSurgicals(int year)
{
    // جلب العمليات الجراحية بناءً على السنة
    var surgicals = await _dataContext.SurgicalProcedures
                                      .Where(s => s.Year == year)
                                      .ToListAsync();

    if (surgicals == null || !surgicals.Any())
    {
        return NotFound(new { message = $"No surgicals found for year {year}." });
    }

    var surgicalIds = surgicals.Select(s => s.Id).ToList();

    // جلب الملاحظات المرتبطة بالعمليات الجراحية
    var notes = await _dataContext.Notes
                                  .Where(n => surgicalIds.Contains(n.SurgicalProcedureId.Value))
                                  .ToListAsync();

    var result = surgicals.Select(s => new
    {
        SurgicalId = s.Id,
        SurgicalName = s.Name,
        Pathological_specialization=s.Pathological_specialization,
        Price=s.Price,
        Ceiling=s.Ceiling,
        IN=s.IN,
        OUT=s.OUT,
        Year = s.Year,
        Notes = notes
            .Where(note => note.SurgicalProcedureId == s.Id)
            .Select(note => new
            {
                note.Id,
                note.Content
            })
    });

    return Ok(result);
}

    }
}