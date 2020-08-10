using Filazor.Core.Shared;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

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

            string result = CheckPassword(id, password);
            if (string.IsNullOrEmpty(result))
            {
                return true;
            }
            Console.WriteLine("Login failed : {0}", result);

            return false;
        }

        public static string ChangePassword(string userID, PasswordModel passwordModel)
        {
            byte[] jsonUtf8Bytes = GetUserInfoJsonUtf8Bytes();
            var readOnlySpan = new ReadOnlySpan<byte>(jsonUtf8Bytes);
            List<UserInfo> userInfoList = JsonSerializer.Deserialize<List<UserInfo>>(readOnlySpan);
            foreach (var userInfo in userInfoList)
            {
                if (userInfo.id == userID)
                {
                    if (userInfo.password != EncryptedBase64String(passwordModel.CurrentPassword, Convert.FromBase64String(userInfo.salt)))
                    {
                        return "Please, check your password.";
                    }

                    byte[] salt = MakeSalt(passwordModel.NewPassword);
                    userInfo.password = EncryptedBase64String(passwordModel.NewPassword, salt);
                    userInfo.salt = Convert.ToBase64String(salt);

                    byte[] jsonUtf8bytes = UserInfo.SerializeToUtf8Bytes(userInfoList);
                    UserInfo.WriteFile(jsonUtf8bytes);

                    return null;
                }
            }

            return "Please, check your ID.";
        }

        private static string CheckPassword(string id, string password)
        {
            byte[] jsonUtf8Bytes = GetUserInfoJsonUtf8Bytes();

            var readOnlySpan = new ReadOnlySpan<byte>(jsonUtf8Bytes);
            List<UserInfo> userInfoList = JsonSerializer.Deserialize<List<UserInfo>>(readOnlySpan);

            foreach (UserInfo userInfo in userInfoList)
            {
                if (userInfo.id == id)
                {
                    if (userInfo.password == EncryptedBase64String(password, Convert.FromBase64String(userInfo.salt)))
                    {
                        return null;
                    }
                    else
                    {
                        return "Please, check your password.";
                    }
                }
            }

            return "Please, check your ID.";
        }

        private static string EncryptedBase64String(string password, byte[] salt)
        {
            byte[] encryptedPassword = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8);
            return Convert.ToBase64String(encryptedPassword);
        }

        private static string GetUserInfoJsonString()
        {
            string result = null;

            try
            {
                result = File.ReadAllText(Common.USER_FILE_PATH);
                Common.DebugPrint(result);
            }
            catch (Exception e)
            {
                Common.DebugPrint(e.Message);
            }

            return result;
        }

        private static byte[] GetUserInfoJsonUtf8Bytes()
        {
            byte[] result = null;

            try
            {
                result = File.ReadAllBytes(Common.USER_FILE_PATH);
            }
            catch (Exception e)
            {
                Common.DebugPrint(e.Message);
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
