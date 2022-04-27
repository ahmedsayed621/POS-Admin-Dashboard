using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamplate.DAL.Database;
using Tamplate.DAL.Enitity;
using Template.BL.Interface;
using Template.BL.Models;
using Microsoft.EntityFrameworkCore;


namespace Template.BL.Repository
{
    public class DepartmentRep : IDepartmentRep
    {

        private readonly TemplateContext db;

        public DepartmentRep(TemplateContext db)
        {
            this.db = db;
        }

        public IEnumerable<Department> Get()
        {
            IQueryable<Department> data = GetDepartments();
            return data;
        }


        public Department GetById(int id)
        {
            var data = db.Department.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }

        public IEnumerable<Department> SearchByName(string Name)
        {
            var data = db.Department.Where(a=>a.DepartmentName.Contains(Name));
            return data;
        }

        public void Create(Department obj)
        {
            db.Department.Add(obj);
            db.SaveChanges();
        }

        public void Update(Department obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Department obj)
        {
            db.Department.Remove(obj);
            db.SaveChanges();
        }


        // ============Refactor=========================

        private IQueryable<Department> GetDepartments()
        {
            return db.Department.Select(a => a);
        }


    }
}
