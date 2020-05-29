using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
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
                catch (Exception ex)
                {
                    if (ex is UnauthorizedAccessException || ex is SecurityException)
                    {
                        Console.WriteLine("{0} = {1}", path, ex.Message);
                    }
                    else
                    {
                        Console.WriteLine("{0}", ex.Message, path);
                    }
                }

                List<DirectoryInfo> filteredList = new List<DirectoryInfo>();
                if (result != null && result.Length > 0)
                {
                    foreach (var dirInfo in result)
                    {
                        if ((dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden
                            || (dirInfo.Attributes & FileAttributes.System) == FileAttributes.System)
                        {
                            //Console.WriteLine("{0} = {1}", dirInfo.FullName, dirInfo.Attributes.ToString());
                            continue;
                        }

                        filteredList.Add(dirInfo);
                    }
                }

                return filteredList.ToArray();
            });
        }

        public Task<FileInfo[]> GetFiles(DirectoryInfo dirInfo)
        {
            return Task.Run(() =>
            { 
                List<FileInfo> result = new List<FileInfo>();

                var fileInfos = dirInfo.GetFiles();
                if (fileInfos != null || fileInfos.Length > 0)
                {
                    foreach (var fileInfo in fileInfos)
                    {
                        if ((fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden
                            || (fileInfo.Attributes & FileAttributes.System) == FileAttributes.System)
                        {
                            continue;
                        }

                        result.Add(fileInfo);
                    }
                }

                return result.ToArray();
            });
        }

        public Task<bool> DeleteFile(string fileName)
        {
            return Task.Run(() =>
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(fileName);
                    fileInfo.Delete();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

                return true;
            });
        }
    }
}
