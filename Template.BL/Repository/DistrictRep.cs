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
    public class DistrictRep : IDistrictRep
    {
        private readonly TemplateContext db;

        public DistrictRep(TemplateContext db)
        {
            this.db = db;
        }

        public IEnumerable<District> Get(Expression<Func<District, bool>> filter = null)
        {
            var data = db.District.Select(a => a);

            if (filter != null)
            {
                data = db.District.Where(filter);
            }
            return data;
        }


        public District GetById(int id)
        {
            var data = db.District.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
    }
}
