using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using static api.DTOS.RegisterAnnualDataDTO;
using static api.Repositories.AnnualDataRepository;
using static api.DTOS.AnnualDataForView;
using api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using api.Data;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnualData : ControllerBase
    {
        private readonly IAnnualDataService _AnnualDataService;
        private readonly IPersonRepository _personRepository;

        private readonly ISearchService _searchService;

        private readonly DataContext _dataContext ;
        

        public AnnualData(IAnnualDataService AnnualDataService,IPersonRepository personRepository , ISearchService searchService , DataContext dataContext )
        {
            _AnnualDataService =AnnualDataService;
            _personRepository=personRepository;
            _searchService = searchService;
            _dataContext = dataContext;
            
        }




        [HttpPost]
       public async Task<ActionResult<Response>> RegisterAnnualData([FromBody] RegisterAnnualDataDTO registerAnnualDataDTO)
{
    try
    {
       
        var response = await _AnnualDataService.Add(registerAnnualDataDTO);
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
        string details = System.Text.Json.JsonSerializer.Serialize(registerAnnualDataDTO);
        return StatusCode(StatusCodes.Status500InternalServerError,
            new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. Person details: {details}" });
    }
}
        





         [HttpGet("{Id}")]
                 public async Task<ActionResult<AnnualDataWithDetails?>>Get([FromRoute] int Id){
         return await _AnnualDataService.Get(Id);
}

 






 [HttpGet]
public async Task<ActionResult<IEnumerable<AnnualDataForView>>> GetAll(
    [FromQuery] int pageNumber = 1, 
    [FromQuery] int pageSize = 10)
{
    var data = await _AnnualDataService.GetAll(pageNumber, pageSize);
    var dataList = data.ToList();
    var annualDataForViews = new List<AnnualDataForView>();

    foreach (var item in dataList)
    {
        var annualDataForView = new AnnualDataForView
        {
            Id = item.AnnualData.Id,
            Year = item.AnnualData.Year,
            EngineereId = item.AnnualData.EngineereId,
            ExAmount = item.AnnualData.ExAmount,
            Amount = item.AnnualData.Amount,
            TotalAmount = item.AnnualData.TotalAmount,
            AnnualDataDetails = item.AnnualDataDetails.Select(detail => new AnnualDataDetailForView
            {
                Id = detail.Id,
                PersonId = detail.PersonId,
                AnnualDataId = detail.AnnualDataId,
                IsEngineer = detail.IsEngineer,
                Amount = detail.Amount
            }).ToList()
        };

        annualDataForViews.Add(annualDataForView);
    }

    return Ok(annualDataForViews);
}


      
    
   


    [HttpDelete("{Id}")] 
      public ActionResult Delete(int Id){
      try{
                  _AnnualDataService.Delete(Id);
                  return Ok("delete Successfully");}

  catch (Exception ex){
    return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
    
  } 








        [HttpPut]
        [Route("UpdateAllFields/{Id}")]
   
        public  ActionResult<bool> UpdateAllFields(int Id,  AnnualDataForView annualDataForView){
           bool result= _AnnualDataService.Update(Id,annualDataForView);
            if (result)
            {
            return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        } 




          [HttpPut]
          [Route("UpdateAmount/{Id}")]
         
        public  ActionResult<bool> UpdateAmount(int Id, AnnualDataDetailForView annualDataDetailForView){
           bool result= _AnnualDataService.Update(Id,annualDataDetailForView);
            if (result)
            {
            return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        }  


 // action method to get the amount of register annual data based on birth date and given Year 
          
  [HttpGet("CalculateAmount")]
public async Task<ActionResult<decimal>> CalculateAmountAsync(DateTime? birthdate, int year)
{
    

    decimal amount = 0m;
    try
    {
        // البحث عن الشخص باستخدام EnsuranceNumber
      //  var person = await _searchService.GetByEnsuranceNumberAsync(ensuranceNumber);

      //  if (person == null)
      //  {
     //       return NotFound(new Response { ErrorMessage = "Person not found." });
     //   }

        // حساب القسط
        amount = _AnnualDataService.calcualteAmount(birthdate, year);

        // تحديث الحقل Amount للشخص
       // person.Amount = amount;

        // حفظ التغييرات في قاعدة البيانات
        //await _personRepository.SavePerson(person);

        return Ok(amount);
    }
    catch (Exception ex)
    {
        var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
        return StatusCode(StatusCodes.Status500InternalServerError, new Response { ErrorMessage = $"{ex.Message} - Inner Exception: {innerExceptionMessage}" });
    }
}






       [HttpDelete("AnnualSetting/{year}")] 
      public ActionResult DeleteAnnualSetting(int year)
      
      {
      try     
                {
                bool completed=   _AnnualDataService.DeleteAnnuaSetting(year);
                if (completed)
                { return Ok("delete Successfully");}
                
                   return StatusCode(StatusCodes.Status500InternalServerError,

                    new Response { Status = "Error", ErrorMessage = "Delete Failed" }) ;
                }
                 catch (Exception ex){
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", ErrorMessage = ex.Message }) ;}
                    
         }



          [HttpPost]
          [Route("AnnualSetting")]
        public async Task<ActionResult< Response>> AddAnnualSetting([FromBody] AnnualSettingDTO annualSettingDTO)
        {
                var response =await _AnnualDataService.AddAnnualSettings(annualSettingDTO);
                  if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
                return Ok(response);
        } 

    [HttpPut]
    [Route("UpdateAnnualSettings")]
    public async Task<IActionResult> UpdateAnnualSettings( [FromBody] AnnualSettingDTO annualSettingDTO)
    {
        if (annualSettingDTO == null)
        {
            return BadRequest("Invalid data.");
        }

        

        var response = await _AnnualDataService.UpdateAnnualSettings( annualSettingDTO);

        if (response.Status == "Success")
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response.ErrorMessage);
        }
   }
   
   
      [HttpGet("GetByYear")]
public async Task<ActionResult<IEnumerable<AnnualDataWithDetails>>> GetByYear(
    [FromQuery] int year,
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10) 
{
    // التحقق من صحة القيم المدخلة
    if (pageNumber < 1) pageNumber = 1;
    if (pageSize < 1) pageSize = 10;
    if (pageSize > 100) pageSize = 100; // فرض حد أقصى للصفحات لتجنب الحمل الزائد

    try 
    {
        var pagedData = await _AnnualDataService.GetByYear(year, pageNumber, pageSize);
        
        // التحقق من وجود بيانات
        if (pagedData == null || !pagedData.Any()) 
        {
            return NotFound(new { message = "No data found for the specified year." });
        }

        return Ok(pagedData);
    } 
    catch (Exception ex) 
    {
        // سجل الخطأ إذا لزم الأمر
        return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request.", details = ex.Message });
    }
}





 [HttpGet("GetEngineerDetailsAndCardStatusByInsuranceNumber/{insuranceNumber}")]
public async Task<IActionResult> GetEngineerDetailsAndCardStatusByInsuranceNumber(string insuranceNumber, int year)
{
    var result = await _AnnualDataService.GetEngineerDetailsAndCardStatus(insuranceNumber, year);

    if (result.engineer == null)
    {
        return NotFound(new { Message = "لم يتم العثور على بيانات للرقم التأميني والسنة المقدمة." });
    }

    return Ok(new
    {
        Engineer = new
        {
            result.engineer.Id,
            result.engineer.EngNumber,
            result.engineer.SubNumber,
            Person = new
            {
                result.engineer.Person.Id,
                result.engineer.Person.FirstName,
                result.engineer.Person.FatherName,
                result.engineer.Person.LastName,
                result.engineer.Person.MotherName,
                result.engineer.Person.NationalId,
                result.engineer.Person.EnsuranceNumber,
                result.engineer.Person.Address,
                result.engineer.Person.Phone,
                result.engineer.Person.Mobile,
                result.engineer.Person.Email,
                result.engineer.Person.StatusId,
                result.engineer.Person.GenderId,
                result.engineer.Person.Amount,

            },
            Relations = result.engineer.Relations.Select(r => new
            {
                r.Id,
                r.Name,
                r.RelationTypeId,
                Person = new
                {
                    r.Person.Id,
                    r.Person.FirstName,
                    r.Person.FatherName,
                    r.Person.LastName,
                    r.Person.MotherName,
                    r.Person.NationalId,
                    r.Person.EnsuranceNumber,
                    r.Person.Address,
                    r.Person.Phone,
                    r.Person.Mobile,
                    r.Person.Email,
                    r.Person.StatusId,
                    r.Person.GenderId,
                    r.Person.Amount
                }
            }).ToList()
        },
        CardStatus = result.cardStatus,
        PayMethod = result.payMethod
    });
}



 [HttpGet("GetYearConfigurations")]
    public async Task<ActionResult<List<YearConfiguration>>> GetYearConfigurations()
    {
        var yearConfigurations = await _dataContext.YearConfigurations.ToListAsync();

        if (yearConfigurations == null || yearConfigurations.Count == 0)
        {
            return NotFound("No year configurations found.");
        }

        return Ok(yearConfigurations);
    }


 [HttpGet("GetAgeSegments")]
        public async Task<ActionResult<List<AgeSegments>>> GetAgeSegments()
        {
            var ageSegments = await _dataContext.AgeSegments.ToListAsync();

            if (ageSegments == null || ageSegments.Count == 0)
            {
                return NotFound("No age segments found.");
            }

            return Ok(ageSegments);
        }
    }
   
   }
