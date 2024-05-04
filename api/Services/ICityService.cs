using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    
    public interface ICityService
    {
         Task <Response> Add (CityEditDTO cityEditDTO);

          Task <IEnumerable<City>> GetAll();
         
    }
}