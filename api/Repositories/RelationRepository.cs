using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

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
        }  
        
         public async Task<bool> Add_RelationType(List<RelationType> ealationType)
        {
            
            await _dataContext.RelationTypes.AddRangeAsync(ealationType);
           return  await _dataContext.SaveChangesAsync()>0;
        }
          


          
        public async Task Update_RelationType(RelationType relationType)
{
    if (relationType == null)
        throw new ArgumentException("Relation type cannot be null.");

    var existingRelationType = await _dataContext.RelationTypes
        .FirstOrDefaultAsync(r => r.Id == relationType.Id);

    if (existingRelationType != null)
    {
        // Update only the Name and Year
        existingRelationType.Name = relationType.Name;
        existingRelationType.Year = relationType.Year;

        _dataContext.RelationTypes.Update(existingRelationType);
    }
    else
    {
        throw new ArgumentException("Relation type not found.");
    }

    await _dataContext.SaveChangesAsync();
}


       

      public async Task Delete_RelationTypesByYear(int year)
            {
                var relationTypes = await _dataContext.RelationTypes
                    .Where(rt => rt.Year == year)
                    .ToListAsync();

                _dataContext.RelationTypes.RemoveRange(relationTypes);
                 _dataContext.SaveChanges();
            }


       


        }
}