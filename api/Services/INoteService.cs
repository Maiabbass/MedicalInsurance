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
        
    }
}