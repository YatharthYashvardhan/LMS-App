using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYvMiniProject2.Data.Models
{
    public class SignInModel
    {
        public SignInModel()
        {
            Email = "default@email.com";
            Password = "password";
        }
        [Required(ErrorMessage = "Enter Valid Email"), EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RemberMe { get; set; }
    }
}
