using Carsharing_Lombardi_Saturnio.DAL.IDAL;
using Carsharing_Lombardi_Saturnio.Extensions;
using Carsharing_Lombardi_Saturnio.Models;
using Carsharing_Lombardi_Saturnio.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Carsharing_Lombardi_Saturnio.Controllers
{
    public class DriverController : Controller
    {
        private readonly IUserDAL _userDAL;
        private readonly IOfferDAL _offerDAL;
        private readonly IRequestDAL _requestDAL;


        public DriverController(IOfferDAL offerDAL, IRequestDAL requestDAL, IUserDAL userDAL)
        {
            _offerDAL = offerDAL;
            _requestDAL = requestDAL;
            _userDAL = userDAL;
        }

        public IActionResult ViewMyOffers()
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            driver.Offers_Driver = Offer.ViewMyOffers(_offerDAL, driver);
            return View(driver.Offers_Driver);
        }

        public IActionResult OfferDetails(int id_offer)
        {
            float total_price = 0f;
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            
            Offer offer = Offer.GetOffer(id_offer,_offerDAL);
            if (offer == null)
            {
                TempData["FailureMessage"] = "An error has occured !";
                return RedirectToAction(nameof(UserController.Welcome), nameof(User));
            }
            ViewData["TotalPrice"] = offer.TotalPrice();
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
            if (offer.Driver.Id == driver.Id && offer.Passengers.Count() == 0)
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
            if (offer.Driver.Id == driver.Id && offer.Completed == false && offer.Passengers.Count() == 0)
            {
                EditOfferViewModel editoffer = new();
                TempData["CurrentID_Offer"] = offer.Id;
                editoffer.Numkm = offer.Numkm;
                editoffer.Price = offer.Price;
                editoffer.Destination = offer.Destination;
                editoffer.StartPoint = offer.StartPoint;
                editoffer.Date = offer.Date;
                editoffer.DepartureTime = offer.DepartureTime;
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
                Offer offer = new();
                offer.Destination = offer_form.Destination;
                offer.StartPoint = offer_form.StartPoint;
                offer.Date = offer_form.Date;
                offer.DepartureTime = offer_form.DepartureTime;
                offer.Numkm = offer_form.Numkm;
                offer.Price = offer_form.Price;
                offer.Id = (int)TempData["CurrentID_Offer"];
                if (offer.UpdateOffer(_offerDAL) == true && offer.Passengers.Count() == 0)
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
                Offer offer = new();
                offer.Numkm = offer_form.Numkm;
                offer.NbPassengerMax = offer_form.NbPassengerMax;
                offer.Price = offer_form.Price;
                offer.Destination = offer_form.Destination;
                offer.StartPoint = offer_form.StartPoint;
                offer.Date = offer_form.Date;
                offer.DepartureTime = offer_form.DepartureTime;
                offer.Driver.Id = driver.Id;
                offer.Completed = false;
                if (offer.InsertOffer(_offerDAL) == true)
                {
                    TempData["SuccessMessage"] = "The offer has been successfully added!";
                    driver.Offers_Driver = Offer.ViewMyOffers(_offerDAL, driver);
                    HttpContext.Session.Set<User>("CurrentUser", driver);
                    return RedirectToAction(nameof(UserController.Welcome), nameof(User));
                }
            }
            TempData["FailureMessage"] = "An error has occured while adding the offer, try again!";
            return RedirectToAction(nameof(UserController.Welcome), nameof(User));
        }

        public IActionResult ViewRequests()
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }

            List<Request> requests = Models.Request.GetRequests(_requestDAL, driver);
            return View(requests);
        }

        public IActionResult RequestDetails(int id_request)
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            Request request = Models.Request.GetRequest(id_request,_requestDAL);
            if (request == null)
            {
                TempData["FailureMessage"] = "An error has occured !";
                return RedirectToAction(nameof(UserController.Welcome), nameof(User));
            }
            return View(request);
        }

        public IActionResult InsertOfferWithRequest(int id_request)
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            Request request = Models.Request.GetRequest(id_request, _requestDAL);
            if (request == null)
            {
                TempData["FailureMessage"] = "An error has occured !";
                return RedirectToAction(nameof(UserController.Welcome), nameof(User));
            }
            HttpContext.Session.Set("CurrentRequest", request);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertOfferWithRequest(InsertOfferWithRequestViewModel offer_form)
        {
            User driver = HttpContext.Session.Get<User>("CurrentUser");
            if (driver == null)
            {
                TempData["NotConnected"] = "Please log into your account.";
                return RedirectToAction(nameof(UserController.Login), nameof(User));
            }
            if (ModelState.IsValid == true)
            {
                Request request = HttpContext.Session.Get<Request>("CurrentRequest");
                Offer offer = new();
                offer.Destination = request.Destination;
                offer.StartPoint = request.StartPoint;
                offer.Date = request.Date;
                offer.DepartureTime = request.DepartureTime;
                offer.AddPassenger(request.User);
                offer.Price = offer_form.Price;
                offer.NbPassengerMax = offer_form.NbPassengerMax;
                offer.Numkm = offer_form.Numkm;
                offer.Driver.Id = driver.Id;
                if(offer.NbPassengerMax == 1)
                    offer.Completed = true;
                else
                offer.Completed = false;
                if (offer.InsertOfferAndUser(_offerDAL) == true && request.RemoveRequest(_requestDAL) == true)
                {
                    TempData["SuccessMessage"] = "The offer has been successfully added!";
                    driver.Offers_Driver = Offer.ViewMyOffers(_offerDAL, driver);
                    HttpContext.Session.Set<User>("CurrentUser", driver);
                    return RedirectToAction(nameof(UserController.Welcome), nameof(User));
                }
            }
            TempData["FailureMessage"] = "An error has occured while adding the offer, try again!";
            return RedirectToAction(nameof(UserController.Welcome), nameof(User));
        }
    }
}
