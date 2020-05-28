using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Filazor.Core.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Filazor.Core.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeleteController : ControllerBase
    {
        private readonly FileDeleteEventNotifyService fileDeleteEventNotifyService;

        public DeleteController(FileDeleteEventNotifyService service)
        {
            fileDeleteEventNotifyService = service;
        }

        [HttpGet("[action]")]
        public IActionResult fileDeleteAsync(string fileName)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(fileName);
                fileInfo.Delete();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            fileDeleteEventNotifyService.Notify(this);

            return StatusCode(200);
        }
    }
}
