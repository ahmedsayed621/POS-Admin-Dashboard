using Microsoft.EntityFrameworkCore;
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
    public class EmployeeRep : IEmployeeRep
    {
        private readonly TemplateContext db;

        public EmployeeRep(TemplateContext db)
        {
            this.db = db;
        }

        public IEnumerable<Employee> Get()
        {
            IQueryable<Employee> data = GetEmployees();
            return data;
        }


        public Employee GetById(int id)
        {
            var data = db.Employee.Include("Department").Include("District").Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public IEnumerable<Employee> SearchByName(string Name)
        {
            var data = db.Employee.Include("Department").Include("District").Where(a => a.Name.Contains(Name));
            return data;
        }

        public Employee Create(Employee obj)
        {
            db.Employee.Add(obj);
            db.SaveChanges();

            return db.Employee.OrderBy(a => a.Id).LastOrDefault();
        }

        public Employee Update(Employee obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();

            return db.Employee.Find(obj.Id);
        }

        public Employee Delete(Employee obj)
        {
            
            db.Employee.Remove(obj);
            db.SaveChanges();

            return obj;
        }


        // ============Refactor=========================

        private IQueryable<Employee> GetEmployees()
        {
            return db.Employee.Include("Department").Include("District").Select(a => a);
        }

        //Employee  ==> Department  -  Country   -  City   - District
        // Eager Load ==> Department  -  Country   -  City   - District
        // Lazy Load
    }
}
