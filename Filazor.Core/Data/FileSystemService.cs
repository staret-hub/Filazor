using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Filazor.Core.Data
{
    public class FileSystemService
    {
        public Dictionary<string, string> MimeTypes = new Dictionary<string, string>
        {
            {"txt", "text/plain"},
            {"pdf", "application/pdf"},
            {"doc", "application/vnd.ms-word"},
            {"docx", "application/vnd.ms-word"},
            {"xls", "application/vnd.ms-excel"},
            {"xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
            {"png", "image/png"},
            {"jpg", "image/jpeg"},
            {"jpeg", "image/jpeg"},
            {"gif", "image/gif"},
            {"csv", "text/csv"}
        }; 


        public Task<string> GetHostName()
        {
            return Task.Run(() =>
            {
                return Environment.MachineName;
            });
        }

        public Task<DriveInfo[]> GetDriveListAsync()
        {
            return Task.Run(() =>
            {
                DriveInfo[] driveInfos = DriveInfo.GetDrives();
               
                return driveInfos;
            });
        }

        public Task<DirectoryInfo[]> GetDirectoryInfos(string path)
        {
            return Task.Run(() =>
            {
                DirectoryInfo[] result = null;

                try
                {
                    var dirInfo = new DirectoryInfo(path);

                    result = dirInfo.GetDirectories();
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine("UnauthorizedAccessException = {1}", e.Message, path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return result;
            });
        }

        public async Task<bool> DownloadFile(FileInfo fileInfo)
        {
            var uri = new System.Uri("https://localhost:5001/main");

            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync(uri);
            using (var fs = new FileStream(fileInfo.FullName, FileMode.Open))
            {
                await response.Content.CopyToAsync(fs);
            }

            return true;
        }

        private string GetContentType(string path)
        {
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return MimeTypes[ext];
        }
    }
}
