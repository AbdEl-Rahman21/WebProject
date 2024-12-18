using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Context context;

        public EmployeeController()
        {
            context = new Context();
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Employee> employeeList = context.Employees.ToList();

            return View(employeeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ViewEmployee viewEmployee)
        {
            if (ModelState.IsValid)
            {
                Employee employee = viewEmployee.MapToEmployee();

                context.Employees.Add(employee);
                context.SaveChanges();

                return Index();
            }

            return View(viewEmployee);
        }
    }
}
