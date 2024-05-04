using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IWorkplaceRepository
    {
        
         Task <IEnumerable<WorkPlace>> GetAll();

         Task <WorkPlace?> Get(int Id);

         Task <int> Add (WorkPlace person);
    }
    }
