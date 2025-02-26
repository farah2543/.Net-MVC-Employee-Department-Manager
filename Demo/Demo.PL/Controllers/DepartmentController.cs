using Demo.BLL.DTOs;
using Demo.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentServices _services;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;   
        public DepartmentController(IDepartmentServices departmentServices, ILogger<DepartmentController> logger,IWebHostEnvironment environment)
        {
            _services = departmentServices;

            _logger = logger;

            _webHostEnvironment = environment;

        }
        [HttpGet]
        public IActionResult Index() // Master action (Main page)
        {
            var departments = _services.GetAllDepartments();
            return View(departments);
                
        }

        [HttpGet]
        // Show the Creation From
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]
        // Show the Creation From
        public IActionResult Create(DepartmentToCreateDTO departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }

            else
            {
                var message = string.Empty;

                try
                {
                    var Result = _services.CreateDepartment(departmentDto);
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    } 
                    else
                    {
                        message = "Department cannot be created";
                        ModelState.AddModelError(string.Empty, message);
                        return View(departmentDto);
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    if (_webHostEnvironment.IsDevelopment())
                    {
                        message = ex.Message;
                        return View(departmentDto);
                    }
                    else
                    {
                        message = "Department Cannot Be Created";
                        return View("Error",message);
                    }
                    
                }

            }

           

        }


    }
}
