using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Repositories
{
    public interface IRelationRepository
    {
        
        Task<int> Add (Relation relation);
        
        Task DeleteByPersonIdAsync(int personId) ;

        Task<bool> Add_RelationType(List<RelationType> ealationType);
        Task Update_RelationType(RelationType relationType);
        Task Delete_RelationTypesByYear(int year);
        Task<bool> DeleteRelationTypeAsync(int id);
        Task Update_Relation_Types_With_Notes(List<RelationTypeWithNotesDTO> relationTypesWithNotes);

        Task<IEnumerable<Person>> GetFamilyMembersByEngineerId(int engineerId);
       
       
    }
}