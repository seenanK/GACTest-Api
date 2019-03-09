using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using timesheet.model;
using timesheet.model.viewmodels;

namespace timesheet.business
{
    public interface IEmployeeService
    {
        IQueryable<Employee> GetEmployees();
        System.Threading.Tasks.Task<TimeSheet> AddEmployeeHours(TimeSheet timeSheet);
        IQueryable<TimeSheetOutputVM> GetEmployeeTimeSheet(EmployeeTimeSheetVM employeeTimesheet);
        IQueryable<Task> GetAllTasks();
    }
}
