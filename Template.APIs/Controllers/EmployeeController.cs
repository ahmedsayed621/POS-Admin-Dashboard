using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tamplate.DAL.Enitity;
using Template.BL.Helper;
using Template.BL.Interface;
using Template.BL.Models;

namespace Template.APIs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {


        #region Fields

        private readonly IEmployeeRep employee;
        private readonly IMapper mapper;

        #endregion


        #region Ctor

        public EmployeeController(IEmployeeRep employee,IMapper mapper)
        {
            this.employee = employee;
            this.mapper = mapper;
        }


        #endregion


        #region APIs

        [Route("~/api/Employee/GetEmployees")]
        [HttpGet]

        //[DisableCors]
        //[EnableCors("")]
        public IActionResult GetEmployees()
        {

            try
            {
                var data = employee.Get();
                var model = mapper.Map<IEnumerable<EmployeeVM>>(data);

                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>()
                {

                    Code = "200",
                    Status = "Ok",
                    Msg = "Data Found",
                    Response = model

                });

            }catch(Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {

                    Code = "404",
                    Status = "Not Found",
                    Msg = "Data Not Found",
                    Response = ex.Message

                });
            }
        }


        [Route("~/api/Employee/GetEmployeesById/{id}")]
        [HttpGet]
        public IActionResult GetEmployeesById(int id)
        {

            try
            {
                var data = employee.GetById(id);
                var model = mapper.Map<EmployeeVM>(data);

                return Ok(new ApiResponse<EmployeeVM>()
                {

                    Code = "200",
                    Status = "Ok",
                    Msg = "Data Found",
                    Response = model

                });

            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {

                    Code = "404",
                    Status = "Not Found",
                    Msg = "Data Not Found",
                    Response = ex.Message

                });
            }
        }


        [Route("~/api/Employee/PostEmployee")]
        [HttpPost]
        public IActionResult PostEmployee(EmployeeVM model)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(model);
                    var result = employee.Create(data);

                return Ok(new ApiResponse<Employee>()
                {

                    Code = "201",
                    Status = "Created",
                    Msg = "Data Saved",
                    Response = result

                });

                }

                return BadRequest(new ApiResponse<string>()
                {
                    Code = "400",
                    Status = "Bad Request",
                    Msg = "Data Not Valid"
                });

            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {

                    Code = "400",
                    Status = "Bad Request",
                    Msg = "Can't Create",
                    Response = ex.Message

                });
            }
        }


        [Route("~/api/Employee/PutEmployee")]
        [HttpPut]
        public IActionResult PutEmployee(EmployeeVM model)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Employee>(model);
                    var result = employee.Update(data);

                    return Ok(new ApiResponse<Employee>()
                    {

                        Code = "201",
                        Status = "Updated",
                        Msg = "Data Updated",
                        Response = result

                    });

                }

                return BadRequest(new ApiResponse<string>()
                {

                    Code = "400",
                    Status = "Bad Request",
                    Msg = "Data Not Valid"
                });

            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {

                    Code = "400",
                    Status = "Bad Request",
                    Msg = "Can't Create",
                    Response = ex.Message

                });
            }
        }



        [Route("~/api/Employee/DeleteEmployee")]
        [HttpDelete]
        public IActionResult DeleteEmployee(EmployeeVM model)
        {

            try
            {
                    var data = mapper.Map<Employee>(model);
                    var result = employee.Delete(data);

                    return Ok(new ApiResponse<Employee>()
                    {

                        Code = "201",
                        Status = "Deleted",
                        Msg = "Data Deleted",
                        Response = result

                    });

            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>()
                {

                    Code = "400",
                    Status = "Bad Request",
                    Msg = "Can't Delete",
                    Response = ex.Message

                });
            }
        }

        #endregion


    }
}
