using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface ICityRepository
    {
         Task <IEnumerable<City>> GetAll();

         Task <City?> Get(int Id);

         Task <int> Add (City person);

          bool Update ( int Id, CityEditDTO city );

          void DeleteByCityId(int CityId);
          void Delete (int Id);
    }
}
 