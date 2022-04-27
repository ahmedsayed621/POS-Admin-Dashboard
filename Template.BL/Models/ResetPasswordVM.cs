using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.BL.Models
{
    public class ResetPasswordVM
    {

        [Required(ErrorMessage = "This field required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        [Compare("Password", ErrorMessage = "Password not match")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }

    }
}
