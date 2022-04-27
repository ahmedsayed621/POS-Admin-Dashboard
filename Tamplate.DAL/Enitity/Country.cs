using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Tamplate.DAL.Enitity
{
    [Table("Country")]
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public ICollection<City> City { get; set; }
    }
}
