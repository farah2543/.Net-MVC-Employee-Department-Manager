using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Roles;
using Demo.PL.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        #region Services
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment,UserManager<ApplicationUser>userManager)
        {
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }
        #endregion

        #region Index


        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            // Users
            var rolesQuery = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValue)) // SearchValue = mariam
            {
                rolesQuery = rolesQuery.Where(R => R.Name.ToLower().Contains(SearchValue.ToLower()));
            }

            var rolesList = await rolesQuery.Select(R => new RoleViewModel
            {
                Id = R.Id,
                Name = R.Name,
                
               
            }).ToListAsync();


            return View(rolesList);
        }
        #endregion


        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = roleViewModel.Name
                });

                return View(nameof(Index));
            }

            return View(roleViewModel);
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

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
            {
                return NotFound();
            }

            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
            
            };

            return View(roleVM);
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

            var role = await _roleManager.FindByIdAsync(id);
            var users =  await _userManager.Users.ToListAsync();  

            if (role is null)
            {
                return NotFound(); //404

            }
            var roleVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                Users = users.Select(user => new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result

                }).ToList()
              
            };
            return View(roleVM);



        }




        [HttpPost]
        // Show the Edit From
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(string id, RoleViewModel roleVM)
        {
            if (!ModelState.IsValid)
            {
                return View(roleVM);
            }

            else
            {
                var message = string.Empty;

                try
                {
                    var role = await _roleManager.FindByIdAsync(id);
                    if (role is null)
                    {
                        return NotFound(); //404

                    }
                    role.Name = roleVM.Name;

                    var Result = await _roleManager.UpdateAsync(role);

                    foreach (var userRole in roleVM.Users)
                    {
                        var user = await _userManager.FindByIdAsync(userRole.UserId);
                        if (user is not null)
                        {
                            if (userRole.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name))) // Sa7
                                await _userManager.AddToRoleAsync(user, role.Name);
                            else if (!userRole.IsSelected && await _userManager.IsInRoleAsync(user, role.Name)) // Checkbox empty
                                await _userManager.RemoveFromRoleAsync(user, role.Name);
                        }
                    }



                    if (Result.Succeeded)
                    {
                        return View(nameof(Index));

                    }
                    else
                    {
                        message = "role cannot be Updated";
                        TempData["Message"] = message;
                        ModelState.AddModelError(string.Empty, message);
                    }
                    return View(nameof(Index));


                }
                catch (Exception ex)
                {

                    message = _webHostEnvironment.IsDevelopment() ? ex.Message : "role Cannot be updated";

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

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null)
            {
                return NotFound(); //404

            }
            var userVM = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
            
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
                var user = await _roleManager.FindByIdAsync(id);
                if (user is not null)
                {

                    await _roleManager.DeleteAsync(user);
                    return View(nameof(Index));


                }


            }
            catch (Exception ex)
            {

                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Error when deleting the role";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(nameof(Index));



        }




        #endregion



    }
}
