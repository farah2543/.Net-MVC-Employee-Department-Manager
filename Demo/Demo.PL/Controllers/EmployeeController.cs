using Demo.BLL.DTOs.Departments;
using Demo.BLL.DTOs.Employees;
using Demo.BLL.Services.Employees;
using Demo.BLL.Services.Employees;
using Demo.DAL.Entities.Common.Enums;

//using Demo.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController :Controller
    {
        #region services
        private readonly IEmployeeService _services;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _services = employeeService;

            _logger = logger;

            _webHostEnvironment = environment;

        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index() // Master action (Main page)
        {
            var Employees = _services.GetAllEmployees();
            return View(Employees);

        }
        #endregion


        #region Create
        [HttpGet]
        // Show the Creation From
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

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
        #endregion


        #region Details

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

        #endregion


        #region Edit


        [HttpGet]
        public IActionResult Edit(int? id)
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
            return View(new EmployeeToUpdateDTO()
            {
                EmployeeType= Enum.TryParse<EmployeeType>(Employee.EmployeeType,out var EmpType) ? EmpType :default,
                Gender = Enum.TryParse<Gender>(Employee.Gender, out var gender) ? gender : default,
                Name = Employee.Name,
                Address = Employee.Address,
                Email = Employee.Email,
                Age = Employee.Age,
                ISActive = Employee.ISActive,
                PhoneNumber = Employee.PhoneNumber,
                HiringDate = Employee.HiringDate,
                Id= id.Value,   
                Salary= Employee.Salary,
               
            });



        }




        [HttpPost]
        // Show the Edit From
        [ValidateAntiForgeryToken]

        public IActionResult Edit(int id, EmployeeToUpdateDTO EmployeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(EmployeeDto);
            }

            else
            {
                var message = string.Empty;

                try
                {
                    var Result = _services.UpdateEmployee(EmployeeDto);
                 
                    if (Result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        message = "Employee cannot be Updated";
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);

                    message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Employee Cannot be updated";

                }
                return View(EmployeeDto);

            }



        }


        #endregion


        #region Delete

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
        [ValidateAntiForgeryToken]

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
        #endregion
    }
}
