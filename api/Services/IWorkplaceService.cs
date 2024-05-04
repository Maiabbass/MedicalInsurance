using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface IWorkplaceService
    {
         
         Task <Response> Add (WorkplaceEditDTO workplaceEditDTO);

          Task <IEnumerable<WorkPlace>> GetAll();
         
    
    }
}