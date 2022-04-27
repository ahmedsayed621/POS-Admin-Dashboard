using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamplate.DAL.Enitity;

namespace Template.BL.Models
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Name Required")]
        [MaxLength(50,ErrorMessage = "* Max Len 50")]
        [MinLength(3,ErrorMessage = "* Min Len 3")]
        public string Name { get; set; }

        [Range(3000,20000,ErrorMessage = "* Salary Btw 3K To 20K")]
        public float Salary { get; set; }

        [Required(ErrorMessage = "* HireDate Required")]
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }

        [EmailAddress(ErrorMessage = "* Enter A vaild Mail Address")]
        public string Email { get; set; }
        
        [RegularExpression("[0-9]{1,5}[a-zA-Z]{3,10}-[a-zA-Z]{3,10}-[a-zA-Z]{3,10}", ErrorMessage = "* Addree Like 13StreetName-CityName-CountryName")]
        public string Address { get; set; }
        public DateTime CreationDate { get; set; }

        [Required(ErrorMessage = "* Choose Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public string CvUrl { get; set; }
        public string ImgUrl { get; set; }
        public IFormFile Cv { get; set; }
        public IFormFile Img { get; set; }


    }
}
