using Microsoft.AspNetCore.Mvc;
 using Microsoft.AspNetCore.Authentication;
 using Microsoft.AspNetCore.Authentication.Cookies;
    using TaskMVC.Models;
using TaskMVC.Service.TaskMVC.Service;

namespace TaskMVC.Controllers
    {
        public class AccountController : Controller
        {
            private readonly AuthService _authService;

            public AccountController(AuthService authService)
            {
                _authService = authService;
            }

            public IActionResult Register()
            {
                return View();
            }




            [HttpPost]
            public async Task<IActionResult> Register(RegisterDto registerDto)
            {
                if (ModelState.IsValid)
                {
                    var newUser = await _authService.Register(registerDto);

                    if (newUser != null)
                    {
                        return RedirectToAction("Login");
                    }

                    ModelState.AddModelError(string.Empty, "Registration failed");
                }

                return View(registerDto);
            }

            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Login(LoginDto loginDto)
            {
                if (ModelState.IsValid)
                {
                    var token = await _authService.Login(loginDto);

                    if (!string.IsNullOrEmpty(token))
                    {
                        return RedirectToAction("Index", "Home"); // Redirect to the home page after successful login
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login credentials");
                }

                return View(loginDto);
            }

            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return RedirectToAction("Index", "Home"); // Redirect to the home page after logout
            }
        }
    }


