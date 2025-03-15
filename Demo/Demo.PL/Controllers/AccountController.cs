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

		public AccountController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
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

	}

}
