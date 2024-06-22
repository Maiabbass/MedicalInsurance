using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.DTOS;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberRepository _subscriberRepository;

        public SubscriberController(ISubscriberRepository subscriberRepository)
        {
            _subscriberRepository = subscriberRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadSub(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            
            //  الحصول على قائمة الأشخاص
            var people = await _subscriberRepository.ReadExcelFileAsync(stream);
            
            // تحميل البيانات إلى قاعدة البيانات
            await _subscriberRepository.LoadSubToDatabase(people);

            return Ok("Data imported successfully.");
        }
    }
}
