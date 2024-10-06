using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Entities;

namespace api.Services
{
    public interface INoteService
    {

        Task AddNoteAsync(NoteCreateDTO noteDto);
        Task<IEnumerable<NoteCreateDTO>> GetNotesByInsuranceNumberAsync(string ensuranceNumber);
        Task<string> DeleteNoteByIdAsync(int? personId, int? ageSegmentId, int? relationId, int? hospitalId, int? surgicalProcedureId);
        Task<string> DeleteNoteAsync(int id);
         Task<string> EditNoteAsync(int id, string newContent);
    }
}