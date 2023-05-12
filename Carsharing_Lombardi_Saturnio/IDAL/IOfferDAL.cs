using Carsharing_Lombardi_Saturnio.Models;

namespace Carsharing_Lombardi_Saturnio.IDAL
{
    public interface IOfferDAL
    {
        public List<Offer> ViewMyOffers(User driver);
    }
}
