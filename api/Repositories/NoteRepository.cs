using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class NoteRepository : INoteRepository
    {

     private readonly DataContext _dataContext;

    public NoteRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task AddNoteAsync(Note note)
    {
        _dataContext.Notes.Add(note);
        await _dataContext.SaveChangesAsync();
    }


    public async Task<IEnumerable<Note>> GetNotesByPersonIdAsync(int personId)
    {
        return await _dataContext.Notes
                             .Where(note => note.PersonId == personId)
                             .ToListAsync();
    }


    public async Task AddNotesAsyncList(List<Note> notes)
{
    _dataContext.Notes.AddRange(notes);
    await _dataContext.SaveChangesAsync();
}



        public async Task Delete_NotesByYearConfigId(int yearConfigId)
    {
        var notesToDelete = await _dataContext.Notes
            .Where(note => note.YearConfigId == yearConfigId)
            .ToListAsync();

        if (notesToDelete.Any())
        {
            _dataContext.Notes.RemoveRange(notesToDelete);
            await _dataContext.SaveChangesAsync();
        }
    }




   public async Task DeleteNotesByHospitalId(int hospitalId)
{
    var notesToDelete = await _dataContext.Notes.Where(note => note.HospitalId == hospitalId).ToListAsync();
    if (notesToDelete.Any())
    {
        _dataContext.Notes.RemoveRange(notesToDelete);
        await _dataContext.SaveChangesAsync();  // Asynchronously saving changes
    }
}




  public async Task DeleteNotesBySurgicalProcedureId(int surgicalProcedureId)
{
    var notesToDelete = await _dataContext.Notes.Where(note => note.SurgicalProcedureId == surgicalProcedureId).ToListAsync();
    if (notesToDelete.Any())
    {
        _dataContext.Notes.RemoveRange(notesToDelete);
        await _dataContext.SaveChangesAsync();
    }
}



      public async Task DeleteNotesByPersonId(int personId)
{
    var notesToDelete = await _dataContext.Notes
        .Where(note => note.PersonId == personId)
        .ToListAsync();

    if (notesToDelete.Any())
    {
        _dataContext.Notes.RemoveRange(notesToDelete);
        await _dataContext.SaveChangesAsync();
    }
}

  

  

   public async Task Delete_NotesByYearAsync(int year)
{
    var yearConfig = await _dataContext.YearConfigurations
        .FirstOrDefaultAsync(yc => yc.Year == year);

    if (yearConfig != null)
    {
        int yearConfigId = yearConfig.Id;

        var notesToDelete = await _dataContext.Notes
            .Where(note => note.YearConfigId == yearConfigId && note.PersonId == null)
            .ToListAsync();

        if (notesToDelete.Any())
        {
            _dataContext.Notes.RemoveRange(notesToDelete);
            await _dataContext.SaveChangesAsync();
        }
    }
    else
    {
        throw new Exception($"No configuration found for the year {year}");
    }
}




  public async Task<YearConfiguration> GetYearConfigurationByIdAsync(int yearConfigId)
    {
        return await _dataContext.YearConfigurations
            .Include(y => y.Notes) // Include related notes
            .FirstOrDefaultAsync(y => y.Id == yearConfigId);
    }



     public async Task SaveChangesAsync()
    {
        await _dataContext.SaveChangesAsync();
    }




        
    }
}