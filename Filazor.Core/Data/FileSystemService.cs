using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Filazor.Core.Data
{
    public class FileSystemService
    {
        public Task<DriveInfo[]> GetDriveListAsync()
        {
            return Task.Run(() =>
            {
                DriveInfo[] driveInfos = DriveInfo.GetDrives();

                return driveInfos;
            });
        }
    }
}
