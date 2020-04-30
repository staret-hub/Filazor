using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filazor.Core.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment environment;

        public UploadController(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> fileUploadAsync(IFormFile[] files, string currentDirectory)
        {
            try
            {
                if (HttpContext.Request.Form.Files.Any())
                {
                    foreach (var file in HttpContext.Request.Form.Files)
                    {
                        string path = Path.Combine(currentDirectory, file.FileName);
                        Console.WriteLine(path);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        Console.WriteLine($"got file: {path}");
                    }
                }

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}