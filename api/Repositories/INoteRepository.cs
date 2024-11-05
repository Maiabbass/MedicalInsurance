using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;

namespace api.Repositories
{
    public interface INoteRepository
    {

        Task AddNoteAsync(Note note);

        Task<IEnumerable<Note>> GetNotesByPersonIdAsync(int personId);

         Task AddNotesAsyncList(List<Note> notes);

        Task Delete_NotesByYearConfigId(int yearConfigId);

        Task DeleteNotesByHospitalId(int hospitalId);
        Task DeleteNotesBySurgicalProcedureId(int surgicalProcedureId);

        Task DeleteNotesByPersonIdAsync(int personId) ;
        Task Delete_NotesByYearAsync(int year);
        Task<YearConfiguration> GetYearConfigurationByIdAsync(int yearConfigId);
        Task SaveChangesAsync();
        Task<bool> DeleteNoteAsync(int? personId = null, int? ageSegmentId = null, int? relationId = null, int? hospitalId = null, int? surgicalProcedureId = null);
        Task<bool> DeleteNoteByIdAsync(int id);
        Task<bool> EditNoteAsync(int id, string newContent);
    }
}