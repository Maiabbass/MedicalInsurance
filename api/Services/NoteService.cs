using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOS;
using api.Entities;
using api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class NoteService : INoteService
    {

     private readonly INoteRepository _noteRepository;
     private readonly DataContext _dataContext;

    public NoteService(INoteRepository noteRepository, DataContext dataContext)
    {
        _noteRepository = noteRepository;
        _dataContext = dataContext;
    }

    public async Task AddNoteAsync(NoteCreateDTO noteDto)
    {
        var note = new Note
        {
            Content = noteDto.Content,
            PersonId = noteDto.PersonId
        };

        await _noteRepository.AddNoteAsync(note);
    }


       public async Task<IEnumerable<NoteCreateDTO>> GetNotesByInsuranceNumberAsync(string ensuranceNumber)
    {
        // البحث عن الشخص بناءً على الرقم التأميني
        var person = await _dataContext.Persons
                                   .FirstOrDefaultAsync(p => p.EnsuranceNumber == ensuranceNumber);
        
        if (person == null)
        {
            throw new Exception("المهندس غير موجود.");
        }

        // جلب الملاحظات بناءً على PersonId
        var notes = await _noteRepository.GetNotesByPersonIdAsync(person.Id);

        // تحويل الملاحظات إلى كائنات DTO
        return notes.Select(note => new NoteCreateDTO
        {
            Id = note.Id,
            Content = note.Content,
            PersonId = (int)note.PersonId
        }).ToList();
    }
}
}
        
    
