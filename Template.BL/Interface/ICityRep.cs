using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tamplate.DAL.Enitity;

namespace Template.BL.Interface
{
    public interface ICityRep
    {
        IEnumerable<City> Get(Expression<Func<City, bool>> filter = null);
        City GetById(int id);

    }
}
