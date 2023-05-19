using Carsharing_Lombardi_Saturnio.DAL;
using Carsharing_Lombardi_Saturnio.IDAL;
using Carsharing_Lombardi_Saturnio.ViewModels;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace Carsharing_Lombardi_Saturnio.Models

{
    public class Offer : TravelDetails
    {
        private int id_offer;
        private float numkm;
        private float price;
        private float totalprice;
        private int nbpassengersmax;
        private bool completed;
        private List<User> passengers = new List<User>();
        private User driver = new User();

        public int Id_Offer { get => id_offer; set => id_offer = value; }
        public float Numkm { get => numkm; set => numkm = value; }
        public float Price { get => price; set => price = value; }
        public float Totalprice { get => totalprice; }
        public int NbPassengerMax { get => nbpassengersmax; set => nbpassengersmax = value; }
        public bool Completed { get => completed; set => completed = value; }
        public List<User> Passengers { get => passengers; set => passengers = value; }
        public User Driver { get => driver; set => driver = value; }

        public Offer() { }

        public Offer(EditOfferViewModel editOffer)
        {
            Destination = editOffer.Destination;
            StartPoint = editOffer.StartPoint;
            Date = editOffer.Date;
            DepartureTime = editOffer.DepartureTime;
            Numkm = editOffer.Numkm;
            Price = editOffer.Price;
        }

        public Offer(InsertOfferViewModel insertoffer)
        {
            Numkm = insertoffer.Numkm;
            NbPassengerMax = insertoffer.NbPassengerMax;
            Price = insertoffer.Price;
            Destination = insertoffer.Destination;
            StartPoint = insertoffer.StartPoint;
            Date = insertoffer.Date;
            DepartureTime = insertoffer.DepartureTime;
        }

        public Offer(Request request, InsertOfferWithRequestViewModel request_offer)
        {
            Destination = request.Destination;
            StartPoint = request.StartPoint;
            Date = request.Date;
            DepartureTime= request.DepartureTime;
            Passengers.Add(request.User);
            Price = request_offer.Price;
            NbPassengerMax= request_offer.NbPassengerMax;
            Numkm= request_offer.Numkm;
        }

        public void TotalPrice() => totalprice = price * numkm;

        public void GetOffers() { }

        public static Offer GetOffer(int id, IOfferDAL _offerDAL) => _offerDAL.GetOffer(id);

        public bool RemoveOffer(IOfferDAL _offerDAL) => _offerDAL.RemoveOffer(this);
        public bool UpdateOffer(IOfferDAL _offerDAL) => _offerDAL.UpdateOffer(this);

        public void ViewAcceptedOffers() { }

        public bool InsertOffer(IOfferDAL _offerDAL) => _offerDAL.InsertOffer(this);
        public bool InsertOfferAndUser(IOfferDAL _offerDAL) => _offerDAL.InsertOfferAndUser(this);


        public void AddPassenger(User passenger) { }

        public void AddDriver(User driver) { }

    }
}
