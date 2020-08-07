using Filazor.Core.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Filazor.Core.Data
{
    public class UserInfo
    {
        public string id { get; set; }

        public string password { get; set; }

        public string salt { get; set; }

        public UserInfo()
        {
            // Default User and password
            id = "Admin";
            password = "2+RiUCswdFn6nqsMnSvlhVzdoy11PBj/GKorobJXtDo=";
            salt = "gq11Pc3RZsEnd2ceMJMisw==";
        }

        public static byte[] SerializeToUtf8Bytes(List<UserInfo> list)
        {
            return JsonSerializer.SerializeToUtf8Bytes(list, new JsonSerializerOptions() { WriteIndented = true });
        }

        public static void WriteFile(byte[] jsonUtf8bytes)
        {
            try
            {
                File.WriteAllBytes(Common.USER_FILE_PATH, jsonUtf8bytes);
                Common.DebugPrint("Created User.json file.");
            }
            catch (Exception e)
            {
                Common.DebugPrint(e.Message);
            }
        }

    }
}
