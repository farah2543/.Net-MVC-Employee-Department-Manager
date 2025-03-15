using AutoMapper;
using Demo.BLL.DTOs.Departments;
using Demo.BLL.DTOs.Employees;
using Demo.BLL.Services.Departments;
using Demo.BLL.Services.Employees;
using Demo.BLL.Services.Employees;
using Demo.DAL.Entities.Common.Enums;
using Demo.DAL.Entities.Employees;
using Demo.PL.ViewModels.Employee;



//using Demo.PL.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class EmployeeController :Controller
    {
        #region services
        private readonly IEmployeeService _services;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmployeeController(IEmployeeService employeeService,IMapper mapper ,ILogger<EmployeeController> logger, IWebHostEnvironment environment/*,IDepartmentServices departmentServices*/)
        {
            _services = employeeService;
            _mapper = mapper;
            _logger = logger;

            _webHostEnvironment = environment;


        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue) // Master action (Main page)
        {
            var Employees =await _services.GetAllEmployeesAsync(SearchValue);
            return View(Employees);

        }
        #endregion


        #region Create
        [HttpGet]
        // Show the Creation From
        public IActionResult Create(/*[FromServices] IDepartmentServices departmentServices*/)
        { 
            //Send departments from Create Action to Create view 
            //ViewData["Departments"] = departmentServices.GetAllDepartmentsAsync();

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // Show the Creation From
        public async Task<IActionResult> CreateAsync(EmployeeViewModel employeeDto)
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
                    var employeeToCreate = _mapper.Map<EmployeeViewModel, EmployeeToCreateDTO>(employeeDto);

                    var Result = await _services.CreateEmployeeAsync(employeeToCreate);
                    if (Result > 0)
                    {
                        TempData["Message"] = "Employee is Created successfully";

                    }
                    else
                    {
                        message = "Employee cannot be created";
                        TempData["Message"] = message;
                        ModelState.AddModelError(string.Empty, message);
                    }
                    return View(nameof(Index));

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

        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var Employee = await _services.GetEmployeesByIdAsync(id.Value);

            if (Employee is null)
            {
                return NotFound(); //404

            }

            return View(Employee);
        }

        #endregion


        #region Edit


        [HttpGet]
        public async Task<IActionResult> EditAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var Employee = await _services.GetEmployeesByIdAsync(id.Value);

            if (Employee is null)
            {
                return NotFound(); //404

            }
            var employeeToEdit = _mapper.Map<EmployeeDetailsToReturnDTO, EmployeeViewModel>(Employee);
            return View(employeeToEdit);



        }




        [HttpPost]
        // Show the Edit From
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EditAsync(int id, EmployeeViewModel EmployeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(EmployeeVM);
            }

            else
            {
                var message = string.Empty;

                try
                {
                    var employeeToUpdate = _mapper.Map<EmployeeViewModel, EmployeeToUpdateDTO>(EmployeeVM);
                    var Result = await _services.UpdateEmployeeAsync(employeeToUpdate);
                  
                 
                    if (Result > 0)
                    {
                        TempData["Message"] = "Employee is Updated successfully";

                    }
                    else
                    {
                        message = "Employee cannot be Updated";
                        TempData["Message"] = message;
                        ModelState.AddModelError(string.Empty, message);
                    }
                    return View(nameof(Index));


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);

                    message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Employee Cannot be updated";

                }
                return View(nameof(Index));


            }



        }


        #endregion


        #region Delete

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var Employee = await _services.GetEmployeesByIdAsync(id.Value);

            if (Employee is null)
            {
                return NotFound(); //404

            }
            return View(Employee);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var message = string.Empty;

            try
            {
                var Result = await _services.DeleteEmployeeAsync(id);

                if (Result)
                {
                    TempData["Message"] = "Employee Deleted Successfully";
                }
                else
                {
                    message = "Employee cannot be created";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);
                }
                return View(nameof(Index));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Error when deleting the Employee";

            }
            return View(nameof(Index));



        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var employee = await _services.GetEmployeesByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            // Define the path where images are stored
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/images", employee.Image);

            // Check if the image exists and delete it
            if (!string.IsNullOrEmpty(employee.Image) && System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            var employeeToEdit = _mapper.Map<EmployeeDetailsToReturnDTO, EmployeeViewModel>(employee);

            employeeToEdit.Image = null;

            var employeeToUpdate = _mapper.Map<EmployeeViewModel, EmployeeToUpdateDTO>(employeeToEdit);

            await _services.UpdateEmployeeAsync(employeeToUpdate);

            return View(nameof(Index));
        }



        #endregion



    }
}
