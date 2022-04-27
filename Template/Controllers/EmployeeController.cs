using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tamplate.DAL.Enitity;
using Template.BL.Helper;
using Template.BL.Interface;
using Template.BL.Models;

namespace Template.Controllers
{
    public class EmployeeController : Controller
    {

        #region Prop

        private readonly IDepartmentRep department;
        private readonly ICountryRep country;
        private readonly ICityRep city;
        private readonly IDistrictRep district;
        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;

        #endregion


        #region Ctor

        public EmployeeController(IEmployeeRep _employee, IDepartmentRep _department, ICountryRep _country, ICityRep _city, IDistrictRep _district, IMapper mapper)
        {
            department = _department;
            country = _country;
            city = _city;
            district = _district;
            employee = _employee;
            this.mapper = mapper;
        }

        #endregion


        #region Actions

        [HttpGet]
        public IActionResult Index(string Ename = "")
        {
            if (Ename == "" || Ename == null)
            {
                var model = employee.Get();
                var data = mapper.Map<IEnumerable<EmployeeVM>>(model);
                return View(data);
            }
            else
            {
                var model = employee.SearchByName(Ename);
                var data = mapper.Map<IEnumerable<EmployeeVM>>(model);
                return View(data);
            }
        }


        public IActionResult Details(int id)
        {
            var model = employee.GetById(id);
            var data = mapper.Map<EmployeeVM>(model);
            var departmentModel = mapper.Map<IEnumerable<DepartmentVM>>(department.Get());
            ViewBag.DepartmentList = new SelectList(departmentModel, "Id", "DepartmentName", data.DepartmentId);
            return View(data);
        }

        public IActionResult Create()
        {
            var departmentModel = mapper.Map<IEnumerable<DepartmentVM>>(department.Get());
            ViewBag.DepartmentList = new SelectList(departmentModel, "Id","DepartmentName");

            var countryModel = mapper.Map<IEnumerable<CountryVM>>(country.Get());
            ViewBag.CountryList = new SelectList(countryModel, "Id", "CountryName");

            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    model.CvUrl = UploadFileHelper.FileUploader("Files/CVs", model.Cv);
                    model.ImgUrl = UploadFileHelper.FileUploader("Files/IMGs", model.Img);

                    var obj = mapper.Map<Employee>(model);
                    employee.Create(obj);
                    return RedirectToAction("Index");
                }

                var departmentModel = mapper.Map<IEnumerable<DepartmentVM>>(department.Get());
                ViewBag.DepartmentList = new SelectList(departmentModel, "Id", "DepartmentName");

                var countryModel = mapper.Map<IEnumerable<CountryVM>>(country.Get());
                ViewBag.CountryList = new SelectList(countryModel, "Id", "CountryName");

                return View();

            }
            catch (Exception ex)
            {
                var departmentModel = mapper.Map<IEnumerable<DepartmentVM>>(department.Get());
                ViewBag.DepartmentList = new SelectList(departmentModel, "Id", "DepartmentName");

                var countryModel = mapper.Map<IEnumerable<CountryVM>>(country.Get());
                ViewBag.CountryList = new SelectList(countryModel, "Id", "CountryName");

                return View();
            }
        }


        public IActionResult Edit(int id)
        {


            var model = employee.GetById(id);
            var data = mapper.Map<EmployeeVM>(model);
            var departmentModel = mapper.Map<IEnumerable<DepartmentVM>>(department.Get());
            ViewBag.DepartmentList = new SelectList(departmentModel, "Id", "DepartmentName", data.DepartmentId);

            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var obj = mapper.Map<Employee>(model);
                    employee.Update(obj);
                    return RedirectToAction("Index");
                }

                var departmentModel = mapper.Map<IEnumerable<DepartmentVM>>(department.Get());
                ViewBag.DepartmentList = new SelectList(departmentModel, "Id", "DepartmentName", model.DepartmentId);

                return View();

            }
            catch (Exception ex)
            {
                var departmentModel = mapper.Map<IEnumerable<DepartmentVM>>(department.Get());
                ViewBag.DepartmentList = new SelectList(departmentModel, "Id", "DepartmentName", model.DepartmentId);

                return View();
            }
        }

        public IActionResult Delete(int id)
        {

            var model = employee.GetById(id);
            var data = mapper.Map<EmployeeVM>(model);
            var departmentModel = mapper.Map<IEnumerable<DepartmentVM>>(department.Get());
            ViewBag.DepartmentList = new SelectList(departmentModel, "Id", "DepartmentName", data.DepartmentId);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            try
            {

                var model = employee.GetById(id);

                UploadFileHelper.FileRemover("Files/CVs", model.CvUrl);
                UploadFileHelper.FileRemover("Files/IMGs", model.ImgUrl);

                employee.Delete(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        #endregion


        #region Ajax

        [HttpPost]
        public JsonResult GetCityDataByCountryId(int CtryId)
        {
            var model = city.Get(a => a.CountryId == CtryId);
            var data = mapper.Map<IEnumerable<CityVM>>(model);
            return Json(data);
        }

        [HttpPost]
        public JsonResult GetDistrictDataByCityId(int CtyId)
        {
            var model = district.Get(a => a.CityId == CtyId);
            var data = mapper.Map<IEnumerable<DistrictVM>>(model);
            return Json(data);
        }

        #endregion

    }
}
