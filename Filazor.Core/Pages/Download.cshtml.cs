using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Filazor.Core.Pages
{
    public class DownloadModel : PageModel
    {
        public IActionResult OnGet(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            Stream stream = System.IO.File.OpenRead(fileInfo.FullName);

            return File(stream, "application/octet-stream", fileInfo.Name);
        }
    }
}