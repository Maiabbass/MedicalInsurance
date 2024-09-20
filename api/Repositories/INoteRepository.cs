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

        Task DeleteNotesByPersonId(int personId);
        Task Delete_NotesByYearAsync(int year);
        Task<YearConfiguration> GetYearConfigurationByIdAsync(int yearConfigId);
        Task SaveChangesAsync();
        
    }
}