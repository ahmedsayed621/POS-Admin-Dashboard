using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamplate.DAL.Enitity;

namespace Template.BL.Interface
{
    public interface ICountryRep
    {
        IEnumerable<Country> Get();
        Country GetById(int id);
    }
}
