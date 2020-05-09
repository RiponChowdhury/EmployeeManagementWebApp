using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWebApp.Models
{
    public class MockEmployeeRepositroy:IEmployeeReposiroty
    {
        private List<Employee> _employeeList;
        public MockEmployeeRepositroy()
        {
            _employeeList = new List<Employee>
            {
                new Employee(){Id=1,Name="Ripon",Email="ripon@gmail.com",Mobile="01812866508",Department="IT"},
                new Employee(){Id=2,Name="Mithun",Email="mithun@gmail.com",Mobile="01812866509",Department="Textile"},
                new Employee(){Id=3,Name="Plabon",Email="plabon@gmail.com",Mobile="01812866507",Department="IT"}

            }; 
        }
        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(i => i.Id == id);
        }
    }
}
