using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IPersonRepository
    {
        Task <IEnumerable<Person>> GetAll();

        Task <Person?> Get(int Id);

        Task <int> Add (Person person);

        

    }
}