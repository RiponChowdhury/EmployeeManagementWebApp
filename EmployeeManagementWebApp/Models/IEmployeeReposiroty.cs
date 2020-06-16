using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWebApp.Models
{
    public interface IEmployeeReposiroty
    {
        Employee GetEmployee(int? Id);
        IEnumerable<Employee> GetEmployeeList();
        Employee AddEmployee(Employee employee);
        Employee Update(Employee changeEmployee);
        Employee Delete(int id);
    }
}
