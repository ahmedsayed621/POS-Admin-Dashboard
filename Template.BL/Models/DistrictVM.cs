using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamplate.DAL.Enitity;

namespace Template.BL.Models
{
    public class DistrictVM
    {
        public int Id { get; set; }
        public string DistrictName { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
