using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Filazor.Core.Data
{
    public class FileSystemService
    {
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
    }
}
