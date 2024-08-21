using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadSExcel : ControllerBase
    {
        private readonly IUploadRepository _uploadRepository;

        public UploadSExcel(IUploadRepository uploadRepository)
        {
            _uploadRepository = uploadRepository;
        }

        [HttpPost("uploadCashExcel")]
        public async Task<IActionResult> UploadSub(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0; // إعادة تعيين الموضع إلى البداية
                
                // الحصول على قائمة الأشخاص
                var people = await _uploadRepository.ReadExcelFileAsync(stream);
                
                // تحميل البيانات إلى قاعدة البيانات
                await _uploadRepository.LoadSubToDatabase(people);

                return Ok("Data imported successfully.");
            }
            catch (UploadRepository.CustomException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "A custom error occurred while processing the file.",
                    details = ex.GetFullMessage()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An unexpected error occurred.",
                    details = ex.Message
                });
            }
        }







        [HttpPost("uploadRetirementExcel")]
        public async Task<IActionResult> UploadSub2(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0; // إعادة تعيين الموضع إلى البداية
                
                // الحصول على قائمة الأشخاص
                var people = await _uploadRepository.ReadExcelFileAsync2(stream);
                
                // تحميل البيانات إلى قاعدة البيانات
                await _uploadRepository.LoadSubToDatabase(people);

                return Ok("Data imported successfully.");
            }
            catch (UploadRepository.CustomException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "A custom error occurred while processing the file.",
                    details = ex.GetFullMessage()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An unexpected error occurred.",
                    details = ex.Message
                });
            }
        }



        [HttpPost("uploadBoxExcel")]
        public async Task<IActionResult> UploadSub3(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0; // إعادة تعيين الموضع إلى البداية
                
                // الحصول على قائمة الأشخاص
                var people = await _uploadRepository.ReadExcelFileAsync3(stream);
                
                // تحميل البيانات إلى قاعدة البيانات
                await _uploadRepository.LoadSubToDatabase(people);

                return Ok("Data imported successfully.");
            }
            catch (UploadRepository.CustomException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "A custom error occurred while processing the file.",
                    details = ex.GetFullMessage()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An unexpected error occurred.",
                    details = ex.Message
                });
            }
        }
    }

}