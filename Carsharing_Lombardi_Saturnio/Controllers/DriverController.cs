using Carsharing_Lombardi_Saturnio.Extensions;
using Carsharing_Lombardi_Saturnio.IDAL;
using Carsharing_Lombardi_Saturnio.Models;
using Carsharing_Lombardi_Saturnio.ViewModels;
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
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            Offer offer = Offer.GetOffer(id_offer,_offerDAL);
            offer.TotalPrice();
            return View(offer);
        }

        public IActionResult RemoveOffer(int id_offer)
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            Offer offer = Offer.GetOffer(id_offer, _offerDAL);
            if (offer.Driver.Id == driver.Id)
            {
                 if(offer.RemoveOffer(_offerDAL) == true)
                {
                    TempData["SuccessMessage"] = "The offer has been successfully removed !";
                    return RedirectToAction(nameof(UserController.Welcome), nameof(User));
                }
            }
            TempData["FailedMessage"] = "An error has occured while removing the offer, try again !";
            return RedirectToAction(nameof(UserController.Welcome), nameof(User));
        }

        public IActionResult EditOffer(int id_offer)
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            Offer offer = Offer.GetOffer(id_offer, _offerDAL);
            if (offer.Driver.Id == driver.Id && offer.Completed == false)
            {
                EditOfferViewModel editoffer = new(offer);
                return View(editoffer);
            }

            TempData["FailedMessage"] = "An error has occured while trying to access the update page, try again !";
            return RedirectToAction(nameof(UserController.Welcome), nameof(User));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOffer(EditOfferViewModel offer_form)
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            if(ModelState.IsValid == true)
            {
                Offer offer = new(offer_form);
                if (offer.UpdateOffer(_offerDAL) == true)
                {
                    TempData["SuccessMessage"] = "The offer has been successfully updated!";
                    return RedirectToAction(nameof(UserController.Welcome), nameof(User));
                }
            }
            TempData["FailureMessage"] = "An error has occured while updating the offer, try again!";
            return RedirectToAction(nameof(UserController.Welcome), nameof(User) );
        }

        public IActionResult InsertOffer()
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOffer(InsertOfferViewModel offer_form)
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            if (ModelState.IsValid == true)
            {
                Offer offer = new(offer_form);
                offer.Driver.Id = driver.Id;
                offer.Completed = false;
                if (offer.InsertOffer(_offerDAL) == true)
                {
                    TempData["SuccessMessage"] = "The offer has been successfully added!";
                    return RedirectToAction(nameof(UserController.Welcome), nameof(User));
                }
            }
            TempData["FailureMessage"] = "An error has occured while adding the offer, try again!";
            return RedirectToAction(nameof(UserController.Welcome), nameof(User));
        }
    }
}
