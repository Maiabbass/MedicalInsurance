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

        private readonly IAgeSegmentsRepository _ageSegmentsRepository;

        private readonly IRelationRepository _relationRepository;

        

        public AnnualData(IAnnualDataService AnnualDataService,IPersonRepository personRepository , ISearchService searchService , DataContext dataContext , IAgeSegmentsRepository ageSegmentsRepository , IRelationRepository relationRepository )
        {
            _AnnualDataService =AnnualDataService;
            _personRepository=personRepository;
            _searchService = searchService;
            _dataContext = dataContext;
            _ageSegmentsRepository=ageSegmentsRepository;
            _relationRepository=relationRepository;
            
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
        [Route("UpdateAnnualDataEng/{Id}")]
   
        public  ActionResult<bool> UpdateAllFields(int Id,  AnnalDataForEdit annualDataForEdit){
           bool result= _AnnualDataService.Update(Id,annualDataForEdit);
            if (result)
            {
            return  Ok(result);
            }
            else{
                return StatusCode(StatusCodes.Status500InternalServerError,result);
            }

        } 




          [HttpPut]
          [Route("UpdateAnnualDataPerson/{Id}")]
         
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






     [HttpDelete("DeleteAnnualSetting/{year}")]
public async Task<ActionResult> DeleteAnnualSetting(int year)
{
    try
    {
        // Call the asynchronous delete method
        bool completed = await _AnnualDataService.DeleteAnnuaSetting(year);

        if (completed)
        {
            return Ok("Deleted successfully");
        }

        return StatusCode(StatusCodes.Status500InternalServerError, 
            new Response { Status = "Error", ErrorMessage = "Delete failed" });
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, 
            new Response { Status = "Error", ErrorMessage = ex.Message });
    }
}




 [HttpPost]
[Route("AnnualSetting/{title}")]
public async Task<ActionResult<Response>> AddAnnualSetting(string title, [FromBody] AnnualSettingDTO annualSettingDTO)
{
    var response = await _AnnualDataService.AddAnnualSettings(title, annualSettingDTO);

    if (response.ErrorMessage != null)
    {
        return StatusCode(StatusCodes.Status500InternalServerError,
            new Response { ErrorMessage = response.ErrorMessage });
    }

    return Ok(response);
}







       [HttpPut("UpdateYearConfiguration")]
    public async Task<IActionResult> UpdateYearConfiguration([FromBody] YearConfigurationDTO yearConfigurationDTO)
    {
        if (yearConfigurationDTO == null)
        {
            return BadRequest("Invalid YearConfiguration data.");
        }

        try
        {
            // Convert DTO to the actual entity
            var yearConfiguration = new YearConfiguration
            {
                Id = yearConfigurationDTO.Id,
                Year = yearConfigurationDTO.Year,
                CardPrice = yearConfigurationDTO.CardPrice
            };

            await _AnnualDataService.UpdateYearConfigurationAsync(yearConfiguration);
            return Ok("Year configuration updated successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }



    
    [HttpPut("UpdateAgeSegments")]
    public async Task<IActionResult> UpdateAgeSegment([FromBody] AgeSegments ageSegment)
    {
        if (ageSegment == null)
        {
            return BadRequest("Invalid age segment data.");
        }

        try
        {
            await _ageSegmentsRepository.Update_Age_Segment(ageSegment);
            return Ok("Age segment updated successfully.");
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message); // If the age segment doesn't exist
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }




 [HttpPut("update-relation-type")]
public async Task<IActionResult> UpdateRelationType([FromBody] RelationTypeDTO relationTypeDTO)
{
    if (relationTypeDTO == null)
        return BadRequest("Relation type cannot be null.");

    try
    {
        var relationType = new RelationType
        {
            Id = relationTypeDTO.Id,
            Name = relationTypeDTO.Name,
            Year = relationTypeDTO.Year
        };

        await _relationRepository.Update_RelationType(relationType);
        return Ok("Relation type updated successfully.");
    }
    catch (ArgumentException ex)
    {
        return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
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



        [HttpGet("GetRelationType")]
public async Task<ActionResult<List<object>>> GetRelationType()
{
    var items = await _dataContext.RelationTypes
                                   .Select(rt => new 
                                   {
                                       rt.Id,
                                       rt.Name,
                                       rt.Year
                                   })
                                   .ToListAsync();

    if (items == null || items.Count == 0)
    {
        return NotFound("No relation types found.");
    }

    return Ok(items);
}




          


       [HttpGet("GetAllConfigData/{year}")]
public async Task<ActionResult> GetAllData(int year)
{
    var yearConfigurations = await _dataContext.YearConfigurations
        .Where(yc => yc.Year == year)
        .ToListAsync();

    var ageSegments = await _dataContext.AgeSegments
        .Where(asg => asg.Year == year)
        .ToListAsync();

     var items = await _dataContext.RelationTypes
                                   .Select(rt => new 
                                   {
                                       rt.Id,
                                       rt.Name,
                                       rt.Year
                                   }).Where(i => i.Year==year)
                                   .ToListAsync();


  

    var result = new
    {
        YearConfigurations = yearConfigurations,
        AgeSegments = ageSegments,
        RelationTypes = items,
       
    };

    if (!result.YearConfigurations.Any() && !result.AgeSegments.Any() &&
        !result.RelationTypes.Any() )
    {
        return NotFound($"No data found for the year {year}.");
    }

    return Ok(result);
}




        
    [HttpGet("get-allPayMethode")]
    public async Task<IActionResult> GetAllPayMethods()
    {
        var payMethods = await _dataContext.PayMethods.ToListAsync();

        if (payMethods == null || payMethods.Count == 0)
        {
            return NotFound("No pay methods found.");
        }

        return Ok(payMethods);
    }





    [HttpGet("check/{ensuranceNumber}/{year}")]
    public async Task<IActionResult> CheckPersonStatus(string ensuranceNumber, int year)
    {
        var annualData = await _AnnualDataService.GetAnnualDataDetailAsync(ensuranceNumber, year);
        
        if (annualData == null)
        {
            return NotFound(new { Message = "  صاحب الرقم التأميني غير مسجل لهذا العام" });
        }



        if (annualData.Subscrib && annualData.Affiliate && !annualData.Beneficiary)
        {
            return Ok(new { Message = "  صاحب الرقم التأميني مسجل"   });
        }
        else if (annualData.Subscrib && annualData.Affiliate && annualData.Beneficiary)
        {
            var totalNonAdd = await _AnnualDataService.GetClaimsSumForPersonAsync(ensuranceNumber);
            return Ok(new
            {
                Message = "صاحب الرقم التأميني مستفيد",
                TotalNonAddForPerson = totalNonAdd,
                Year = year
            });
        }

        return BadRequest(new { Message = "حالة غير معروفة" });
    }


     
/*

  [HttpPost("CopyAnnualDataForNewYear")]
public async Task<ActionResult<Response>> CopyAnnualDataForNewYear(
    [FromQuery] int previousYear, 
    [FromQuery] int newYear, 
    [FromQuery] string ensuranceNumber, 
    [FromQuery] bool waiting = false,  // القيمة الافتراضية False
    [FromQuery] bool cardStatus = false,
    [FromQuery] bool copyAnnualData = true, 
    [FromQuery] bool copyAnnualDataDetails = true,
    [FromBody] List<AnnualNewDTO> detailIdsToCopy = null // استخدام DTO هنا
)  
{
    try
    {
        var response = await _AnnualDataService.CopyAnnualDataForNewYear(
            previousYear, 
            newYear, 
            ensuranceNumber, 
            waiting, 
            cardStatus,     
            copyAnnualData, 
            copyAnnualDataDetails,
            detailIdsToCopy // تمرير قائمة الـ DTO إلى الخدمة
        );

        if (response.ErrorMessage != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new Response { ErrorMessage = response.ErrorMessage });
        }

        return Ok(response);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, 
            new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. InsuranceNumber: {ensuranceNumber}, PreviousYear: {previousYear}, NewYear: {newYear}" });
    }
}

*/



[HttpGet("GetPayMethodByEngineerId")]
public async Task<ActionResult<PayMethod>> GetPayMethodByEngineerId(int engineerId)
{
    try
    {
        var payMethod = await _AnnualDataService.GetPayMethodByEngineerIdAsync(engineerId);
        if (payMethod == null)
        {
            return NotFound("No payment method found for the given engineer.");
        }
        return Ok(payMethod);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
    }
}





 [HttpPost("{yearConfigId}/notes")]
    public async Task<IActionResult> AddNoteToYearConfig(int yearConfigId, [FromBody] NoteCreateDTO noteDto)
    {
        try
        {
            await _AnnualDataService.AddNoteToYearConfigAsync(yearConfigId, noteDto);
            return Ok("Note added successfully.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }





   [HttpPost("RenewEngineerAnnualData")]
public async Task<ActionResult<Response>> RenewEngineerAnnualData(
    [FromQuery] int previousYear,
    [FromQuery] int newYear,
    [FromQuery] string insuranceNumber,
    [FromQuery] bool waiting,
    [FromQuery] bool cardStatus)
{
    try
    {
        var response = await _AnnualDataService.RenewEngineerAnnualData(
            previousYear, newYear, insuranceNumber, waiting, cardStatus);

        if (response.ErrorMessage != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new Response { ErrorMessage = response.ErrorMessage });
        }

        return Ok(response);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, 
            new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. InsuranceNumber: {insuranceNumber}, PreviousYear: {previousYear}, NewYear: {newYear}" });
    }
}





[HttpPost("RenewFamilyMembersAnnualData")]
public async Task<ActionResult<Response>> RenewFamilyMembersAnnualData(
    [FromQuery] int engineerId,
    [FromQuery] int previousYear,
    [FromQuery] int newYear,
    [FromBody] List<FamilyMemberRenewalDTO> familyMembersToRenew)
{
    try
    {
        var response = await _AnnualDataService.RenewFamilyMembersAnnualData(
            engineerId, previousYear, newYear, familyMembersToRenew);

        if (response.ErrorMessage != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                new Response { ErrorMessage = response.ErrorMessage });
        }

        return Ok(response);
    }
    catch (Exception ex)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, 
            new Response { ErrorMessage = $"An unexpected error occurred: {ex.Message}. EngineerId: {engineerId}, PreviousYear: {previousYear}, NewYear: {newYear}" });
    }
}




 [HttpGet("GetEngineerStatus/{engineerId}")]
    public async Task<IActionResult> GetEngineerStatusByYear(int engineerId)
    {
        var result = await _AnnualDataService.GetEngineerStatusByYear(engineerId);
        if (result == null || result.Count == 0)
        {
            return NotFound("No data found for this engineer.");
        }

        return Ok(result);
    }

    


    [HttpGet("GetFamilyMemberStatus/{personId}")]
public async Task<IActionResult> GetFamilyMemberStatusByYear(int personId)
{
    var result = await _AnnualDataService.GetFamilyMemberStatusByYear(personId);
    if (result == null || result.Count == 0)
    {
        return NotFound("No data found for this family member.");
    }

    return Ok(result);
}




[HttpGet("GetYearConfigurations/{year}")]
public async Task<ActionResult<List<YearConfigurationDTO>>> GetYearConfigurations(int year)
{
    var yearConfigurations = await _dataContext.YearConfigurations
                                               .Where(yc => yc.Year == year)
                                               .Select(yc => new YearConfigurationDTO
                                               {
                                                   Id = yc.Id,
                                                   CardPrice= (decimal)yc.CardPrice,
                                                  
                                               })
                                               .ToListAsync();

    if (yearConfigurations == null || yearConfigurations.Count == 0)
    {
        return NotFound($"year_not_found {year}.");
    }

    return Ok(yearConfigurations);
}






 [HttpGet("GetAgeSegments-With-Nots/{year}")]
public async Task<IActionResult> GetAgeSegments(int year)
{
    // جلب الشرائح العمرية بناءً على السنة
    var ageSegments = await _dataContext.AgeSegments
                                        .Where(asg => asg.Year == year)
                                        .ToListAsync();

    if (ageSegments == null || !ageSegments.Any())
    {
        return NotFound(new { message = $"No age segments found for year {year}." });
    }

    var ageSegmentIds = ageSegments.Select(asg => asg.Id).ToList();

    // جلب الملاحظات المرتبطة بالشرائح العمرية
    var notes = await _dataContext.Notes
                                  .Where(n => ageSegmentIds.Contains(n.AgeSegmentId.Value))
                                  .ToListAsync();

    var result = ageSegments.Select(asg => new
    {
        AgeSegmentId = asg.Id,
        Year = asg.Year,
        FromYear=asg.FromYear,
        ToYear=asg.ToYear,
        TheAmount=asg.TheAmount,
        EnduranceRatio=asg.EnduranceRatio,
        Notes = notes
            .Where(note => note.AgeSegmentId == asg.Id)
            .Select(note => new
            {
                note.Id,
                note.Content
            })
    });

    return Ok(result);
}





  [HttpGet("RelationType-With-Notes/{year}")]
    public async Task<IActionResult> GetRelationsAndNotesByYear(int year)
    {
        // الخطوة الأولى: جلب العلاقات بناءً على السنة
        var relations = await _dataContext.RelationTypes
            .Where(r => r.Year == year)
            .ToListAsync();

        if (relations == null || !relations.Any())
        {
            return NotFound(new { message = "لم يتم العثور على علاقات لهذه السنة." });
        }

        var relationIds = relations.Select(r => r.Id).ToList();


        var notes = await _dataContext.Notes
            .Where(n => relationIds.Contains(n.RelationId.Value)) // جلب الملاحظات التي لها RelationId مطابق
             .ToListAsync();


 
        var result = relations.Select(relation => new
        {
            RelationId = relation.Id,
            RelationName = relation.Name,
            Year = relation.Year,
            Notes = notes
                .Where(note => note.RelationId == relation.Id) // ربط الملاحظات بالعلاقة بناءً على RelationId
                .Select(note => new
                {
                    note.Id,
                    note.Content
                })
        });

        return Ok(result);
    }






 [HttpDelete("DeleteRelationType/{id}")]
    public async Task<IActionResult> DeleteRelationType(int id)
    {
        var result = await _relationRepository.DeleteRelationTypeAsync(id);

        if (!result)
        {
            return NotFound(); // لم يتم العثور على العنصر
        }

        return NoContent(); // تم الحذف بنجاح
    }





     [HttpDelete("DeleteAgeSegment/{id}")]
  
    public async Task<IActionResult> DeleteAgeSegment(int id)
    {
        var result = await _ageSegmentsRepository.DeleteAgeSegmentAsync(id);

        if (!result)
        {
            return NotFound(); // لم يتم العثور على العنصر
        }

        return NoContent(); // تم الحذف بنجاح
    }





      [HttpDelete("DeleteYearConfigcuration{id}")]
    public async Task<IActionResult> DeleteYearConfiguration(int id)
    {
        var yearConfig = await _dataContext.YearConfigurations.FindAsync(id);

        if (yearConfig == null)
        {
            return NotFound(); // لم يتم العثور على العنصر
        }

        _dataContext.YearConfigurations.Remove(yearConfig);
        await _dataContext.SaveChangesAsync();

        return NoContent(); // تم الحذف بنجاح
    }





    [HttpPut]
    [Route("UpdateAgeSegmentsList")]
 
public async Task<IActionResult> UpdateAgeSegments([FromBody] List<AgeSegmentWithNotesDTO> ageSegmentsWithNotes)
{
    if (ageSegmentsWithNotes == null || !ageSegmentsWithNotes.Any())
    {
        return BadRequest("Invalid or empty age segment data.");
    }

    try
    {
        await _ageSegmentsRepository.Update_Age_Segments_With_Notes(ageSegmentsWithNotes);
        return Ok("Age segments and their notes updated successfully.");
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}







[HttpPut]
[Route("UpdateRelationTypesList")]
public async Task<IActionResult> UpdateRelationTypes([FromBody] List<RelationTypeWithNotesDTO> relationTypesWithNotes)
{
    if (relationTypesWithNotes == null || !relationTypesWithNotes.Any())
    {
        return BadRequest("Invalid or empty relation type data.");
    }

    try
    {
        await _relationRepository.Update_Relation_Types_With_Notes(relationTypesWithNotes);
        return Ok("Relation types and their notes updated successfully.");
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Internal server error: {ex.Message}");
    }
}


}
}

   
