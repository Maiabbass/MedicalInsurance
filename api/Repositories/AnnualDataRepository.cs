using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.EntityFrameworkCore;
using static api.DTOS.RegisterAnnualDataDTO;

namespace api.Repositories
{
    public class AnnualDataRepository : IAnnualDataRepository
    {

        private readonly DataContext _dataContext;

        public AnnualDataRepository(DataContext dataContext)
        {
            _dataContext=dataContext;
        }
        public async Task<int> Add_AnnualData(AnnualData annualData)
        {
             AnnualData newitem=new AnnualData ()
             {
                EngineereId = annualData.EngineereId,
                Year = annualData.Year,
                Amount = annualData.Amount,
                ExAmount = annualData.ExAmount,
                TotalAmount = annualData.TotalAmount,
                PayMethodId = annualData.PayMethodId,
                WorkPlaceId = annualData.WorkPlaceId,
                EngineeringUnitsId = annualData.EngineeringUnitsId,
                HisDic=annualData.HisDic,

             };
             _dataContext.AnnualDatas.Add(newitem);
            await _dataContext.SaveChangesAsync();
            return newitem.Id;
        }

        public async Task<int> Add_AnnualDataDetail(AnnualDataDetail annualDataDetail)
        {
            AnnualDataDetail newitem =new AnnualDataDetail ()
            {
                    PersonId  = annualDataDetail.PersonId,
                    AnnualDataId = annualDataDetail.AnnualDataId,
                    IsEngineer = annualDataDetail.IsEngineer,
                    Amount = annualDataDetail.Amount
            } ;

             _dataContext.AnnualDataDetails.Add(newitem);
              await _dataContext.SaveChangesAsync();

              return newitem.Id;
        }

   


         //get data from tow tabel with id


        public async Task<AnnualDataWithDetails> Get(int wid)
        {

           var data = await _dataContext.AnnualDatas.Where(x=>x.Id==wid)
            .Include(a => a.AnnualDataDetails)
           .FirstOrDefaultAsync() ; 
            
             AnnualData _annualData=new AnnualData ()
             {
             Id =data.Id,
             Year= data.Year,
             Amount=data.Amount,
             ExAmount=data.ExAmount,
             EngineereId=data.EngineereId,
             PayMethodId=data.PayMethodId,
             WorkPlaceId=data.WorkPlaceId,
             EngineeringUnitsId=data.EngineeringUnitsId,
             TotalAmount=data.TotalAmount,
             };
               List<AnnualDataDetail> annualDataDetails =new  List<AnnualDataDetail>();

               foreach(var item in data.AnnualDataDetails)
               {
                AnnualDataDetail annualDataDetailnew=new  AnnualDataDetail()
                {
                    Id =item.Id,
                    PersonId=item.PersonId,
                    AnnualDataId=item.AnnualDataId,
                    IsEngineer=item.IsEngineer,
                    Amount=item.Amount,
                };
                annualDataDetails.Add(annualDataDetailnew);
               }  
             
           return  new AnnualDataWithDetails
           {
               AnnualData = _annualData,
               AnnualDataDetails =annualDataDetails
           };
        } 





 
// get all data from tow tabel

    public async Task<IEnumerable<AnnualDataWithDetails>> GetAll() {
            
            List<AnnualDataWithDetails> result =new  List<AnnualDataWithDetails>();
           var data = await _dataContext.AnnualDatas
            .Include(a => a.AnnualDataDetails)
           .ToListAsync() ; 
            

            foreach (var item in data)
            {
            AnnualData _annualData=new AnnualData ()
             {
             Id =item.Id,
             Year= item.Year,
             Amount=item.Amount,
             ExAmount=item.ExAmount,
             EngineereId=item.EngineereId,
             PayMethodId=item.PayMethodId,
             WorkPlaceId=item.WorkPlaceId,
             EngineeringUnitsId=item.EngineeringUnitsId,
             TotalAmount=item.TotalAmount,
             };
               List<AnnualDataDetail> annualDataDetails =new  List<AnnualDataDetail>();

               foreach(var detailitem in item.AnnualDataDetails)
               {
                AnnualDataDetail annualDataDetailnew=new  AnnualDataDetail()
                {
                    Id =detailitem.Id,
                    PersonId=detailitem.PersonId,
                    AnnualDataId=detailitem.AnnualDataId,
                    IsEngineer=detailitem.IsEngineer,
                    Amount=detailitem.Amount,
                };
                annualDataDetails.Add(annualDataDetailnew);
               }  

               AnnualDataWithDetails AnnualDataWithDetailsNew=new AnnualDataWithDetails ()
               {
                AnnualData=_annualData,
                AnnualDataDetails=annualDataDetails
               };
               result.Add(AnnualDataWithDetailsNew);
            }
             
           return  result;} 


       

        public void DeleteByPersonId(int PersonId){
         var rest=   _dataContext.AnnualDataDetails.Where(x=>x.PersonId==PersonId).ToList();
         if(rest!=null){
            _dataContext.AnnualDataDetails.RemoveRange(rest);

         }
         
          
        }
        
        public void DeleteByAnnualDataId(int AnnualDataId){
         var rest=   _dataContext.AnnualDataDetails.Where(x=>x.AnnualDataId==AnnualDataId).ToList();
         if(rest!=null){
            _dataContext.AnnualDataDetails.RemoveRange(rest);
            _dataContext.SaveChanges();
         }}

         

        public void Delete(int Id){
            var result = _dataContext.AnnualDatas.Where(x=>x.Id==Id).ToList();
            if (result!=null){
                 _dataContext.AnnualDatas.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }

         public bool Update(int Id, AnnualDataForView annualDataForView )
        {
       var databaseEntity= _dataContext.AnnualDatas.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Amount=annualDataForView.Amount;
       databaseEntity.ExAmount=annualDataForView.ExAmount;
       databaseEntity.Year=annualDataForView.Year;
       databaseEntity.EngineereId=annualDataForView.EngineereId;
       databaseEntity.PayMethodId=annualDataForView.PayMethodId;
       databaseEntity.WorkPlaceId=annualDataForView.WorkPlaceId;
       databaseEntity.EngineeringUnitsId=annualDataForView.EngineeringUnitsId;
       databaseEntity.TotalAmount=annualDataForView.TotalAmount;
       
       
       return _dataContext.SaveChanges()>0;
      
        }
        


 public bool Update(int Id, AnnualDataDetailForView annualDataDetailForView )
        {
       var databaseEntity= _dataContext.AnnualDataDetails.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Amount=annualDataDetailForView.Amount;
       databaseEntity.PersonId=annualDataDetailForView.PersonId;
       databaseEntity.AnnualDataId=annualDataDetailForView.AnnualDataId;
       databaseEntity.IsEngineer=annualDataDetailForView.IsEngineer;


       return _dataContext.SaveChanges()>0;
      
        }

        public async Task<int> Add_Year_Configuration(YearConfiguration yearConfiguration)
        {
            int insertedId=0;
             _dataContext.YearConfigurations.Add(yearConfiguration);
            await _dataContext.SaveChangesAsync();
            insertedId = yearConfiguration.Id;
            return insertedId;

        }

        public  void Delete_Year_Configuration(int year)
        {
             var yearConfigurationItem=  _dataContext.YearConfigurations.FirstOrDefault(x=>x.Year==year);
             if (yearConfigurationItem!=null)
             {
                _dataContext.YearConfigurations.Remove(yearConfigurationItem);
             }

        }

        
    }

    }