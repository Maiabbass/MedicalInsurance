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
        public async Task<IActionResult> UploadSubCash(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0; // إعادة تعيين الموضع إلى البداية
                
                // الحصول على قائمة الأشخاص
                var people = await _uploadRepository.ReadExcelFileCash(stream);
                
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
        public async Task<IActionResult> UploadSubRetirement(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0; // إعادة تعيين الموضع إلى البداية
                
                // الحصول على قائمة الأشخاص
                var people = await _uploadRepository.ReadExcelFileRetirement(stream);
                
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
        public async Task<IActionResult> UploadSubBox(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0; 
                
                var people = await _uploadRepository.ReadExcelFileBox(stream);
                
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




     [HttpPost("uploadHospitalExcel")]
public async Task<IActionResult> UploadSubHospital(IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest("No file uploaded.");

    try
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;

        var hospitals = await _uploadRepository.ReadExcelFileHospital(stream);

        if (hospitals.Count == 0)
        {
            return BadRequest("No valid data found in the file.");
        }

        await _uploadRepository.LoadSubToHospital(hospitals);

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

      [HttpPost("uploadSurgicalExcel")]
public async Task<IActionResult> UploadSubSurgical(IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest("No file uploaded.");

    try
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;

        var surgicals = await _uploadRepository.ReadExcelFileSurgical(stream);

        if (surgicals.Count == 0)
        {
            return BadRequest("No valid data found in the file.");
        }

        await _uploadRepository.LoadSubToSurgical(surgicals);

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