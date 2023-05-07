﻿using Carsharing_Lombardi_Saturnio.IDAL;
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

        public int Id { get => id; set => id = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public int Phone_number { get => phone_number; set => phone_number = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

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

        }

        public void Register(IUserDAL _userDAL) => _userDAL.Register(this);

        public bool CheckUsername(IUserDAL _userDAL) => _userDAL.CheckUsername(this.Username);

        public void Login(IUserDAL _userDAL) => _userDAL.Login(this);

        public void ViewMyOffers() { }

        public void AddOffer(Offer offer) { }

        public void EditOffer(Offer offer) { }

        public void RemoveMyOffer() { }

        public void ContactPassenger() { }

        public void ContactDriver() { }

        public void AddRequest() { }
    }
}
