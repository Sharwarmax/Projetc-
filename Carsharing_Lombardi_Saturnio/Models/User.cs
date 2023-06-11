using Carsharing_Lombardi_Saturnio.IDAL;
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
        private List<Offer> offers_driver;
        private List<Offer> offers_passengers;


        public int Id { get => id; set => id = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public int Phone_number { get => phone_number; set => phone_number = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public List<Offer> Offers_Driver { get => offers_driver; set => offers_driver = value; }
        public List<Offer> Offers_Passengers { get => offers_passengers; set => offers_passengers = value; }

        public User() { }
        public User(RegisterViewModel vm)
        {
            First_name = vm.First_Name;
            Last_name = vm.Last_Name;
            Phone_number = vm.Phone_number;
            Username = vm.Username;
            Password = vm.Password;
        }

        public User(LoginViewModel vm)
        {
            Username= vm.Username;
            Password= vm.Password;
        }

        public void Register(IUserDAL _userDAL) => _userDAL.Register(this);

        public bool CheckUsername(IUserDAL _userDAL) => _userDAL.CheckUsername(this.Username);

        public bool Login(IUserDAL _userDAL) => _userDAL.Login(this);

        public List<Offer> ViewMyOffers(IOfferDAL _offerDAL) => _offerDAL.ViewMyOffers(this);

				public List<Offer> ViewOffers(IOfferDAL _offerDAL) => _offerDAL.ViewOffers(this);
 
    }
}
