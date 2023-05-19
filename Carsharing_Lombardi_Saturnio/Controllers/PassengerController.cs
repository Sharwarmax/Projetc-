using System;
using Carsharing_Lombardi_Saturnio.DAL;
using Carsharing_Lombardi_Saturnio.Extensions;
using Carsharing_Lombardi_Saturnio.IDAL;
using Carsharing_Lombardi_Saturnio.Models;
using Microsoft.AspNetCore.Mvc;

namespace Carsharing_Lombardi_Saturnio.Controllers
{
    public class PassengerController : Controller
    {
        private readonly IOfferDAL _offerDAL;

        public PassengerController(IOfferDAL offerDAL)
        {
			_offerDAL = offerDAL;
        }
        public IActionResult ViewOffers()
        {
            User passenger = HttpContext.Session.Get<User>("CurrentUser");
            if (passenger == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            passenger.Offers_Passengers = passenger.ViewOffers(_offerDAL);
            return View(passenger);
        }
		public IActionResult ViewDetails(int id_offer)
		{
			User passenger = HttpContext.Session.Get<User>("CurrentUser");
			if (passenger == null)
			{
				TempData["NotConnected"] = "Please log into your account.";
				return RedirectToAction(nameof(UserController.Login), nameof(User));
			}
			Offer offer = Offer.GetOffer(id_offer, _offerDAL);
			HttpContext.Session.Set("currentOffer", offer);
			return View(offer);
		}
		public IActionResult ConfirmOffer()
		{
			User passenger = HttpContext.Session.Get<User>("CurrentUser");
			Offer offer = HttpContext.Session.Get<Offer>("currentOffer");
			if (passenger == null)
			{
				TempData["NotConnected"] = "Please log into your account.";
				return RedirectToAction(nameof(UserController.Login), nameof(User));
			}
			offer.AddPassenger(passenger, _offerDAL);
			TempData["SuccessMessage"] = "You have successfully accepted the offer";
            return RedirectToAction("ViewOffers"); 
        }
		public IActionResult ViewAcceptedOffer()
        {
			User passenger = HttpContext.Session.Get<User>("CurrentUser");
			if (passenger == null)
			{
				TempData["NotConnected"] = "Please log into your account.";
				return RedirectToAction(nameof(UserController.Login), nameof(User));
			}
			List<Offer> offer = _offerDAL.ViewAcceptedOffers(passenger);
			return View(offer);
        }
        public IActionResult AcceptedOffer()
        {
			User passenger = HttpContext.Session.Get<User>("CurrentUser");
			if (passenger == null)
			{
				TempData["NotConnected"] = "Please log into your account.";
				return RedirectToAction(nameof(UserController.Login), nameof(User));
			}
			return View();
        }
    }
}
