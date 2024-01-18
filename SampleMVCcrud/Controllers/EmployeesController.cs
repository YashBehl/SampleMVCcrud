using Microsoft.AspNetCore.Mvc;

namespace SampleMVCcrud.Controllers
{
    public class EmployeesController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}
