﻿using System;
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
    [AllowAnonymous]
    public class LoginService
    {
        public static bool Login(string id, string password)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Login failed : {0}", "Input was wrong.");
                
                return false;
            }

            //using (StreamReader sr = File.OpenText(Common.USER_FILE_PATH))
            //{
            //    string s;
            //    while ((s = sr.ReadLine()) != null)
            //    {
            //        Console.WriteLine(s);
            //    }
            //}

            byte[] salt = Convert.FromBase64String("gq11Pc3RZsEnd2ceMJMisw=="); // MakeSalt(password);
            //string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));
            string encrypedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));

            return encrypedPassword == "2+RiUCswdFn6nqsMnSvlhVzdoy11PBj/GKorobJXtDo=";
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
