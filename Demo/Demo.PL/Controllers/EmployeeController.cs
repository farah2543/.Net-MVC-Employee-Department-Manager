using Demo.BLL.DTOs.Departments;
using Demo.BLL.DTOs.Employees;
using Demo.BLL.Services.Employees;
using Demo.BLL.Services.Employees;
//using Demo.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController :Controller
    {
        private readonly IEmployeeService _services;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _services = employeeService;

            _logger = logger;

            _webHostEnvironment = environment;

        }
        [HttpGet]
        public IActionResult Index() // Master action (Main page)
        {
            var Employees = _services.GetAllEmployees();
            return View(Employees);

        }

        [HttpGet]
        // Show the Creation From
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]
        // Show the Creation From
        public IActionResult Create(EmployeeToCreateDTO employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }

            else
            {
                var message = string.Empty;

                try
                {
                    var Result = _services.CreateEmployee(employeeDto);
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        message = "Employee cannot be created";
                        ModelState.AddModelError(string.Empty, message);
                        return View(employeeDto);
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    if (_webHostEnvironment.IsDevelopment())
                    {
                        message = ex.Message;
                        return View(employeeDto);
                    }
                    else
                    {
                        message = "Employee Cannot Be Created";
                        return View("Error", message);
                    }

                }

            }



        }


        [HttpGet]

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var Employee = _services.GetEmployeesById(id.Value);

            if (Employee is null)
            {
                return NotFound(); //404

            }

            return View(Employee);
        }


        [HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if (id is null)
        //    {
        //        return BadRequest(); //400
        //    }

        //    var Employee = _services.GetEmployeesById(id.Value);

        //    if (Employee is null)
        //    {
        //        return NotFound(); //404

        //    }
        //    return View(new EmployeeEditViewModel()
        //    {
        //        Code = Employee.Code,
        //        Name = Employee.Name,
        //        Description = Employee.Description,
        //        CreationDate = Employee.CreationDate,
        //    });



        //}

        //[HttpPost]
        //// Show the Edit From
        //public IActionResult Edit(int id, EmployeeEditViewModel EmployeeViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(EmployeeViewModel);
        //    }

        //    else
        //    {
        //        var message = string.Empty;

        //        try
        //        {
        //            var Result = _services.UpdateEmployee(new EmployeeToUpdateDTO()
        //            {
        //                Id = id,
        //                Code = EmployeeViewModel.Code,
        //                Name = EmployeeViewModel.Name,
        //                Description = EmployeeViewModel.Description,
        //                CreationDate = EmployeeViewModel.CreationDate,
        //            });
        //            if (Result > 0)
        //            {
        //                return RedirectToAction(nameof(Index));
        //            }
        //            else
        //            {
        //                message = "Employee cannot be Updated";
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, ex.Message);

        //            message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Employee Cannot be updated";

        //        }
        //        return View(EmployeeViewModel);

        //    }



        //}



        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var Employee = _services.GetEmployeesById(id.Value);

            if (Employee is null)
            {
                return NotFound(); //404

            }
            return View(Employee);


        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var Result = _services.DeleteEmployee(id);

                if (Result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Error when deleting the Employee";
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Error when deleting the Employee";

            }
            return View(nameof(Index));



        }

    }
}
