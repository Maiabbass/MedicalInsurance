using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.Data.SqlClient;
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
    try
    {
        AnnualData newitem = new AnnualData()
        {
            EngineereId = annualData.EngineereId,
            Year = annualData.Year,
            Amount = annualData.Amount,
            ExAmount = annualData.ExAmount,
            TotalAmount = annualData.TotalAmount,
            PayMethodId = annualData.PayMethodId,
            WorkPlaceId = annualData.WorkPlaceId,
            EngineeringUnitsId = annualData.EngineeringUnitsId,
            HisDic = annualData.HisDic,
            CardStatuse=annualData.CardStatuse,
            Subscrib = annualData.Subscrib,
            Affiliate = annualData.Affiliate,
            Beneficiary = annualData.Beneficiary
        };
        _dataContext.AnnualDatas.Add(newitem);
        await _dataContext.SaveChangesAsync();
        return newitem.Id;
    }
    catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
    {
        throw new Exception("Duplicate entry detected for unique index or constraint.", sqlEx);
    }
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
             CardStatuse=data.CardStatuse,
             HisDic=data.HisDic,
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

   public async Task<IEnumerable<AnnualDataWithDetails>> GetAll(int pageNumber, int pageSize)
{
    int skip = (pageNumber - 1) * pageSize;
    int take = pageSize;

    // استعلام SQL لاستخدام ROW_NUMBER لتطبيق Paging على AnnualData فقط
    var query = @"
        WITH PagedData AS (
             SELECT a.Id, a.Year, a.Amount, a.ExAmount, a.EngineereId, a.PayMethodId, 
                   a.WorkPlaceId, a.EngineeringUnitsId, a.TotalAmount, a.CardStatuse, a.HisDic,
                   a.Affiliate, a.Subscrib, a.Beneficiary,
                   ROW_NUMBER() OVER (ORDER BY a.Id) AS RowNumber
            FROM AnnualDatas a
        )
        SELECT * FROM PagedData
        WHERE RowNumber BETWEEN @startRow AND @endRow;
    ";

    var startRow = skip + 1;
    var endRow = skip + take;

    var rawData = await _dataContext.AnnualDatas
        .FromSqlRaw(query, 
            new SqlParameter("@startRow", startRow), 
            new SqlParameter("@endRow", endRow))
        .ToListAsync();

    // تحويل البيانات إلى النموذج المطلوب (AnnualDataWithDetails) مع تحميل التفاصيل لاحقًا
    var result = new List<AnnualDataWithDetails>();

    foreach (var item in rawData)
    {
        var annualDataDetails = await _dataContext.AnnualDataDetails
            .Where(d => d.AnnualDataId == item.Id)
            .ToListAsync();

        result.Add(new AnnualDataWithDetails
        {
            AnnualData = new AnnualData
            {
                Id = item.Id,
                Year = item.Year,
                Amount = item.Amount,
                ExAmount = item.ExAmount,
                EngineereId = item.EngineereId,
                PayMethodId = item.PayMethodId,
                WorkPlaceId = item.WorkPlaceId,
                EngineeringUnitsId = item.EngineeringUnitsId,
                TotalAmount = item.TotalAmount,
                CardStatuse=item.CardStatuse,
                HisDic = item.HisDic,
                Affiliate = item.Affiliate,
                Subscrib = item.Subscrib,
                Beneficiary = item.Beneficiary
            },
            AnnualDataDetails = annualDataDetails.Select(d => new AnnualDataDetail
            {
                Id = d.Id,
                PersonId = d.PersonId,
                AnnualDataId = d.AnnualDataId,
                IsEngineer = d.IsEngineer,
                Amount = d.Amount
            }).ToList()
        });
    }

    return result;
}
 


       

       

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
       databaseEntity.HisDic=annualDataForView.HisDic;
       databaseEntity.CardStatuse=annualDataForView.CardStatuse;
       
       
       
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

        public async Task Update_Year_Configuration(YearConfiguration yearConfiguration)
{
    var existingConfig = await _dataContext.YearConfigurations
        .FirstOrDefaultAsync(y => y.Id == yearConfiguration.Id);

    if (existingConfig != null)
    {
        existingConfig.Year = yearConfiguration.Year;
        existingConfig.InsideHospitalPercentage = yearConfiguration.InsideHospitalPercentage;
        existingConfig.OutsideHospitalPercentage = yearConfiguration.OutsideHospitalPercentage;
        existingConfig.CardPrice = yearConfiguration.CardPrice;
        existingConfig.Limit=yearConfiguration.Limit;

        _dataContext.YearConfigurations.Update(existingConfig);
        await _dataContext.SaveChangesAsync();
    }
}





 public async Task<IEnumerable<AnnualDataWithDetails>> GetByYear(int year, int pageNumber, int pageSize)
{
    int skip = (pageNumber - 1) * pageSize;
    int take = pageSize;
    int endRow = skip + take;

    var query = @"
        WITH PagedData AS (
            SELECT 
                a.Id, 
                a.Year, 
                a.Amount, 
                a.ExAmount, 
                a.EngineereId, 
                a.PayMethodId, 
                a.WorkPlaceId, 
                a.EngineeringUnitsId, 
                a.TotalAmount, 
                a.CardStatuse,
                a.HisDic,
                a.Affiliate, 
                a.Subscrib, 
                a.Beneficiary,
                ROW_NUMBER() OVER (ORDER BY a.Id) AS RowNumber
            FROM AnnualDatas a
            WHERE a.Year = @year
        )
        SELECT 
            Id, 
            Year, 
            Amount, 
            ExAmount, 
            EngineereId, 
            PayMethodId, 
            WorkPlaceId, 
            EngineeringUnitsId, 
            TotalAmount, 
            CardStatuse,
            HisDic,
            Affiliate, 
            Subscrib, 
            Beneficiary
        FROM PagedData
        WHERE RowNumber > @skip AND RowNumber <= @endRow;
    ";

    var parameters = new[]
    {
        new SqlParameter("@year", year),
        new SqlParameter("@skip", skip),
        new SqlParameter("@endRow", endRow)
    };

    var data = await _dataContext.AnnualDatas
        .FromSqlRaw(query, parameters)
        .ToListAsync();

    // تحويل البيانات إلى النموذج المطلوب (AnnualDataWithDetails)
    var result = data.Select(item => new AnnualDataWithDetails
    {
        AnnualData = new AnnualData
        {
            Id = item.Id,
            Year = item.Year,
            Amount = item.Amount,
            ExAmount = item.ExAmount,
            EngineereId = item.EngineereId,
            PayMethodId = item.PayMethodId,
            WorkPlaceId = item.WorkPlaceId,
            EngineeringUnitsId = item.EngineeringUnitsId,
            TotalAmount = item.TotalAmount,
            CardStatuse=item.CardStatuse,
            HisDic = item.HisDic,
            Affiliate = item.Affiliate,
            Subscrib = item.Subscrib,
            Beneficiary = item.Beneficiary
        },
        AnnualDataDetails = new List<AnnualDataDetail>() // قائمة فارغة لأن التفاصيل تُعبأ لاحقًا
    }).ToList();

    return result;
}


  public async Task<(SimpleEngineer engineer, bool? cardStatus, int? payMethod)> GetEngineerDetailsAndCardStatus(string insuranceNumber, int year)
{
    // Step 1: Retrieve the engineer based on the insurance number
    var engineer = await _dataContext.Engineeres
        .Include(e => e.Person)
        .Include(e => e.Relations)
            .ThenInclude(r => r.Person)
        .FirstOrDefaultAsync(e => e.Person.EnsuranceNumber == insuranceNumber);

    if (engineer == null)
    {
        // Handle the case where the engineer is not found
        return (null, null, null);
    }

    // Step 2: Retrieve the card status and pay method from the AnnualData table
    var annualData = await _dataContext.AnnualDatas
        .Where(x => x.EngineereId == engineer.Id && x.Year == year)
        .Select(x => new { x.CardStatuse, x.PayMethodId })
        .FirstOrDefaultAsync();

    // تحويل الكائن إلى كائن بسيط
    var simpleEngineer = new SimpleEngineer
    {
        Id = engineer.Id,
        EngNumber = engineer.EngNumber,
        SubNumber = engineer.SubNumber,
        Person = new SimplePerson
        {
            Id = engineer.Person.Id,
            FirstName = engineer.Person.FirstName,
            FatherName = engineer.Person.FatherName,
            LastName = engineer.Person.LastName,
            MotherName = engineer.Person.MotherName,
            NationalId = engineer.Person.NationalId,
            EnsuranceNumber = engineer.Person.EnsuranceNumber,
            Address = engineer.Person.Address,
            Phone = engineer.Person.Phone,
            Mobile = engineer.Person.Mobile,
            Email = engineer.Person.Email,
            StatusId = engineer.Person.StatusId ?? 0,
            GenderId = engineer.Person.GenderId,
            Amount = (decimal)engineer.Person.Amount
        },
        Relations = engineer.Relations.Select(r => new SimpleRelation
        {
            Id = r.Id,
            Name = r.Name,
            RelationTypeId = r.RelationTypeId, // توضيح نوع العلاقة
            Person = new SimplePerson
            {
                Id = r.Person.Id,
                FirstName = r.Person.FirstName,
                FatherName = r.Person.FatherName,
                LastName = r.Person.LastName,
                MotherName = r.Person.MotherName,
                NationalId = r.Person.NationalId,
                EnsuranceNumber = r.Person.EnsuranceNumber,
                Address = r.Person.Address,
                Phone = r.Person.Phone,
                Mobile = r.Person.Mobile,
                Email = r.Person.Email,
                StatusId = r.Person.StatusId ?? 0,
                GenderId = r.Person.GenderId,
                Amount = (decimal)r.Person.Amount
            }
        }).ToList()
    };

    // Return the engineer details, card status, and pay method
    return (simpleEngineer, annualData?.CardStatuse, annualData?.PayMethodId);
}



   
        
    }}

    