using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using api.Data;
using api.DTOS;
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

        

       public async Task DeleteByPersonIdAsync(int personId) {
    var ras = await _dataContext.Relations
        .Where(x => x.PersonId == personId)
        .ToListAsync();

    if (ras.Any()) {
        _dataContext.Relations.RemoveRange(ras);
        await _dataContext.SaveChangesAsync();
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






 public async Task Update_Relation_Types_With_Notes(List<RelationTypeWithNotesDTO> relationTypesWithNotes)
{
    // جمع السنوات المدخلة حديثا
    var yearsToUpdate = relationTypesWithNotes.Select(r => r.Year).Distinct().ToList();

    // جلب العلاقات الحالية والملاحظات المرتبطة بها حسب السنوات المدخلة حديثا فقط
    var existingRelationTypes = await _dataContext.RelationTypes
        .Where(r => yearsToUpdate.Contains(r.Year))
        .Include(r => r.Notes)
        .ToListAsync();

    if (existingRelationTypes.Any())
    {
        // حذف الملاحظات المرتبطة أولا
        foreach (var relationType in existingRelationTypes)
        {
            if (relationType.Notes != null && relationType.Notes.Any())
            {
                _dataContext.Notes.RemoveRange(relationType.Notes);
            }
        }

        // حذف العلاقات بعد حذف الملاحظات
        _dataContext.RelationTypes.RemoveRange(existingRelationTypes);
    }

    // إضافة العلاقات والملاحظات الجديدة
    foreach (var relationTypeWithNotes in relationTypesWithNotes)
    {
        var newRelationType = new RelationType
        {
            Name = relationTypeWithNotes.Name,
            Year = relationTypeWithNotes.Year,
            Notes = relationTypeWithNotes.Notes.Select(n => new Note
            {
                Content = n.Content, // الملاحظات التي سيتم إدخالها
            }).ToList()
        };

        await _dataContext.RelationTypes.AddAsync(newRelationType);
    }

    await _dataContext.SaveChangesAsync();
}





public async Task<IEnumerable<Person>> GetFamilyMembersByEngineerId(int engineerId)
{
    return await _dataContext.Relations
        .Where(r => r.EngineereId == engineerId)
        .Select(r => r.Person)
        .ToListAsync();
}


}


        }
