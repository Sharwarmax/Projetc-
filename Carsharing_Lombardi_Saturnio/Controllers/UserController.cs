using Carsharing_Lombardi_Saturnio.Models;
using Carsharing_Lombardi_Saturnio.ViewModels;
using Carsharing_Lombardi_Saturnio.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Carsharing_Lombardi_Saturnio.DAL.IDAL;

namespace Carsharing_Lombardi_Saturnio.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDAL _userDAL;


        public UserController(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }
        public IActionResult Register()
        {
            User user = HttpContext.Session.Get<User>("CurrentUser");
            if (user != null)
                return RedirectToAction(nameof(Welcome));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                User user = new User(userVM);
                if (user.CheckUsername(_userDAL))
                {
                    user.Register(_userDAL);
                    return RedirectToAction(nameof(Login));
                }
            }
            return View();
        }


        public IActionResult Login()
        {
            User user = HttpContext.Session.Get<User>("CurrentUser");
            if (user != null)
                return RedirectToAction(nameof(Welcome));

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                User user = new User(userVM);
                if (!user.CheckUsername(_userDAL))
                {
                    if (user.Login(_userDAL) == true)
                    {
                        HttpContext.Session.Set("CurrentUser", user);
                        return RedirectToAction(nameof(Welcome));
                    }
                }
                TempData["FailureMessage"] = "The username or password is incorrect, try again!";
            }
            return View();
        }

        public IActionResult Welcome()
        {
            User user = HttpContext.Session.Get<User>("CurrentUser");
            if (user == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(Login));
            }

            return View(user);
        }
    }
}
