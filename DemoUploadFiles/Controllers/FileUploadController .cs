using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DemoUploadFiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly DataContext _context;

        public FileUploadController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var fileUpload = new FileUpload
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType, // שמור את סוג התוכן
                    Data = memoryStream.ToArray()
                };
                _context.FileUploads.Add(fileUpload);
                await _context.SaveChangesAsync();
            }

            return Ok(new { Message = "File uploaded successfully" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var fileUpload = await _context.FileUploads.FindAsync(id);
            if (fileUpload == null)
                return NotFound();

            return File(fileUpload.Data, fileUpload.ContentType, fileUpload.FileName); // השתמש בסוג התוכן השמור
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var fileUpload = await _context.FileUploads.FindAsync(id);
            if (fileUpload == null)
                return NotFound();

            _context.FileUploads.Remove(fileUpload);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "File deleted successfully" });
        }
    }


    //    [HttpPost("upload")]
    //    public async Task<IActionResult> UploadFile(IFormFile file)
    //    {
    //        if (file == null || file.Length == 0)
    //            return BadRequest("File not selected");

    //        using (var memoryStream = new MemoryStream())
    //        {
    //            await file.CopyToAsync(memoryStream);
    //            var fileUpload = new FileUpload
    //            {
    //                FileName = file.FileName,
    //                Data = memoryStream.ToArray(),
    //                ContentType = file.ContentType // Add this line

    //            };
    //            _context.FileUploads.Add(fileUpload);
    //            await _context.SaveChangesAsync();
    //        }

    //        return Ok(new { Message = "File uploaded successfully" });
    //    }

    //    [HttpGet("{id}")]
    //    public async Task<IActionResult> GetFile(int id)
    //    {
    //        var fileUpload = await _context.FileUploads.FindAsync(id);
    //        if (fileUpload == null)
    //            return NotFound();

    //        //return File(fileUpload.Data, "application/octet-stream", fileUpload.FileName);
    //        return File(fileUpload.Data, fileUpload.ContentType, fileUpload.FileName); // Use fileUpload.ContentType here
    //    }
    //}
}
