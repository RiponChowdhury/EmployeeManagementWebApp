using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementWebApp.Models;
using EmployeeManagementWebApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementWebApp.Controllers
{
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private  readonly IEmployeeReposiroty _employeeRepositry;
        private readonly IHostingEnvironment hostingEnvironment;

        public HomeController(IEmployeeReposiroty employeeReposiroty,
            IHostingEnvironment hostingEnvironment)
        {
            _employeeRepositry = employeeReposiroty;
            this.hostingEnvironment = hostingEnvironment;
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
        public IActionResult Create(EmployeeCreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (createViewModel.Photo != null)
                {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + createViewModel.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    createViewModel.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Employee employee = new Employee
                {

                    Name = createViewModel.Name,
                    Email = createViewModel.Name,
                    Mobile = createViewModel.Mobile,
                    Department = createViewModel.Department,
                };
                _employeeRepositry.AddEmployee(employee);
                return RedirectToAction("Details", new { id = employee.Id });
            }
            return View();
        }
    }
}