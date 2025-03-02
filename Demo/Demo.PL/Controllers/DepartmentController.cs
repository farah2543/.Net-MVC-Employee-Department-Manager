using Demo.BLL.DTOs;
using Demo.BLL.Services.Departments;
using Demo.PL.ViewModels.Department;
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


        [HttpGet]

        public IActionResult Details(int? id)
        {
            if(id is null)
            {
                return BadRequest(); //400
            }

            var department = _services.GetDepartmentsById(id.Value);

            if (department is null)
            {
                return NotFound(); //404
                
            }

            return View(department);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var department = _services.GetDepartmentsById(id.Value);

            if (department is null)
            {
                return NotFound(); //404

            }
            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            });



        }

        [HttpPost]
        // Show the Edit From
        public IActionResult Edit(int id ,DepartmentEditViewModel departmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentViewModel);
            }

            else
            {
                var message = string.Empty;

                try
                {
                    var Result = _services.UpdateDepartment(new DepartmentToUpdateDTO()
                    {
                        Id = id,
                        Code = departmentViewModel.Code,
                        Name = departmentViewModel.Name,
                        Description = departmentViewModel.Description,
                        CreationDate = departmentViewModel.CreationDate,
                    });
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        message = "Department cannot be Updated";
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);

                    message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Department Cannot be updated";

                }
                return View(departmentViewModel);

            }



        }



        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var department = _services.GetDepartmentsById(id.Value);

            if (department is null)
            {
                return NotFound(); //404

            }
                return View(department);
          

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {  
            var message = string.Empty;

            try
            {
                var Result = _services.DeleteDepartment(id);
                   
                if (Result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Error when deleting the Department";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Error when deleting the Department";

            }
            return View(nameof(Index));

            

        }

    }
}
