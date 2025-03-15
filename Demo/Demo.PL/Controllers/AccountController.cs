using Azure.Identity;
using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Identitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> singInManager)
        {
			_userManager = userManager;
            _singInManager = singInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{
			if (ModelState.IsValid)
			{
				var User = new ApplicationUser()
				{
					UserName = registerViewModel.Email.Split('@')[0],
					Email = registerViewModel.Email,
					FName = registerViewModel.FName,
					LName = registerViewModel.LName,
					IsAgree = registerViewModel.IsAgree
				};

				var result = await _userManager.CreateAsync(User, registerViewModel.Password);
				if (result.Succeeded)
				{
					RedirectToAction("Login");	
				}
				else
				{
					foreach(var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}

			}

			return View(registerViewModel); //ModelState is not valid or result not succeeded
		}


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if(User is not null)
                {
                    bool flag = await _userManager.CheckPasswordAsync(User, loginViewModel.Password);

                    if (flag)
                    {
                        var result = await _singInManager
                            .PasswordSignInAsync(User, loginViewModel.Password, loginViewModel.RememberMe, false);//Creates the Token;
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index","Home");


                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Password is incorrect");

                    }

                }
              
                else
                {
                    
                  ModelState.AddModelError(string.Empty, "Email is not found");
                    
                }

            }

            return View(loginViewModel); //ModelState is not valid or result not succeeded
        }


        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
            await _singInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }

}
