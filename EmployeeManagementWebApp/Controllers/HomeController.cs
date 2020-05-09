using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementWebApp.Controllers
{
    public class HomeController : Controller
    {
        private  readonly IEmployeeReposiroty _employeeRepositry;
        public HomeController(IEmployeeReposiroty employeeReposiroty)
        {
            _employeeRepositry = employeeReposiroty;
        }
        public string Index()
        {

            //return View(a);
            return _employeeRepositry.GetEmployee(1).Name;
        }
        public JsonResult GetEmployeeDetails(int id)
        {
            Employee employee = _employeeRepositry.GetEmployee(id);
            //return View(a);
            return Json(employee);
        }
    }
}