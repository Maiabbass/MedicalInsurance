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

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnualData : ControllerBase
    {
        private readonly IAnnualDataService _AnnualDataService;

        

        public AnnualData(IAnnualDataService AnnualDataService)
        {
            _AnnualDataService =AnnualDataService;
        }

        [HttpPost]
        public async Task<ActionResult< Response>> RegisterAnnualData([FromBody] RegisterAnnualDataDTO registerAnnualDataDTO)
        {
                var response =await _AnnualDataService.Add(registerAnnualDataDTO);
                  if (response.ErrorMessage!=null)
               {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response {  ErrorMessage =response.ErrorMessage});
               }
                return Ok(response);
        }
        





         [HttpGet("{Id}")]
                 public async Task<ActionResult<AnnualDataWithDetails?>>Get([FromRoute] int Id){
         return await _AnnualDataService.Get(Id);
}

 






[HttpGet]
public async Task<ActionResult<IEnumerable<AnnualDataForView>>> GetAll()
{
    var data = await _AnnualDataService.GetAll();
    List<AnnualDataWithDetails> dataList = data.ToList();
    List<AnnualDataForView> annualDataForViews = new List<AnnualDataForView>();
    
    foreach (var item in dataList)
    {
        int id = item.AnnualData.Id;
        int year = item.AnnualData.Year;
        int engineereId = item.AnnualData.EngineereId;
        decimal exAmount = item.AnnualData.ExAmount;
        decimal amount = item.AnnualData.Amount;
        decimal totalAmount = item.AnnualData.TotalAmount;
        List<AnnualDataDetailForView> annualDataDetailsForView = new List<AnnualDataDetailForView>();
        
        foreach (var detail in item.AnnualDataDetails)
        {
            int detailId = detail.Id;
            int personId = detail.PersonId;
            int annualDataId=detail.AnnualDataId;
            bool isEngineer=detail.IsEngineer;
            decimal amount1=detail.Amount;
            AnnualDataDetailForView annualDataDetailForView = new AnnualDataDetailForView
            {
                Id = detailId,
                PersonId = personId,
                AnnualDataId=annualDataId,
                IsEngineer=isEngineer,
                Amount=amount1,
            };
            annualDataDetailsForView.Add(annualDataDetailForView);
        }
        
        AnnualDataForView annualDataForView = new AnnualDataForView
        {
            Id = id,
            Year=year,
            EngineereId=engineereId,
            ExAmount = exAmount,
            Amount = amount,
            TotalAmount = totalAmount,
            AnnualDataDetails = annualDataDetailsForView
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
            public ActionResult<decimal> CalculateAmount(DateTime? birthdate,int year)
            {
                 
                 decimal amount=0m;
                 try 
                 {
                 amount= _AnnualDataService.calcualteAmount(birthdate,year);
                 return  Ok(amount);
                 }
                 catch(Exception ex)
                 {
                   return StatusCode(StatusCodes.Status500InternalServerError,new Response {ErrorMessage=ex.Message});
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
  
  }
   }
