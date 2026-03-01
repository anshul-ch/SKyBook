using Microsoft.AspNetCore.Mvc;
using SkyBookApp.Models;

namespace SkyBookApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Implement actual authentication logic
                // For now, just redirect to home
                TempData["Success"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Implement actual user registration logic
                // For now, just redirect to login
                TempData["Success"] = "Account created successfully! Please login.";
                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // TODO: Implement actual logout logic
            TempData["Success"] = "Logged out successfully!";
            return RedirectToAction("Index", "Home");
        }
    }
}
