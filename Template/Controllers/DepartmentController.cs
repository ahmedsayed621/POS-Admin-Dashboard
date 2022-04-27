using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tamplate.DAL.Database;
using Tamplate.DAL.Enitity;
using Template.BL.Interface;
using Template.BL.Models;
using Template.BL.Repository;

namespace Template.Controllers
{
    public class DepartmentController : Controller
    {


        #region Prop

        // tightly coupled
        //private readonly DepartmentRep department;

        // Loosly coupled 
        private readonly IDepartmentRep department;
        private readonly IMapper mapper;

        #endregion


        #region Ctor

        public DepartmentController(IDepartmentRep _department, IMapper mapper)
        {
            department = _department;
            this.mapper = mapper;
        }

        #endregion


        #region Actions

        [HttpGet]
        public IActionResult Index(string Dname = "")
        {
            if(Dname == "" || Dname == null)
            {
                var model = department.Get();
                var data = mapper.Map<IEnumerable<DepartmentVM>>(model);
                return View(data);
            }
            else
            {
                var model = department.SearchByName(Dname);
                var data = mapper.Map<IEnumerable<DepartmentVM>>(model);
                return View(data);
            }
        }


        public IActionResult Details(int id)
        {
            var model = department.GetById(id);
            var data = mapper.Map<DepartmentVM>(model);
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentVM model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var obj = mapper.Map<Department>(model);
                    department.Create(obj);
                    return RedirectToAction("Index");
                }

                return View();

            }
            catch (Exception ex)
            {
                return View();
            }
        }


        public IActionResult Edit(int id)
        {


            var model = department.GetById(id);
            var data = mapper.Map<DepartmentVM>(model);

            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var obj = mapper.Map<Department>(model);
                    department.Update(obj);
                    return RedirectToAction("Index");
                }

                return View();

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {

            var model = department.GetById(id);
            var data = mapper.Map<DepartmentVM>(model);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {
                var model = department.GetById(id);
                department.Delete(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        #endregion

    }
}
