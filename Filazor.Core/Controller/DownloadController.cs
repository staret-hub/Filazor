using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filazor.Core.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult fileDownloadAsync(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            Stream stream = System.IO.File.OpenRead(fileInfo.FullName);

            return File(stream, "application/octet-stream", fileInfo.Name);
        }
    }
}
