using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWebApp.Models
{
    public class SQLEmployeeRepository : IEmployeeReposiroty
    {
        private readonly AppDBContext context;
        public SQLEmployeeRepository(AppDBContext context)
        {
            this.context = context;
        }
        public Employee AddEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if (employee!=null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public Employee GetEmployee(int? Id)
        {
            return context.Employees.Find(Id);
        }

        public IEnumerable<Employee> GetEmployeeList()
        {
            return context.Employees;
        }

        public Employee Update(Employee changeEmployee)
        {
            var employee = context.Employees.Attach(changeEmployee);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return changeEmployee;
        }
    }
}
