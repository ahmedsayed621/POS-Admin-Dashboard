using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;    
    
namespace Template.BL.Models
{
    public class RegistrationVM
    {


        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "This field required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field required")]
        [MinLength(6,ErrorMessage = "Min Len 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field required")]
        [MinLength(6, ErrorMessage = "Min Len 6")]
        [Compare("Password", ErrorMessage ="Password not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "This field required")]
        public bool IsAgree { get; set; }


    }
}
