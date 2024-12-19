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
        public async Task<IActionResult> Index()
        {
            List<Employee> employeeList = await context.Employees.ToListAsync();
            List<ViewEmployee> viewEmployeeList = [];

            foreach (Employee employee in employeeList)
            {
                viewEmployeeList.Add(new ViewEmployee(employee));
            }

            return View(viewEmployeeList);
        }

        // GET: Employees/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(new ViewEmployee(employee));
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public async Task<IActionResult> Create(ViewEmployee viewEmployee)
        {
            if (ModelState.IsValid)
            {
                await context.Employees.AddAsync(await viewEmployee.MapToEmployeeAsync());

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewEmployee);
        }

        // GET: Employees/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(new ViewEmployee(employee));
        }

        // POST: Employees/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ViewEmployee viewEmployee)
        {
            if (id != viewEmployee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (viewEmployee.ProfilePicture != null)
                {
                    context.Employees.Update(await viewEmployee.MapToEmployeeAsync());
                }
                else
                {
                    await context.Employees
                        .Where(employee => employee.Id == viewEmployee.Id)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(employee => employee.Name, viewEmployee.Name)
                            .SetProperty(employee => employee.PhoneNumber, viewEmployee.PhoneNumber)
                            .SetProperty(employee => employee.Age, viewEmployee.Age)
                        );
                }

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewEmployee);
        }

        // GET: Employees/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(new ViewEmployee(employee));
        }

        // POST: Employees/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await context.Employees.FindAsync(id);

            if (employee != null)
            {
                context.Employees.Remove(employee);

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
