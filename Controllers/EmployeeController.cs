using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                context.Employees.Add(viewEmployee.MapToEmployee());
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(viewEmployee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(new ViewEmployee(employee));
        }

        [HttpPost]
        public IActionResult Edit(int id, ViewEmployee viewEmployee)
        {
            if (id != viewEmployee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (viewEmployee.ProfilePicture != null)
                {
                    context.Employees.Update(viewEmployee.MapToEmployee());
                }
                else
                {
                    context.Employees
                        .Where(employee => employee.Id == viewEmployee.Id)
                        .ExecuteUpdate(setters => setters
                            .SetProperty(employee => employee.Name, viewEmployee.Name)
                            .SetProperty(employee => employee.PhoneNumber, viewEmployee.PhoneNumber)
                            .SetProperty(employee => employee.Age, viewEmployee.Age)
                        );
                }

                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(viewEmployee);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var employee = context.Employees.Find(id);

            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
