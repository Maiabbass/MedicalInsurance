using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface IEngineeringeDeparRepositry
    {
          Task <IEnumerable<EngineeringeDepar>> GetAll();

         Task <EngineeringeDepar?> Get(int Id);

         Task <int> Add (EngineeringeDepar EngDepaE);

          bool Update ( int Id, EngineeringeDeparEditDTO EngDepar );
          public void Delete(int Id);
    }
}