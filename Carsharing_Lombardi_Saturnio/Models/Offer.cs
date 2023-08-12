using Carsharing_Lombardi_Saturnio.DAL;
using Carsharing_Lombardi_Saturnio.DAL.IDAL;
using Carsharing_Lombardi_Saturnio.ViewModels;
using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;
namespace Carsharing_Lombardi_Saturnio.Models

{
    public class Offer : TravelDetails
    {
        private float numkm;
        private float price;
        private int nbpassengersmax;
        private bool completed;
        private List<User> passengers = new List<User>();
        private User driver = new User();

        public float Numkm { get => numkm; set => numkm = value; }
        public float Price { get => price; set => price = value; }
        public int NbPassengerMax { get => nbpassengersmax; set => nbpassengersmax = value; }
        public bool Completed { get => completed; set => completed = value; }
        public List<User> Passengers { get => passengers; set => passengers = value; }
        public User Driver { get => driver; set => driver = value; }

        public Offer() { }

        public float TotalPrice() => price * numkm;

        public void GetOffers() { }
				
        public static Offer GetOffer(int id, IOfferDAL _offerDAL) => _offerDAL.GetOffer(id);
        public bool RemoveOffer(IOfferDAL _offerDAL) => _offerDAL.RemoveOffer(this);
        public bool UpdateOffer(IOfferDAL _offerDAL) => _offerDAL.UpdateOffer(this);
        public bool InsertOffer(IOfferDAL _offerDAL) => _offerDAL.InsertOffer(this);
        public bool InsertOfferAndUser(IOfferDAL _offerDAL) => _offerDAL.InsertOfferAndUser(this);

        public static List<Offer> ViewAcceptedOffers(IOfferDAL _offerDAL,User passenger) => _offerDAL.ViewAcceptedOffers(passenger);

        public void AddPassenger(User passenger, IOfferDAL _offerDAL) => _offerDAL.AddPassenger(this,passenger);
        public static List<Offer>  ViewMyOffers(IOfferDAL _offerDAL, User user) => _offerDAL.ViewMyOffers(user);

		public static List<Offer> ViewOffers(IOfferDAL _offerDAL, User user) => _offerDAL.ViewOffers(user);


	}
}
