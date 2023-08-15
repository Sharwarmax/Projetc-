using Carsharing_Lombardi_Saturnio.DAL.IDAL;
using Carsharing_Lombardi_Saturnio.ViewModels;

namespace Carsharing_Lombardi_Saturnio.Models
{
    public class User
    {
        private int id;
        private string first_name;
        private string last_name;
        private int phone_number;
        private string username;
        private string password;
        private List<Offer> offers_driver = new();
        private List<Offer> offers_passengers = new();


        public int Id { get => id; set => id = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public int Phone_number { get => phone_number; set => phone_number = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public List<Offer> Offers_Driver { get => offers_driver; set => offers_driver = value; }
        public List<Offer> Offers_Passengers { get => offers_passengers; set => offers_passengers = value; }



        public User() { }

        public bool Register(IUserDAL _userDAL) => _userDAL.Register(this);

        public bool CheckUsername(IUserDAL _userDAL) => _userDAL.CheckUsername(this.Username);

        public bool Login(IUserDAL _userDAL, IOfferDAL _offerDAL)
        {
			if (_userDAL.Login(this))
            {
                this.Offers_Driver = Offer.ViewMyOffers(_offerDAL, this);
                this.Offers_Passengers = Offer.ViewAcceptedOffers(_offerDAL, this);
                return true;
            }
            return false;

        }
 
    }
}
