using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // GET: Employees
        public IActionResult Index()
        {
            List<Employee> employeeList = [.. context.Employees];
            List<ViewEmployee> viewEmployeeList = [];

            foreach (Employee employee in employeeList)
            {
                viewEmployeeList.Add(new ViewEmployee(employee));
            }

            return View(viewEmployeeList);
        }

        // GET: Employees/Details
        public IActionResult Details(int? id)
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

        // POST: Employees/Details
        [HttpPost]
        public IActionResult Details(int id)
        {
            var employee = context.Employees.Find(id);

            if (employee != null)
            {
                return View(new ViewEmployee(employee));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
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

        // GET: Employees/Edit
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

        // POST: Employees/Edit
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

        // GET: Employees/Delete
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

            return View(new ViewEmployee(employee));
        }

        // POST: Employees/Delete
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
