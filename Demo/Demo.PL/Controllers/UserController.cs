using Demo.BLL.DTOs.Employees;
using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Employee;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    public class UserController : Controller
    {
        #region Services
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UserController(UserManager<ApplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            // Users
            var usersQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue)) // SearchValue = mariam
            {
                usersQuery = usersQuery.Where(U => U.Email.ToLower().Contains(SearchValue.ToLower()));
            }

            var usersList = await usersQuery.Select(U => new UserViewModel
            {
                Id = U.Id,
                FName = U.FName,
                LName = U.LName,
                Email = U.Email
            }).ToListAsync();

            foreach (var user in usersList)
            {
                user.Roles = await _userManager.GetRolesAsync(await _userManager.FindByIdAsync(user.Id));
            }

            return View(usersList);
        }
        #endregion
        #region Details

        [HttpGet]

        public async Task<IActionResult> Details(string? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var user = await _userManager.FindByIdAsync(id);

            if(user is null)
            {
                return NotFound();
            }

            var userVM = new UserViewModel()
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };

            return View(userVM);
        }

        #endregion


        #region Edit


        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
            {
                return NotFound(); //404

            }
            var userVM = new UserViewModel()
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userVM);



        }




        [HttpPost]
        // Show the Edit From
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string id, UserViewModel userVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userVM);
            }

            else
            {
                var message = string.Empty;

                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user is null)
                    {
                        return NotFound(); //404

                    }
                    user.FName = userVM.FName;
                    user.LName = userVM.LName;  
                    user.Email = userVM.Email;

                    var Result = await _userManager.UpdateAsync(user);


                    if (Result.Succeeded)
                    {
                        return View(nameof(Index));

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

                    message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Employee Cannot be updated";

                }
                return View(nameof(Index));


            }



        }


        #endregion


        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id is null)
            {
                return BadRequest(); //400
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
            {
                return NotFound(); //404

            }
            var userVM = new UserViewModel()
            {
                Id = user.Id,
                FName = user.FName,
                LName = user.LName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userVM);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var message = string.Empty;

            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is not null)
                {

                    await _userManager.DeleteAsync(user);
                    return View(nameof(Index));


                }


            }
            catch (Exception ex)
            {

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Error when deleting the Employee";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));



        }

       


        #endregion





    }
}
