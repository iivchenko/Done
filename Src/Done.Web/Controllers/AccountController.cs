using System.Threading.Tasks;
using Done.Web.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Done.Web.Controllers
{
    public sealed  class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
 
        public AccountController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return BackHome();
            }

            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return BackHome();
            }
            
            if (ModelState.IsValid)
            {
                var result = 
                    await _signInManager.PasswordSignInAsync(model.User, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return BackHome();
                }
                else
                {
                    ModelState.AddModelError("", "Login or Password is invalid!");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return BackHome();
        }

        private IActionResult BackHome()
        {
            return RedirectToAction("Index", "Goals");
        }
    }
}