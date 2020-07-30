using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UdemyTutorial.Model.ViewModel;

namespace UdemyTutorial.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        public async Task<IActionResult> Registration(RegistrationVModel registrationVModel)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }

            var user = new IdentityUser
            {
                UserName = registrationVModel.Email,
                Email = registrationVModel.Email,
                PasswordHash = registrationVModel.Password
            };

            var success = await _userManager.CreateAsync(user, registrationVModel.Password);
            if(success.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Index");
            }

            foreach(var error in success.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Registration");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginVModel loginVModel)
        {
            if(!ModelState.IsValid)
            {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(loginVModel.Email);
            if(user==null)
            {
                return NotFound();
            }

            var success = await _signInManager.PasswordSignInAsync(user, loginVModel.Password, true, false);

            if(success.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}