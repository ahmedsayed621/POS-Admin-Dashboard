using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamplate.DAL.Database;
using Tamplate.DAL.Enitity;
using Template.BL.Interface;

namespace Template.BL.Repository
{
    public class CountryRep : ICountryRep
    {
        private readonly TemplateContext db;

        public CountryRep(TemplateContext db)
        {
            this.db = db;
        }

        public IEnumerable<Country> Get()
        {
            var data = db.Country.Select(a => a);
            return data;
        }


        public Country GetById(int id)
        {
            var data = db.Country.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }
}
