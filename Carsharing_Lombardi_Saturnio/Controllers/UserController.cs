using Carsharing_Lombardi_Saturnio.IDAL;
using Carsharing_Lombardi_Saturnio.Models;
using Carsharing_Lombardi_Saturnio.ViewModels;
using Carsharing_Lombardi_Saturnio.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Carsharing_Lombardi_Saturnio.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserDAL _userDAL;


        public UserController(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register()
        {
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
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel userVM)
        {
            if(ModelState.IsValid)
            {
                User user = new User(userVM);
                if (!user.CheckUsername(_userDAL))
                {
                    user.Login(_userDAL);
                    if(user != null)
                    {
                        HttpContext.Session.Set("CurrentUser", user);
                        return RedirectToAction(nameof(Welcome));
                    }
                }
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
