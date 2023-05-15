using Carsharing_Lombardi_Saturnio.Extensions;
using Carsharing_Lombardi_Saturnio.IDAL;
using Carsharing_Lombardi_Saturnio.Models;
using Microsoft.AspNetCore.Mvc;

namespace Carsharing_Lombardi_Saturnio.Controllers
{
    public class DriverController : Controller
    {
        private readonly IOfferDAL _offerDAL;


        public DriverController(IOfferDAL offerDAL)
        {
            _offerDAL = offerDAL;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewMyOffers()
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            driver.Offers_Driver = driver.ViewMyOffers(_offerDAL);
            return View(driver);
        }

        public IActionResult OfferDetails(int id_offer)
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(User), nameof(UserController.Login));
            }
            Offer offer = Offer.GetOffer(id_offer,_offerDAL);
            /*if(offer == null)
                return View(null);

            offer.Driver.Id = driver.Id;
            offer.Driver.Username = driver.Username;
            offer.Driver.First_name= driver.First_name;
            offer.Driver.Last_name= driver.Last_name;
            offer.Driver.Phone_number= driver.Phone_number;*/
            return View(offer);
        }
    }
}
