using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementWebApp.Models
{
    public interface IEmployeeReposiroty
    {
        Employee GetEmployee(int Id);
    }
}
