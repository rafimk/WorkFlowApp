using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using Work_Flow_App.Models;
using Work_Flow_App.Services.Interfaces;

namespace Work_Flow_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly SignInManager<User> _signInManager;
        public AccountController(IIdentityService identityService, SignInManager<User> signInManager)
        {
            _identityService = identityService;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserModel user)
        {
            var isAdmin = false;

            if (user.IsAdmin == "A")
            {
                isAdmin = true;
            }

            await _identityService.RegisterAsync(user.Username, user.Pwd, isAdmin);
            ViewBag.message = "The user " + user.Username + " is saved successfully";
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var result = await _identityService.LoginAsync(userModel.UserName, userModel.Password);

            if (result.Errors == null)
            {
                {
                    var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, result.UserName));
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                        new ClaimsPrincipal(identity));
                    return RedirectToAction(nameof(RequestController.Index), "Request");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
