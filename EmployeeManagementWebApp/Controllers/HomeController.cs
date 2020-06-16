using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementWebApp.Models;
using EmployeeManagementWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementWebApp.Controllers
{
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private  readonly IEmployeeReposiroty _employeeRepositry;
        public HomeController(IEmployeeReposiroty employeeReposiroty)
        {
            _employeeRepositry = employeeReposiroty;
        }
        //[Route("")]
        //[Route("Home")]
        //[Route("[action]")]
        //[Route("~/")]
        public ViewResult Index()
        {
         
            var model= _employeeRepositry.GetEmployeeList();
            return View(model);
        }
        //[Route("[action]/{id?}")]
        public ViewResult Details(int? id)
        {
           // ViewBag.PageTitle = "Details";
         //   Employee employee = _employeeRepositry.GetEmployee(id);
            //return View(a);
            //ViewBag.Employee = employee;
            //ViewBag.PageTitle = "This is Page Title";
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel()
            {

                Employee = _employeeRepositry.GetEmployee(id),
                PageTitle= "Page Title From ViewModel Property"
            };
            //viewModel.Employee = employee;
            //viewModel.PageTitle = "Page Title From ViewModel Property";
            return View(viewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepositry.AddEmployee(employee);
                return RedirectToAction("Details", new { id = newEmployee.Id });
            }
            else
            {
                return View();
            }
            
        }
    }
}