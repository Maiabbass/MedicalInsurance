using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class Nots : ControllerBase
    {
     private readonly INoteService _noteService;

    public Nots(INoteService noteService)
    {
        _noteService = noteService;
    }

    

    // إضافة ملاحظة جديدة
    [HttpPost]
    public async Task<IActionResult> AddNoteAsync([FromBody] NoteCreateDTO noteDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            // استدعاء الخدمة لإضافة الملاحظة
            await _noteService.AddNoteAsync(noteDto);

            // إرجاع رسالة النجاح
            return Ok(new { message = "تمت إضافة الملاحظة بنجاح" });
        }
        catch (Exception ex)
        {
            // إرجاع رسالة خطأ في حالة حدوث استثناء
            return StatusCode(500, new { message = "حدث خطأ أثناء إضافة الملاحظة", error = ex.Message });
        }
    }



    [HttpGet("{ensuranceNumber}")]
   public async Task<IActionResult> GetNotesByInsuranceNumberAsync(string ensuranceNumber)
    {
        try
        {
            var notes = await _noteService.GetNotesByInsuranceNumberAsync(ensuranceNumber);

            if (notes == null || !notes.Any())
            {
                return NotFound(new { message = "لا توجد ملاحظات لهذا المهندس." });
            }

            // إرجاع الملاحظات في شكل DTO
            return Ok(notes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "حدث خطأ أثناء جلب الملاحظات", error = ex.Message });
        }
    }
    } }
