namespace Carsharing_Lombardi_Saturnio.Models
{
    public class Offer : TravelDetails
    {
        private int id_offer;
        private float numkm;
        private float price;
        private int nbpassengersmax;
        private bool completed;

        public int Id_Offer { get => id_offer; set => id_offer = value; }
        public float Numkm { get => numkm; set => numkm = value; }
        public float Price { get => price; set => price = value; }
        public int NbPassengerMax { get => nbpassengersmax; set => nbpassengersmax = value; }
        public bool Completed { get => completed; set => completed = value; }

        public void GetOffers() { }

        public void GetOffer(int id) { }

        public void ViewAcceptedOffers() { }

        public void InsertOffer() { }

        public void AddPassenger(User passenger) { }

        public void AddDriver(User driver) { }

        public void UpdateOffer() { }
    }
}
