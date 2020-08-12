using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Filazor.Core.Data
{
    public class UserLoginModel
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
