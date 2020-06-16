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
                new Employee(){Id=1,Name="Ripon",Email="ripon@gmail.com",Mobile="01812866508",Department=Dept.IT},
                new Employee(){Id=2,Name="Mithun",Email="mithun@gmail.com",Mobile="01812866509",Department=Dept.Textile},
                new Employee(){Id=3,Name="Plabon",Email="plabon@gmail.com",Mobile="01812866507",Department=Dept.IT}

            }; 
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.Id=_employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
           Employee employee= _employeeList.FirstOrDefault(x => x.Id == id);
            if (employee!=null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }

        public Employee GetEmployee(int? id)
        {
            return _employeeList.FirstOrDefault(i => i.Id == id);
        }

        public IEnumerable<Employee> GetEmployeeList()
        {
           return _employeeList;
        }

        public Employee Update(Employee changeEmployee)
        {
            Employee employee = _employeeList.FirstOrDefault(x => x.Id == changeEmployee.Id);
            if (employee != null)
            {
                employee.Name = changeEmployee.Name;
                employee.Mobile = changeEmployee.Mobile;
                employee.Email = changeEmployee.Email;
                employee.Department = changeEmployee.Department;
            }
            return employee;
        }
    }
}
