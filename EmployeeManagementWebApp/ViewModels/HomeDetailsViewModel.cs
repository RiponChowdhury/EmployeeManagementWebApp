﻿using EmployeeManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWebApp.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Employee Employee { get; set; }
        public String PageTitle { get; set; }
    }
}
