using System;
using Carsharing_Lombardi_Saturnio.DAL;
using Carsharing_Lombardi_Saturnio.DAL.IDAL;
using Carsharing_Lombardi_Saturnio.Extensions;
using Carsharing_Lombardi_Saturnio.Models;
using Carsharing_Lombardi_Saturnio.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Carsharing_Lombardi_Saturnio.Controllers
{
    public class PassengerController : Controller
    {
		private readonly IUserDAL _userDAL;
        private readonly IOfferDAL _offerDAL;
		private readonly IRequestDAL _requestDAL;


        public PassengerController(IOfferDAL offerDAL, IRequestDAL requestDAL, IUserDAL userDAL)
        {
			_offerDAL = offerDAL;
			_requestDAL = requestDAL;
			_userDAL = userDAL;
        }

        public IActionResult ViewOffers()
        {
            User passenger = HttpContext.Session.Get<User>("CurrentUser");
            if (passenger == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
			List<Offer> offers = Offer.ViewOffers(_offerDAL, passenger);
            return View(offers);
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
			ViewData["TotalPrice"]=offer.TotalPrice();
			HttpContext.Session.Set("currentOffer", offer);
			return View(offer);
		}

        public IActionResult ViewAcceptedOfferDetails(int id_offer)
        {
            User passenger = HttpContext.Session.Get<User>("CurrentUser");
            if (passenger == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            Offer offer = Offer.GetOffer(id_offer, _offerDAL);
            ViewData["TotalPrice"] = offer.TotalPrice();
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
			if(offer.Passengers.Count() +1 == offer.NbPassengerMax)
			{
                offer.Completed = true;
				offer.UpdateOffer(_offerDAL);
            }

            if (offer.AddPassenger(passenger, _offerDAL))
                TempData["Message"] = "You have successfully accepted the offer";
            else
			TempData["Message"] = "You have successfully accepted the offer";
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
			passenger.Offers_Passengers = Offer.ViewAcceptedOffers(_offerDAL, passenger);
			return View(passenger.Offers_Passengers);
        }
    
		public IActionResult RequestOffer()
		{
			User passenger = HttpContext.Session.Get<User>("CurrentUser");
			if (passenger == null)
			{
				TempData["NotConnected"] = "Please log into your account.";
				return RedirectToAction(nameof(UserController.Login), nameof(User));
			}
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult RequestOffer(InsertRequestViewModel requestoffer)
		{
			User passenger = HttpContext.Session.Get<User>("CurrentUser");
			if (passenger == null)
			{
				TempData["NotConnected"] = "Please log into your account.";
				return RedirectToAction(nameof(UserController.Login), nameof(User));
			}
			if (ModelState.IsValid == true)
			{
				Request request = new();
				request.User = passenger;
				request.DepartureTime = requestoffer.DepartureTime;
				request.Date = requestoffer.Date;
				request.Destination = requestoffer.Destination;
				request.StartPoint = requestoffer.StartPoint;
				if (request.InsertRequest(_requestDAL) == true)
				{
					TempData["SuccessMessage"] = "The request has been successfully added!";
					return RedirectToAction(nameof(UserController.Welcome), nameof(User));
				}
			}
			TempData["FailureMessage"] = "An error has occured while adding the request, try again!";
			return View();
		}
	}
}
