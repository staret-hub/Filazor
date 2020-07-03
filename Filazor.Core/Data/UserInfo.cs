using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Filazor.Core.Data
{
    public class UserInfo
    {
        public string id { get; set; }

        public string password { get; set; }

        public string salt { get; set; }
    }
}
