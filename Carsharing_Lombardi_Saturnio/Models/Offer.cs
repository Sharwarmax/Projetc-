﻿using Carsharing_Lombardi_Saturnio.IDAL;
namespace Carsharing_Lombardi_Saturnio.Models

{
    public class Offer : TravelDetails
    {
        private int id_offer;
        private float numkm;
        private float price;
        private int nbpassengersmax;
        private bool completed;
        private List<User> passengers;
        private List<User> driver;

        public int Id_Offer { get => id_offer; set => id_offer = value; }
        public float Numkm { get => numkm; set => numkm = value; }
        public float Price { get => price; set => price = value; }
        public int NbPassengerMax { get => nbpassengersmax; set => nbpassengersmax = value; }
        public bool Completed { get => completed; set => completed = value; }
        public List<User> Passengers { get => passengers; set => passengers = value; }
        public List<User> Driver { get => driver; set => driver = value; }

        public void GetOffers() { }

        public static Offer GetOffer(int id, IOfferDAL _offerDAL) => _offerDAL.GetOffer(id);

        public void ViewAcceptedOffers() { }

        public void InsertOffer() { }

        public void AddPassenger(User passenger) { }

        public void AddDriver(User driver) { }

        public void UpdateOffer() { }
    }
}
