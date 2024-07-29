using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using api.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class EngineerRepository : IEngineerRepository
    {
         private readonly DataContext _dataContext;

         private readonly IAnnualDataService _annualDataService;
         public EngineerRepository(DataContext dataContext, IAnnualDataService annualDataService)
         {
            _dataContext =dataContext;
            _annualDataService =annualDataService;
         }
        public async Task<int> Add(Engineere engineere)
        {
          try{
            Engineere newEngineer =new Engineere()
             {
               EngNumber=engineere.EngNumber,
               SubNumber = engineere.SubNumber,
               Id =engineere.Id,
               
               SpecializationId=engineere.SpecializationId,
               WorkPlaceId= engineere.WorkPlaceId
             }; 


              _dataContext.Engineeres.Add(newEngineer);
              await _dataContext.SaveChangesAsync();

              return newEngineer.Id;
          }
          catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            throw new Exception("Duplicate entry detected for unique index or constraint.", sqlEx);
        }
        }

        

        public async Task<Engineere?> Get(int Id)
        {
              return await _dataContext.Engineeres.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Engineere>> GetAll()
        {
             return await _dataContext.Engineeres.ToListAsync();
        }


          public bool Update(int Id, EngineerPersonEditDTO engineerPersonEditDTO)
{
    // حساب المبلغ الجديد بناءً على تاريخ الميلاد الجديد
    var amount = _annualDataService.calcualteAmount(engineerPersonEditDTO.BirthDate, 2024);

    // البحث عن المهندس في قاعدة البيانات
    var engineerEntity = _dataContext.Engineeres.FirstOrDefault(x => x.Id == Id);
    if (engineerEntity == null)
    {
        return false;
    }

    // تحديث الحقول الخاصة بالمهندس
    engineerEntity.EngNumber = engineerPersonEditDTO.EngNumber;
    engineerEntity.SubNumber = engineerPersonEditDTO.SubNumber;
    engineerEntity.SpecializationId = engineerPersonEditDTO.SpecializationId;
    engineerEntity.WorkPlaceId = engineerPersonEditDTO.WorkPlaceId;

    // البحث عن الشخص في قاعدة البيانات باستخدام نفس المعرف
    var personEntity = _dataContext.Persons.FirstOrDefault(x => x.Id == Id);
    if (personEntity == null)
    {
        return false;
    }

    // تحديث الحقول الخاصة بالشخص
    personEntity.FirstName = engineerPersonEditDTO.FirstName;
    personEntity.FatherName = engineerPersonEditDTO.FatherName;
    personEntity.LastName = engineerPersonEditDTO.LastName;
    personEntity.MotherName = engineerPersonEditDTO.MotherName;
    personEntity.BirthDate = engineerPersonEditDTO.BirthDate;
    personEntity.NationalId = engineerPersonEditDTO.NationalId;
    personEntity.EnsuranceNumber = engineerPersonEditDTO.EnsuranceNumber;
    personEntity.Address = engineerPersonEditDTO.Address;
    personEntity.Phone = engineerPersonEditDTO.Phone;
    personEntity.Subscrib = engineerPersonEditDTO.Subscrib;
    personEntity.Affiliate = engineerPersonEditDTO.Affiliate;
    personEntity.Beneficiary = engineerPersonEditDTO.Beneficiary;
    personEntity.GenderId = engineerPersonEditDTO.GenderId;
    personEntity.StatusId = engineerPersonEditDTO.statusId;
    personEntity.Amount = amount; // تحديث المبلغ الجديد

    // حفظ التغييرات في قاعدة البيانات
    return _dataContext.SaveChanges() > 0;
}


              public void DeleteByEngId(int EngineereId){
         var rest=   _dataContext.Relations.Where(x=>x.EngineereId==EngineereId).ToList();
         if(rest!=null){
            _dataContext.Relations.RemoveRange(rest);
            _dataContext.SaveChanges();
         }}
           


           public void Delete(int Id){
            var result = _dataContext.Engineeres.Where(x=>x.Id==Id).ToList();
            if (result!=null){
                 _dataContext.Engineeres.RemoveRange(result);
                 _dataContext.SaveChanges();
            }
        }

    }
}