using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filazor.Core.Data
{
    public class LoginService
    {
        public bool Login(string id, string password)
        {
            if (id == "admin" && password == "Admin1234!")
            {
                Console.WriteLine("Login admin..");

                return true;
            }

            return false;
        }
    }
}
