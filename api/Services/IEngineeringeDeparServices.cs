using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface IEngineeringeDeparServices
    {
          Task <Response> Add (EngineeringeDeparEditDTO EngEditDTO);

          Task <IEnumerable<EngineeringeDepar>> GetAll();
          Task<EngineeringeDepar?> Get (int id);
          public bool Update(int Id, EngineeringeDepar EngDepa);
        bool Update(int id, EngineeringeDeparEditDTO engineeringeDeparEditDTO);

        public bool Delete(int Id);
    }
}