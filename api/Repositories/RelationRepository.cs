using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;

namespace api.Repositories
{
    public class RelationRepository : IRelationRepository
    {
         private readonly DataContext _dataContext;

         public RelationRepository(DataContext dataContext)
         {
            _dataContext=dataContext;
         }
        public async Task<int> Add(Relation relation)
        {
           Relation newRelation = new Relation()
           {
                Name = relation.Name,
                PersonId = relation.PersonId,
                EngineereId = relation.EngineereId,
                RelationTypeId = relation.RelationTypeId
           };
           _dataContext.Relations.Add(newRelation);
           await _dataContext.SaveChangesAsync();

           return  newRelation.Id;
        }

        public void DeleteByPersonId(int PersonId)  {
         var ras= _dataContext.Relations.Where(x=>x.PersonId==PersonId).ToList();
         if(ras!=null){
            _dataContext.Relations.RemoveRange(ras) ; 
         }
        }  }
}