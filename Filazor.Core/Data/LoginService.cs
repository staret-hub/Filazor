using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Filazor.Core.Shared;

namespace Filazor.Core.Data
{
    public class LoginService
    {
        public static bool Login(string id, string password)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Login failed : {0}", "Input was wrong.");
                
                return false;
            }

            string jsonUserInfos = GetUserInfoJsonString();

            byte[] salt;
            string encrypedPassword;
            using (JsonDocument jDoc = JsonDocument.Parse(jsonUserInfos))
            {
                foreach (JsonElement element in jDoc.RootElement.EnumerateArray())
                {
                    if (element.GetProperty("id").GetString() == id)
                    {
                        salt = element.GetProperty("salt").GetBytesFromBase64();
                        encrypedPassword = element.GetProperty("password").GetString();

                        if (encrypedPassword == Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8)))
                            return true;
                    }
                }
            }

            return false;
        }

        public static string ChangePassword(string userID, PasswordModel passwordModel)
        {
            Console.WriteLine("Sorry, This feature will be updated.");


            return null;
        }

        private static string GetUserInfoJsonString()
        {
            string result;
            using (StreamReader sr = File.OpenText(Common.USER_FILE_PATH))
            {
                result = sr.ReadToEnd();
                Console.WriteLine(result);
            }

            return result;
        }

        private static byte[] MakeSalt(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            return salt;
        }
    }
}
