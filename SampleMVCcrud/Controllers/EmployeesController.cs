using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleMVCcrud.Data;
using SampleMVCcrud.Models;
using SampleMVCcrud.Models.Domain;

namespace SampleMVCcrud.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly SampleDbContext sampleDbContext;

        public EmployeesController(SampleDbContext sampleDbContext)
        {
            this.sampleDbContext = sampleDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await sampleDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Phone = addEmployeeRequest.Phone,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department
            };

            await sampleDbContext.Employees.AddAsync(employee);
            await sampleDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id) 
        {
            var employee = await sampleDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department
                };

                return await Task.Run(() => View("View", viewModel));
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await sampleDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Phone = model.Phone;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await sampleDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}