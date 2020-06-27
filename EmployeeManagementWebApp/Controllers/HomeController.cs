using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementWebApp.Models;
using EmployeeManagementWebApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

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
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepositry.GetEmployee(id);
            EmployeeEditViewModel employeeEditView = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Mobile = employee.Mobile,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditView);
        }
        [HttpPost]
        public IActionResult Edit( EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee exEmployee = _employeeRepositry.GetEmployee(model.Id);
                exEmployee.Name = model.Name;
                exEmployee.Email = model.Email;
                exEmployee.Department = model.Department;
                exEmployee.Mobile = model.Mobile;
                if (model.ExistingPhotoPath!=null)
                {
                  string FilePath=  Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                    System.IO.File.Delete(FilePath);
                }
                exEmployee.PhotoPath = ProcessUploadFile(model);
                _employeeRepositry.Update(exEmployee);
                return RedirectToAction("index", new { id = exEmployee.Id });
            }

            return View();
        }
        [HttpPost]
        public IActionResult Create( EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadFile(model);

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    Mobile = model.Mobile,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PhotoPath = uniqueFileName
                };

                _employeeRepositry.AddEmployee(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }

        private string ProcessUploadFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            // If the Photo property on the incoming model object is not null, then the user
            // has selected an image to upload.
            if (model.Photos != null && model.Photos.Count > 0)
            {
                foreach (IFormFile photo in model.Photos)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    using (var fileStream= new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
    }
}