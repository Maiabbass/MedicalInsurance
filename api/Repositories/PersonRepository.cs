using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
    }
}