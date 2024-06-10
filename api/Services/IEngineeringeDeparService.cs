using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;

namespace api.Services
{
    public interface IEngineeringeDeparService
    {
        
    Task <Response> Add (EngineeringeDeparEditDTO EngineeringeDeparEditDTO);
        
    }
}