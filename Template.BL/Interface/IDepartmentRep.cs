using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamplate.DAL.Enitity;
using Template.BL.Models;

namespace Template.BL.Interface
{
    public interface IDepartmentRep
    {
        IEnumerable<Department> Get();
        Department GetById(int id);
        IEnumerable<Department> SearchByName(string Name);
        void Create(Department obj);
        void Update(Department obj);
        void Delete(Department obj);
    }
}
