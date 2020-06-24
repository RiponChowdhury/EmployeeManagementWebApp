﻿using System;
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
        [HttpPost]
        public IActionResult Create([FromForm] EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                // If the Photo property on the incoming model object is not null, then the user
                // has selected an image to upload.
                if (model.Photo != null)
                {
                    // The image must be uploaded to the images folder in wwwroot
                    // To get the path of the wwwroot folder we are using the inject
                    // HostingEnvironment service provided by ASP.NET Core
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    // To make sure the file name is unique we are appending a new
                    // GUID value and and an underscore to the file name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    // Use CopyTo() method provided by IFormFile interface to
                    // copy the file to wwwroot/images folder
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

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
    }
}