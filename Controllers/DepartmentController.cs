using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly Context context;

        public DepartmentController()
        {
            context = new Context();
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            List<Department> departmentList = await context.Departments.ToListAsync();
            List<ViewDepartment> viewDepartmentList = [];

            foreach (Department department in departmentList)
            {
                viewDepartmentList.Add(new ViewDepartment(department));
            }

            return View(viewDepartmentList);
        }

        // GET: Department/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(new ViewDepartment(department));
        }

        // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        public async Task<IActionResult> Create(ViewDepartment viewDepartment)
        {
            if (ModelState.IsValid)
            {
                await context.Departments.AddAsync(viewDepartment.MapToDepartment());

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewDepartment);
        }

        // GET: Department/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(new ViewDepartment(department));
        }

        // POST: Department/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ViewDepartment viewDepartment)
        {
            if (id != viewDepartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                context.Update(viewDepartment.MapToDepartment());

                await context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewDepartment);
        }

        // GET: Department/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await context.Departments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(new ViewDepartment(department));
        }

        // POST: Department/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await context.Departments.FindAsync(id);

            if (department != null)
            {
                context.Departments.Remove(department);

                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
