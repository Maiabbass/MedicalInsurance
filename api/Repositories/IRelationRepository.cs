using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface IRelationRepository
    {
        
        Task<int> Add (Relation relation);
        public void DeleteByPersonId(int PersonId) ;
    }
}