using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using static api.Repositories.UploadRepository;
using OfficeOpenXml;

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



    [HttpPost("import")]
        public async Task<IActionResult> ImportSubscribers(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty");

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var subscribers = await _uploadRepository.ImportSubscribersAsync(stream);

                    // Save subscribers to the database using your existing method
                    await _uploadRepository.LoadSubToDatabase2024(subscribers);

                    return Ok($"Successfully imported {subscribers.Count} subscribers");
                }
            }
            catch (CustomException ex)
            {
                // Log the custom exception
                return BadRequest($"An error occurred during import: {ex.GetFullMessage()}");
            }
            catch (Exception ex)
            {
                // Log the general exception
                return StatusCode(500, $"An unexpected error occurred during import: {ex.Message}");
            }
        }




         [HttpPost("uploadUniteExcel")]
        public async Task<IActionResult> UploadSubUnite(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0; // إعادة تعيين الموضع إلى البداية
                
                // الحصول على قائمة الأشخاص
                var people = await _uploadRepository.ReadExcelFileUnits(stream);
                
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




          [HttpPost("uploadUniteExcel2")]
        public async Task<IActionResult> UploadSubUnite2(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                using var stream = new MemoryStream();
                await file.CopyToAsync(stream);
                stream.Position = 0; // إعادة تعيين الموضع إلى البداية
                
                // الحصول على قائمة الأشخاص
                var people = await _uploadRepository.ReadExcelFileUnits2(stream);
                
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



    

