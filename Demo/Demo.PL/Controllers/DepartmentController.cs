using Demo.BLL.DTOs;
using Demo.BLL.DTOs.Departments;
using Demo.BLL.Services.Departments;
using Demo.PL.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {

        #region Services
        private readonly IDepartmentServices _services;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;   
        public DepartmentController(IDepartmentServices departmentServices, ILogger<DepartmentController> logger,IWebHostEnvironment environment)
        {
            _services = departmentServices;

            _logger = logger;

            _webHostEnvironment = environment;

        }

        #endregion

        #region Index
        [HttpGet]
        public async Task <IActionResult >Index() // Master action (Main page)
        {
            ViewData["Message"] = "Hello from the ViewData";
             ViewBag.Message = "Hello from the ViewBag";
            //ViewBag.Message = new { Message = "Hello from viewBag" , Id =1  };
            var departments =await  _services.GetAllDepartmentsAsync();
            return View(departments);
                
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
        public async Task<IActionResult> Create(DepartmentViewModel departmentViewModel)
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
                    var Result = await _services.CreateDepartmentAsync(new DepartmentToCreateDTO()
                    {
                        Code = departmentViewModel.Code,
                        Name = departmentViewModel.Name,
                        Description = departmentViewModel.Description,
                        CreationDate =departmentViewModel.CreationDate,

                    });
                    if (Result > 0)
                    {
                        TempData["Message"] = "Department is Created successfully";
                    } 
                    else
                    {
                        message = "Department cannot be created";
                        TempData["Message"] = message; 
                        ModelState.AddModelError(string.Empty, message);
                    }
                    return RedirectToAction(nameof(Index));


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    if (_webHostEnvironment.IsDevelopment())
                    {
                        message = ex.Message;
                        return View(departmentViewModel);
                    }
                    else
                    {
                        message = "Department Cannot Be Created";
                        return View("Error",message);
                    }
                    
                }

            }

           

        }

        #endregion



        #region Details
        [HttpGet]

        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if(id is null)
            {
                return BadRequest(); //400
            }

            var department = await _services.GetDepartmentsByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound(); //404
                
            }

            return View(department);
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

            var department = await _services.GetDepartmentsByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound(); //404

            }
            return View(new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            });



        }
        [HttpPost]
        // Show the Edit From
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id ,DepartmentViewModel departmentViewModel)
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
                    var Result = await _services.UpdateDepartmentAsync(new DepartmentToUpdateDTO()
                    {
                        Id = id,
                        Code = departmentViewModel.Code,
                        Name = departmentViewModel.Name,
                        Description = departmentViewModel.Description,
                        CreationDate = departmentViewModel.CreationDate,
                    });
                    if (Result > 0)
                    {
                        TempData["Message"] = "Department Updated successfully";
                    }
                    else
                    {
                        message = "Department cannot be Updated";
                        TempData["Message"] = message;
                        ModelState.AddModelError(string.Empty, message);

                    }
                    return RedirectToAction(nameof(Index));


                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);

                    message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Department Cannot be updated";

                }
                return View(departmentViewModel);

            }



        }
        #endregion



        #region Delete
        [HttpGet]
        public async Task< IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var department = await _services.GetDepartmentsByIdAsync(id.Value);

            if (department is null)
            {
                return NotFound(); //404

            }
                return View(department);
          

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteAsync(int id)
        {  
            var message = string.Empty;

            try
            {
                var Result =await _services.DeleteDepartmentAsync(id);
                   
                if (Result)
                {
                    TempData["Message"] = "Department Deleted successfully";
                }
                else
                {
                    message = "Department cannot be Deleted";
                    TempData["Message"] = message;
                    ModelState.AddModelError(string.Empty, message);

                }
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Error when deleting the Department";

            }
            return View(nameof(Index));


           
        }
        #endregion 

    }
}
