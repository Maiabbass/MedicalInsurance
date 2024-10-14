using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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


       


       public async Task<bool> DeleteRelationTypeAsync(int id)
    {
        try
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                 var notes = _dataContext.Notes.Where(n => n.RelationId == id);
                _dataContext.Notes.RemoveRange(notes);

                var relations = _dataContext.Relations.Where(r => r.RelationTypeId == id);
                _dataContext.Relations.RemoveRange(relations);

                var relationType = await _dataContext.RelationTypes.FindAsync(id);
                if (relationType == null)
                {
                    return false; // لم يتم العثور على العنصر
                }

                _dataContext.RelationTypes.Remove(relationType);
                await _dataContext.SaveChangesAsync(); 

                scope.Complete();
                return true; // تم الحذف بنجاح
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}


        }
