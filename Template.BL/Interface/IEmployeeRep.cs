using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamplate.DAL.Enitity;

namespace Template.BL.Interface
{
    public interface IEmployeeRep
    {
        IEnumerable<Employee> Get();
        Employee GetById(int id);
        IEnumerable<Employee> SearchByName(string Name);
        Employee Create(Employee obj);
        Employee Update(Employee obj);
        Employee Delete(Employee obj);
    }
}
