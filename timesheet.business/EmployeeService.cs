using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using timesheet.data;
using timesheet.model;
using timesheet.model.viewmodels;

namespace timesheet.business
{
    public class EmployeeService : IEmployeeService
    {
        public TimesheetDb db { get; }
        public EmployeeService(TimesheetDb dbContext)
        {
            this.db = dbContext;
        }

        public IQueryable<Employee> GetEmployees()
        {
            var startDate = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek);
            var endDate = startDate.AddDays(7).AddSeconds(-1);
            //var data = this.db.Employees
            //    .Include(o => o.Timesheets)
            //    .Select(o => new EmployeeVM { id = o.Id, code = o.Code, name = o.Name,
            //        totalweeklyeffort = o.Timesheets.Where(d => d.Date >= startDate && d.Date <= endDate).Sum(s => s.Hours),
            //        averageweeklyeffort = o.Timesheets.Select(s => s.Hours).Average()
            //    }).ToList().AsQueryable();
            var data1 = this.db.Employees.AsQueryable();
            return data1;
        }

        public IQueryable<Task> GetAllTasks()
        {
            return this.db.Tasks;
        }

        public async System.Threading.Tasks.Task<TimeSheet> AddEmployeeHours(TimeSheet timeSheet)
        {
            db.TimeSheets.Add(timeSheet);
            await db.SaveChangesAsync();
            return timeSheet;
        }

        public IQueryable<TimeSheetOutputVM> GetEmployeeTimeSheet(EmployeeTimeSheetVM employeeTimesheet)
        {

            var startDate = employeeTimesheet.ScheduleDate.AddDays(-(int)employeeTimesheet.ScheduleDate.DayOfWeek);
            var endDate = startDate.AddDays(7).AddSeconds(-1);
            var empID = employeeTimesheet.EmployeeID;
            var data = db.TimeSheets
                        .Include(i => i.Task)
                        .Where(o => o.EmployeeID == empID && o.Date >= startDate && o.Date <= endDate)
                        .AsEnumerable()
                        .GroupBy(o => new { o.Date, o.Task })
                        .Select(g => new TimeSheetVM { Task = g.Key.Task.Name, Week =  g.Key.Date.ToString("dddd"), Hours = g.Sum(o => o.Hours) })
                        .ToList()
                        .AsQueryable();

            var pivotdata = data.GroupBy(p => p.Task)
            .Select(g => new 
            {
                TaskName = g.Key,
                Days = g.ToDictionary(item => item.Week, item => item.Hours)
            });

            var emptimesheet = new List<TimeSheetOutputVM>();
            foreach(var record in pivotdata)
            {
                var timesheetoutput = new TimeSheetOutputVM();
                timesheetoutput.task = record.TaskName;
                foreach(var day in record.Days)
                {
                    switch (day.Key)
                    {
                        case "Sunday":
                            timesheetoutput.Sunday = day.Value;
                            break;
                        case "Monday":
                            timesheetoutput.Monday = day.Value;
                            break;
                        case "Tuesday":
                            timesheetoutput.Tuesday = day.Value;
                            break;
                        case "Wednesday":
                            timesheetoutput.Wednesday = day.Value;
                            break;
                        case "Thursday":
                            timesheetoutput.Thursday = day.Value;
                            break;
                        case "Friday":
                            timesheetoutput.Friday = day.Value;
                            break;
                        case "Saturday":
                            timesheetoutput.Saturday = day.Value;
                            break;
                    }
                }
                emptimesheet.Add(timesheetoutput);
            }
            return emptimesheet.AsQueryable();
        }
    }
}
