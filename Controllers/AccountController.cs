using Microsoft.AspNetCore.Identity;
using ProductManagementSystem.Models;
using ProductManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace ProductManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> signInManager; // Sign in user
        private UserManager<User> userManager; // Create user in registration process
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        //Get Method
        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(); // Return login view
        }

        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            ModelState.AddModelError("", "Failed to login");
            return View();
        }

        //Get method
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductManagementSystem.Models.User newuser = new ProductManagementSystem.Models.User();
                newuser.FirstName = registerViewModel.FirstName;
                newuser.LastName = registerViewModel.LastName;
                newuser.UserName = registerViewModel.UserName;
                newuser.Email = registerViewModel.Email;
                newuser.PhoneNumber = registerViewModel.PhoneNumber.ToString();

                // Creates new user in user table
                var result = await userManager.CreateAsync(newuser, registerViewModel.Password);
                if (result.Succeeded)
                {
                    if (newuser.UserName == "Admin")
                    {
                        await userManager.AddToRoleAsync(newuser, "Admin");
                        await userManager.AddToRoleAsync(newuser, "Customer");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(newuser, "Customer");
                    }
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerViewModel);
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
