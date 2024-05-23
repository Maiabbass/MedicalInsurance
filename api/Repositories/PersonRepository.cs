using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
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
             Person newperson =new Person()
             {
             FirstName = person.FirstName,
             FatherName =person.FatherName,
             MotherName =person.MotherName,
             LastName = person.LastName,
             BirthDate = person.BirthDate,
             NationalId = person.NationalId,
             EnsuranceNumber = person.EnsuranceNumber,
             Address = person.Address,
             Phone = person.Phone,
             Subscrib=person.Subscrib,
             Affiliate=person.Affiliate,
             Beneficiary=person.Beneficiary,
             GenderId = person.GenderId
             }; 

             _dataContext.Persons.Add(newperson);
              await _dataContext.SaveChangesAsync();

              return newperson.Id;
             
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


}}