using Azure.Identity;
using Demo.DAL.Entities.Identity;
using Demo.PL.ViewModels.Identitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Demo.DAL.Entities.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Demo.BLL.Common.Services.EmailSettings;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _singInManager;
        private readonly IEmailSettings _emailSettings;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> singInManager, IEmailSettings emailSettings)
        {
            _userManager = userManager;
            _singInManager = singInManager;
            _emailSettings = emailSettings;
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
        [HttpGet]

        public IActionResult ForgetPassword()
        {
            return View();

        }


        [HttpPost]
        public async Task<IActionResult> SendRestPasswordUrl(ForgetPasswordViewModel forgetPasswordVM)
        {

            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(forgetPasswordVM.Email);

                if (User is not null)
                {
                    var Token = await _userManager.GeneratePasswordResetTokenAsync(User);

                    var url = Url.Action("ResetPassword", "Account", new { email = forgetPasswordVM.Email, token = Token }, Request.Scheme);

                    var email = new DAL.Entities.Identity.Email()
                    {
                        TO = forgetPasswordVM.Email,
                        Subject = "Reset Your Password",
                        Body = url
                    };

                    _emailSettings.SendMail(email);
                    return RedirectToAction(nameof(CheckInbox));




                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid operation please try again");
                }


            }
            return View();

        }

        [HttpGet]

        public IActionResult CheckInbox()
        {
            return View();

        }


        [HttpGet]

        public IActionResult ResetPassword(string Email, string Token)
        {
            TempData["Email"] = Email;
            TempData["Token"] = Token;

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordVM)
        {

            if (ModelState.IsValid)
            {
                var email = TempData["Email"] as string;
                var token = TempData["Token"] as string;

                var user = await _userManager.FindByEmailAsync(email);

                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordVM.Password);
                    if (result.Succeeded)
                    {
                        return View(nameof(Login));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Opearation was not successful please try again later");
                        return View(resetPasswordVM);

                    }

                }


            }
            ModelState.AddModelError(string.Empty, "Invalid operation please try again");

            return View(resetPasswordVM);

        }

    }

}
