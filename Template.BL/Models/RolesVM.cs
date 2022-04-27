using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.BL.Models
{
    public class RolesVM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
