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
    public class Hospitals : ControllerBase
    {
        

    private readonly IHospitalService _hospitalService;

    private readonly DataContext _dataContext;
          
        
    public Hospitals(IHospitalService hospitalService , DataContext dataContext)
      {
      _hospitalService= hospitalService;
      _dataContext= dataContext;
         }
        

         [HttpPost]
        public async Task <ActionResult<Response>> AddHospital([FromBody] HospitalEditDTO   hospitalEditDTO)
        {
           
    
                var response=  await _hospitalService.Add(hospitalEditDTO);
               if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
               return Ok (response);


        }
[HttpGet("{Id}")]
public async Task<ActionResult<Hospitals?>>Get( int Id){
   var data= await _hospitalService.Get(Id);
   return Ok(data);
}



[HttpGet()]
public async Task<ActionResult<Hospitals?>>GetAll(){
   
   var  data= await _hospitalService.GetAll();
     return Ok(data);
}

 [HttpPut("{Id}")]
        public  ActionResult<bool> Update(int Id,[FromBody] HospitalEditDTO hospital){
           bool result= _hospitalService.Update(Id,hospital);
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
                  _hospitalService.DeleteAsync(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  }



   [HttpGet("hospitalsInCityWithCityId/{id}")]
        public async Task<ActionResult<IEnumerable<Hospitals>>> GetHospitalsByCityId(int id)
        {
            var hospitals = await _hospitalService.GetHospitalsByCityIdAsync(id);

            if (hospitals == null || !hospitals.Any())
            {
                return NotFound("No hospitals found for the given city.");
            }

            return Ok(hospitals);
        }




         [HttpGet("hospitalsInYear/{year}")]
        public async Task<ActionResult<IEnumerable<Hospitals>>> GetHospitalsByYear(int year)
        {
            var hospitals = await _hospitalService.GetHospitalsByYear(year);

            if (hospitals == null || !hospitals.Any())
            {
                return NotFound("No hospitals found for the This Year.");
            }

            return Ok(hospitals);
        }





      [HttpGet("GetHospitals-With-Year/{year}")]
public async Task<IActionResult> GetHospitals(int year)
{
    // جلب المستشفيات بناءً على السنة
    var hospitals = await _dataContext.Hospitals
                                      .Where(h => h.Year == year)
                                      .ToListAsync();

    if (hospitals == null || !hospitals.Any())
    {
        return NotFound(new { message = $"No hospitals found for year {year}." });
    }

    var hospitalIds = hospitals.Select(h => h.Id).ToList();

    // جلب الملاحظات المرتبطة بالمستشفيات
    var notes = await _dataContext.Notes
                                  .Where(n => hospitalIds.Contains(n.HospitalId.Value))
                                  .ToListAsync();

    var result = hospitals.Select(h => new
    {
        HospitalId = h.Id,
        HospitalName = h.Name,
        Adress=h.Address,
        Enabled=h.Enabled,
        Inside=h.Inside,
        Phone=h.Phone,
        Email=h.Email,
        CityId=h.CityId,
        Latitude=h.latitude,
        Longitude=h.Longitude,

        Year = h.Year,
        Notes = notes
            .Where(note => note.HospitalId == h.Id)
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