using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYvMiniProject2.Data.Models
{
    public class LoginResponseModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Token { get; set; }

        public int coin {  get; set; }
    }
}
