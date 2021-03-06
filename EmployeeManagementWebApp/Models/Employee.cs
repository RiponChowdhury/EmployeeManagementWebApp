﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWebApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage ="Max Lenght 50")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max Lenght 50")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage ="Invalid Email")]
        [Display(Name="Official Email")]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public Dept? Department { get; set; }
        [MaxLength(200)]
        public string PhotoPath { get; set; }


    }
}
