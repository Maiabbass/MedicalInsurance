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




     public async Task DeleteNotesByPersonIdAsync(int personId) {
    var notesToDelete = await _dataContext.Notes
        .Where(note => note.PersonId == personId)
        .ToListAsync();

    if (notesToDelete.Any()) {
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
    

  public async Task<bool> DeleteNoteAsync(int? personId = null, int? ageSegmentId = null, int? relationId = null, int? hospitalId = null, int? surgicalProcedureId = null)
    {
        Note noteToDelete = null;

        // البحث عن الـ Note حسب نوع المعرف الذي تم تمريره
        if (personId.HasValue)
        {
            noteToDelete = await _dataContext.Notes.FirstOrDefaultAsync(n => n.PersonId == personId.Value);
        }
        else if (ageSegmentId.HasValue)
        {
            noteToDelete = await _dataContext.Notes.FirstOrDefaultAsync(n => n.AgeSegmentId == ageSegmentId.Value);
        }
        else if (relationId.HasValue)
        {
            noteToDelete = await _dataContext.Notes.FirstOrDefaultAsync(n => n.RelationId == relationId.Value);
        }
        else if (hospitalId.HasValue)
        {
            noteToDelete = await _dataContext.Notes.FirstOrDefaultAsync(n => n.HospitalId == hospitalId.Value);
        }
        else if (surgicalProcedureId.HasValue)
        {
            noteToDelete = await _dataContext.Notes.FirstOrDefaultAsync(n => n.SurgicalProcedureId == surgicalProcedureId.Value);
        }

        // إذا لم يتم العثور على السجل، ارجع false
        if (noteToDelete == null)
        {
            return false;
        }

        // حذف السجل
        _dataContext.Notes.Remove(noteToDelete);
        await _dataContext.SaveChangesAsync();

        return true;
    }


      public async Task<bool> DeleteNoteByIdAsync(int id)
    {
        var noteToDelete = await _dataContext.Notes.FindAsync(id);

        // إذا لم يتم العثور على الملاحظة، ارجع false
        if (noteToDelete == null)
        {
            return false;
        }

        // حذف الملاحظة
        _dataContext.Notes.Remove(noteToDelete);
        await _dataContext.SaveChangesAsync();

        return true;
    }




      public async Task<bool> EditNoteAsync(int id, string newContent)
    {
        var noteToEdit = await _dataContext.Notes.FindAsync(id);

        // إذا لم يتم العثور على الملاحظة، ارجع false
        if (noteToEdit == null)
        {
            return false;
        }

        // تعديل محتوى الملاحظة
        noteToEdit.Content = newContent;

        // حفظ التعديلات
        _dataContext.Notes.Update(noteToEdit);
        await _dataContext.SaveChangesAsync();

        return true;
    }
}


        
    }
