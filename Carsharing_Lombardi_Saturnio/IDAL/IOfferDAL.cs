using Carsharing_Lombardi_Saturnio.Models;

namespace Carsharing_Lombardi_Saturnio.IDAL
{
    public interface IOfferDAL
    {
        public List<Offer> ViewMyOffers(User driver);
        public Offer GetOffer(int id);
        public bool RemoveOffer(Offer offer);
        public bool UpdateOffer(Offer offer);
        public bool InsertOffer(Offer offer);

    }
}
