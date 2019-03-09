using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timesheet.business;
using timesheet.model;
using timesheet.model.viewmodels;

namespace timesheet.api.controllers
{
    [Route("api/v1/timesheet")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public TimeSheetController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("getemployeetimesheet")]
        public IActionResult GetEmployeeTimesheet(EmployeeTimeSheetVM model)
        {
            var items = _employeeService.GetEmployeeTimeSheet(model);
            return new ObjectResult(items);
        }

        [HttpPost("addtimesheet")]
        public IActionResult AddTimesheet(TimeSheet model)
        {
            var items = _employeeService.AddEmployeeHours(model);
            return new ObjectResult(model);
        }

        [HttpGet("getalltasks")]
        public IActionResult GetAllTasks()
        {
            var items = _employeeService.GetAllTasks();
            return new ObjectResult(items);
        }        

    }
}
