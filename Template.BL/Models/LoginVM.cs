using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.BL.Models
{
    public class LoginVM
    {

        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "This field required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
