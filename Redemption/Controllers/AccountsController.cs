using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redemption.Interfaces;
using Redemption.Models;

namespace Redemption.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAuthService _authService;
        public AccountsController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.RegisterAsync(model);
                if (response.IsValid)
                {
                    return RedirectToAction("Profile", "Accounts");
                }
                else if (response.ModelErrors is not null)
                {
                    foreach (var error in response.ModelErrors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неизвестная ошибка!");
                }
            }
            ModelState.AddModelError(string.Empty, "Не все поля введены");
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new AuthVM { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(AuthVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.LoginAsync(model);
                if (response.IsValid)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    if (User.IsInRole("admin"))
                        return RedirectToAction("Profile", "Accounts");
                    return RedirectToAction("Index", "Redemption");
                }
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            return View(model);
        }
        public IActionResult Profile()
        {
            if (User.IsInRole("admin"))
            {
                return View();
            }
            return BadRequest();
        }
        public IActionResult GetUsers()
        {
            var result = _authService.GetUsers();
            return PartialView(result);
        }
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login", "Accounts");
        }







    }
}
