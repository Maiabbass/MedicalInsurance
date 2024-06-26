using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PersonRepository : IPersonRepository
    {

        private readonly DataContext _dataContext;


        public PersonRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<int> Add(Person person)
    {
        try
        {
            Person newPerson = new Person()
            {
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
                Subscrib = person.Subscrib,
                Affiliate = person.Affiliate,
                Beneficiary = person.Beneficiary,
                GenderId = person.GenderId
            };

            _dataContext.Persons.Add(newPerson);
            await _dataContext.SaveChangesAsync();

            return newPerson.Id;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
        {
            throw new Exception("Duplicate entry detected for unique index or constraint.", sqlEx);
        }
    }

     
        public async Task<Person?> Get(int Id)
        {
            return await _dataContext.Persons.Where(x=>x.Id==Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetAll()
        {
            return await _dataContext.Persons.ToListAsync();
        }
        public void   Delete(int Id)
        {
            
            var rest = _dataContext.Persons.FirstOrDefault(x=>x.Id==Id);
            if(rest!=null)
            {
                _dataContext.Persons.Remove(rest);
                _dataContext.SaveChanges();
            }
        }










        public Task<int> Add(City city)
        {
            throw new NotImplementedException();
        }


    
 public bool Update(int Id, PersonEditDTO PersonEditDTO)
        {
       var databaseEntity= _dataContext.Persons.FirstOrDefault(x=>x.Id==Id);
       if(databaseEntity==null){
        
         return false;

       }
       databaseEntity.FirstName=PersonEditDTO.FirstName;
       databaseEntity.FatherName=PersonEditDTO.FatherName;
       databaseEntity.LastName=PersonEditDTO.LastName;
       databaseEntity.MotherName=PersonEditDTO.MotherName;
       databaseEntity.BirthDate=PersonEditDTO.BirthDate;
       databaseEntity.NationalId=PersonEditDTO.NationalId;
       databaseEntity.EnsuranceNumber=PersonEditDTO.EnsuranceNumber;
       databaseEntity.Address=PersonEditDTO.Address;
       databaseEntity.Phone=PersonEditDTO.Phone;
       databaseEntity.Subscrib=PersonEditDTO.Subscrib;
       databaseEntity.Affiliate=PersonEditDTO.Affiliate;
       databaseEntity.Beneficiary=PersonEditDTO.Beneficiary;
       databaseEntity.GenderId=PersonEditDTO.GenderId;

       return _dataContext.SaveChanges()>0;
      
        }
       public async Task<AnnualData?> GetEngId(int EngineereId) {
        return await _dataContext.AnnualDatas.Where(x=>x.Id==EngineereId).FirstOrDefaultAsync();

       }
      public async Task<bool> IsEnsuranceNumberInClaimsAsync(string ensuranceNumber)
{
    return await _dataContext.Claims.AnyAsync(c => c.EnsuranceNumber == ensuranceNumber);
}


}}