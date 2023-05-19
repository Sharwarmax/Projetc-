using Carsharing_Lombardi_Saturnio.Models;

namespace Carsharing_Lombardi_Saturnio.IDAL
{
    public interface IOfferDAL
    {
        public List<Offer> ViewMyOffers(User driver);
        public Offer GetOffer(int id);
		public List<Offer> ViewOffers(User passenger);
        public List<Offer> GetOffers();
        public void AddPassenger(Offer offer, User passenger);
        public List<Offer> ViewAcceptedOffers(User passenger);
	}
}
