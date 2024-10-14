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
            Beneficiary = annualData.Beneficiary,
           Waiting=annualData.Waiting
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
                    Amount = annualDataDetail.Amount,
                    Beneficiary=annualDataDetail.Beneficiary,
                    Affiliate=annualDataDetail.Affiliate,
                    Subscrib=annualDataDetail.Subscrib,
                    CardStatuse=annualDataDetail.CardStatuse,
                    Year=annualDataDetail.Year,
                    ExAmount=annualDataDetail.ExAmount,
                    Waiting=annualDataDetail.Waiting,
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
             Waiting=data.Waiting,
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
                    Year=item.Year,
                    CardStatuse=item.CardStatuse,
                    ExAmount=item.ExAmount,
                    Affiliate=item.Affiliate,
                    Subscrib=item.Subscrib,
                    Beneficiary=item.Beneficiary,
                    Waiting=item.Waiting,
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
                   a.Affiliate, a.Subscrib, a.Beneficiary, a.Waiting,
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
                Beneficiary = item.Beneficiary,
                Waiting=item.Waiting,
            },
            AnnualDataDetails = annualDataDetails.Select(d => new AnnualDataDetail
            {
                Id = d.Id,
                PersonId = d.PersonId,
                AnnualDataId = d.AnnualDataId,
                IsEngineer = d.IsEngineer,
                Amount = d.Amount,
                Year=d.Year,
                CardStatuse=d.CardStatuse,
                ExAmount=d.ExAmount,
                Affiliate=d.Affiliate,
                Subscrib=d.Subscrib,
                Beneficiary=d.Beneficiary,
                Waiting=d.Waiting,

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

         public bool Update(int Id,AnnalDataForEdit  annualDataForEdit )
        {
       var databaseEntity= _dataContext.AnnualDatas.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.Amount=annualDataForEdit.Amount;
       databaseEntity.ExAmount=annualDataForEdit.ExAmount;
       databaseEntity.Year=annualDataForEdit.Year;
       databaseEntity.EngineereId=annualDataForEdit.EngineereId;
       databaseEntity.PayMethodId=annualDataForEdit.PayMethodId;
       databaseEntity.WorkPlaceId=annualDataForEdit.WorkPlaceId;
       databaseEntity.EngineeringUnitsId=annualDataForEdit.EngineeringUnitsId;
       databaseEntity.TotalAmount=annualDataForEdit.TotalAmount;
       databaseEntity.HisDic=annualDataForEdit.HisDic;
       databaseEntity.CardStatuse=annualDataForEdit.CardStatuse;
       databaseEntity.Waiting=annualDataForEdit.Waiting;
       
       
       
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
       databaseEntity.Year=annualDataDetailForView.Year;
       databaseEntity.ExAmount=annualDataDetailForView.ExAmount;
       databaseEntity.CardStatuse=annualDataDetailForView.CardStatuse;
       databaseEntity.Beneficiary=annualDataDetailForView.Beneficiary;
       databaseEntity.Affiliate=annualDataDetailForView.Affiliate;
       databaseEntity.Subscrib=annualDataDetailForView.Subscrib;
       databaseEntity.Waiting=annualDataDetailForView.Waiting;



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

        public async Task Delete_Year_Configuration(int year)
    {
        var yearConfiguration = await _dataContext.YearConfigurations
            .FirstOrDefaultAsync(y => y.Year == year);

        if (yearConfiguration != null)
        {
            _dataContext.YearConfigurations.Remove(yearConfiguration);
            await _dataContext.SaveChangesAsync();
        }
    }

        public async Task Update_Year_Configuration(YearConfiguration yearConfiguration)
{
    var existingConfig = await _dataContext.YearConfigurations
        .FirstOrDefaultAsync(y => y.Id == yearConfiguration.Id);

    if (existingConfig != null)
    {
        existingConfig.Year = yearConfiguration.Year;
        
        existingConfig.CardPrice = yearConfiguration.CardPrice;
       

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
                a.Waiting ,
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
            Beneficiary,
            Waiting,
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
            Beneficiary = item.Beneficiary,
            Waiting=item.Waiting,
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
            Amount = (decimal)engineer.Person.Amount,
            
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





      //جدول الاستفادة///////////

    public async Task<AnnualDataDetail?> GetAnnualDataDetailAsync(string ensuranceNumber, int year)
    {
        var person = await _dataContext.Persons.FirstOrDefaultAsync(p => p.EnsuranceNumber == ensuranceNumber);
        if (person == null) return null;

        return await _dataContext.AnnualDataDetails
            .FirstOrDefaultAsync(a => a.PersonId == person.Id && a.Year == year);
    }



    public async Task<decimal> GetClaimsSumForPersonAsync(string ensuranceNumber)
    {
        var person = await _dataContext.Persons.FirstOrDefaultAsync(p => p.EnsuranceNumber == ensuranceNumber);
        if (person == null) return 0;

        return await _dataContext.Claims
            .Where(c => c.PersonId == person.Id)
            .SumAsync(c => c.non_AddForPerson);
    }





         //التجديد /////////////////
        public async Task<AnnualData> GetByEngineerIdAndYear(int engineerId, int year)
        {
            return await _dataContext.AnnualDatas
                .FirstOrDefaultAsync(a => a.EngineereId == engineerId && a.Year == year);
        }



        public async Task<List<AnnualDataDetail>> GetAnnualDataDetails(int annualDataId)
        {
            return await _dataContext.AnnualDataDetails
                .Where(d => d.AnnualDataId == annualDataId)
                .ToListAsync();
        }




        public async Task<Person> GetByInsuranceNumber(string engineerEnsuranceNumber)
{
    // استعلام لجلب بيانات الشخص بناءً على الرقم التأميني
    return await _dataContext.Persons
        .FirstOrDefaultAsync(p => p.EnsuranceNumber == engineerEnsuranceNumber);
}


     


 


        public async Task<PayMethod> GetPayMethodByEngineerId(int engineerId)
    {
        var annualData = await _dataContext.AnnualDatas
            .Where(ad => ad.EngineereId == engineerId)
            .Select(ad => ad.PayMethod)
            .FirstOrDefaultAsync();

        return annualData;
    }



     public async Task<AnnualDataDetail> GetAnnualDataDetailByPersonIdAndYear(int personId, int year)
    {
        return await _dataContext.AnnualDataDetails
            .FirstOrDefaultAsync(detail => detail.PersonId == personId && detail.Year == year);
    }


    public async Task<List<int>> GetPersonsWithClaimsByYearAsync(int year)
{
    return await _dataContext.Claims
        .Where(c => c.Year == year)
        .Select(c => c.PersonId)
        .Distinct()
        .ToListAsync();
}


public async Task UpdateAnnualData(AnnualData annualData)
{
    _dataContext.AnnualDatas.Update(annualData);
    await _dataContext.SaveChangesAsync();
}



public async Task UpdateAnnualDataDetail(AnnualDataDetail annualDataDetail)
{
    _dataContext.AnnualDataDetails.Update(annualDataDetail);
    await _dataContext.SaveChangesAsync();
}








     public async Task<List<EngineerStatusDto>> GetEngineerStatusByYear(int engineerId)
    {
        var annualDataList = await _dataContext.AnnualDatas
            .Where(ad => ad.EngineereId == engineerId)
            .Include(ad => ad.Engineere)
            .ThenInclude(e => e.Person)
            .ToListAsync();

        if (annualDataList == null || annualDataList.Count == 0)
        {
            return new List<EngineerStatusDto>();
        }

        var result = new List<EngineerStatusDto>();

        foreach (var data in annualDataList)
        {
            if (data.Engineere == null || data.Engineere.Person == null)
            {
                continue;
            }

            string status = "";
            decimal? nonAddForPersonSum = null; // Nullable decimal for the sum

            if (data.Subscrib && data.Affiliate && !data.Beneficiary)
            {
                status = "Registered";
            }
            else if (data.Subscrib && data.Affiliate && data.Beneficiary)
            {
                status = "Beneficiary";

                // Get the sum of non_AddForPerson from Clims table
                nonAddForPersonSum = await _dataContext.Claims
                    .Where(c => c.PersonId == data.Engineere.Person.Id)
                    .SumAsync(c => (decimal?)c.non_AddForPerson) ?? 0;
            }

            result.Add(new EngineerStatusDto
            {
                Year = data.Year,
                InsuranceNumber = data.Engineere.Person.EnsuranceNumber ?? "Unknown",
                Status = status,
                NonAddForPersonSum = nonAddForPersonSum // Set the sum here
            });
        }

        return result;
    }






    public async Task<List<AnnualDataDetailStatusDto>> GetFamilyMemberStatusByYear(int personId)
    {
        // Get AnnualDataDetail for the given PersonId and where IsEngineer is false
        var annualDataDetailList = await _dataContext.AnnualDataDetails
            .Where(add => add.PersonId == personId && !add.IsEngineer)
            .Include(add => add.Person)
            .ToListAsync();

        if (annualDataDetailList == null || annualDataDetailList.Count == 0)
        {
            return new List<AnnualDataDetailStatusDto>();
        }

        var result = new List<AnnualDataDetailStatusDto>();

        foreach (var data in annualDataDetailList)
        {
            if (data.Person == null)
            {
                continue;
            }

            string status = "";
            decimal? nonAddForPersonSum = null;

            // Logic for determining status based on Subscrib, Affiliate, and Beneficiary
            if (data.Subscrib && data.Affiliate && !data.Beneficiary)
            {
                status = "Registered";
            }
            else if (data.Subscrib && data.Affiliate && data.Beneficiary)
            {
                status = "Beneficiary";

                // Get the sum of non_AddForPerson from Clims table for this person
                nonAddForPersonSum = await _dataContext.Claims
                    .Where(c => c.PersonId == data.PersonId)
                    .SumAsync(c => (decimal?)c.non_AddForPerson) ?? 0;
            }

            result.Add(new AnnualDataDetailStatusDto
            {
                Year = data.Year,
                InsuranceNumber = data.Person.EnsuranceNumber ?? "Unknown",
                Status = status,
                NonAddForPersonSum = nonAddForPersonSum // Set the sum here
            });
        }

        return result;
    }



    public async Task<YearConfiguration> GetYearConfigurationByYear(int year)
{
    return await _dataContext.YearConfigurations
        .FirstOrDefaultAsync(y => y.Year == year);
}

}
}





  
        
    

    