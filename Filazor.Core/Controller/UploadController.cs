using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Filazor.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Filazor.Core.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly FileUploadEventNotifyService fileUploadEventNotifyService;
        
        public UploadController(FileUploadEventNotifyService service)
        {
            fileUploadEventNotifyService = service;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> fileUploadAsync(IFormFile[] files, string uploadDirectory)
        {
            try
            {
                if (HttpContext.Request.Form.Files.Any())
                {
                    foreach (var file in HttpContext.Request.Form.Files)
                    {
                        string path = Path.Combine(uploadDirectory, file.FileName);
                        //Console.WriteLine(path);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        //Console.WriteLine($"got file: {path}");
                    }
                }

                fileUploadEventNotifyService.Notify(this);

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}