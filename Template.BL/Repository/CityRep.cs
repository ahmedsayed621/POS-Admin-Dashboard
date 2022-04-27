using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tamplate.DAL.Database;
using Tamplate.DAL.Enitity;
using Template.BL.Interface;

namespace Template.BL.Repository
{
    public class CityRep : ICityRep
    {
        private readonly TemplateContext db;

        public CityRep(TemplateContext db)
        {
            this.db = db;
        }

        public IEnumerable<City> Get(Expression<Func<City, bool>> filter = null)
        {
            var data = db.City.Select(a => a);

            if(filter != null)
            {
                data = db.City.Where(filter);
            }

            return data;
        }


        public City GetById(int id)
        {
            var data = db.City.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }
}
