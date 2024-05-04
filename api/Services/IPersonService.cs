using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;

namespace api.Services
{
    public interface IPersonService
    {
        object Person { get; }

        Task <Response> Add (PersonEditDTO  personEditDTO);
        Task GetAll();
       
    }
}