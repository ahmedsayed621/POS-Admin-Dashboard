using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Template.BL.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Department Name")]
        [MinLength(5,ErrorMessage = "Min 5 Characters")]
        [MaxLength(50,ErrorMessage = "Max 50 Characters")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Enter Department Code")]
        [MinLength(5, ErrorMessage = "Min 5 Characters")]
        [MaxLength(50, ErrorMessage = "Max 50 Characters")]
        public string DepartmentCode { get; set; }
    }
}
