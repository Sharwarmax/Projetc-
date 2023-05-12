﻿using Carsharing_Lombardi_Saturnio.Extensions;
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
                return RedirectToAction(nameof(User), nameof(UserController.Login));
            }
            List<Offer> offers_driver = driver.ViewMyOffers(_offerDAL);
            return View(offers_driver);
        }
    }
}
