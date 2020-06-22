using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWebApp.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 2,
                   Name = "Mithun Kar",
                   Email = "mithun.kar@gmail.com",
                   Mobile = "01515652129",
                   Department = Dept.Textile
               },
               new Employee
               {
                   Id = 3,
                   Name = "Rajesh Kumar Majumdar",
                   Email = "ponting15@gmail.com",
                   Mobile = "01922805565",
                   Department = Dept.IT
               }
               );
        }
    }
}
