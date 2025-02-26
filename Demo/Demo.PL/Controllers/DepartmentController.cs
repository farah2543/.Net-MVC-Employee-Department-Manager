using Demo.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _services;
        public DepartmentController(IDepartmentServices departmentServices)
        {
            _services = departmentServices;
            
        }
        public IActionResult Index() // Master action (Main page)
        {
            var departments = _services.GetAllDepartments();
            return View(departments);
                
        }
    }
}
