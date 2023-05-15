using Carsharing_Lombardi_Saturnio.IDAL;
using Carsharing_Lombardi_Saturnio.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Data.SqlClient;

namespace Carsharing_Lombardi_Saturnio.DAL
{
    public class OfferDAL : IOfferDAL
    {
        private string connectionString;
        public OfferDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public List<Offer> ViewMyOffers(User driver)
        {
            List<Offer> offers_driver = new List<Offer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT [Date],o.Id_Offer, NbPassengersMax, StartPoint, Destination, Price, Completed FROM Saturnio_Lombardi.[dbo].[Offer] o " +
                    "INNER JOIN Saturnio_Lombardi.[dbo].[Users_Offers] ou ON o.Id_Offer = ou.Id_Offer WHERE o.Id_User = @Id_User AND ou.Type = 'Driver'", connection);
                cmd.Parameters.AddWithValue("Id_User", driver.Id);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        Offer offer = new Offer();
                        offer.Date = reader.GetDateTime("Date");
                        offer.Id_Offer = reader.GetInt32("Id_Offer");
                        offer.NbPassengerMax = reader.GetInt32("NbPassengersMax");
                        offer.StartPoint = reader.GetString("StartPoint");
                        offer.Destination = reader.GetString("Destination");
                        offer.Price = Convert.ToSingle(reader.GetDouble("Price"));
                        offer.Completed = reader.GetBoolean("Completed");
                        offers_driver.Add(offer);
                    }
                }
            }
            return offers_driver;
        }

        public Offer GetOffer(int id)
        {
            Offer offer = new Offer();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT o.Date, o.Id_Offer, o.NbPassengersMax, o.StartPoint, o.Destination, o.Price, o.Completed, o.NumKm" +
                    "ou.Id_User, uf.First_name, uf.Last_name, uf.Phone_number, uf.Username, ou.Type " +
                    "FROM Saturnio_Lombardi.[dbo].[Offer] o " +
                    "INNER JOIN Saturnio_Lombardi.[dbo].[Users_Offers] ou " +
                    "ON o.Id_Offer = ou.Id_Offer " +
                    "INNER JOIN Saturnio_Lombardi.[dbo].[User] uf " +
                    "ON ou.Id_User = uf.Id_User WHERE ou.Id_Offer = @Id_Offer", connection);
                cmd.Parameters.AddWithValue("Id_Offer", id);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        offer.Date = reader.GetDateTime("Date");
                        offer.Id_Offer = reader.GetInt32("Id_Offer");
                        offer.NbPassengerMax = reader.GetInt32("NbPassengersMax");
                        offer.StartPoint = reader.GetString("StartPoint");
                        offer.Destination = reader.GetString("Destination");
                        offer.Price = Convert.ToSingle(reader.GetDouble("Price"));
                        offer.Completed = reader.GetBoolean("Completed");
                        offer.Numkm = Convert.ToSingle(reader.GetDouble("NumKm"));
                        if(reader.GetString("Type") == "Driver")
                        {
                            offer.Driver.Id = reader.GetInt32("Id_User");
                            offer.Driver.First_name = reader.GetString("First_name");
                            offer.Driver.Last_name = reader.GetString("Last_name");
                            offer.Driver.Phone_number = reader.GetInt32("Phone_number");
                            offer.Driver.Username = reader.GetString("Username");
                        }

                        if (reader.GetString("Type") == "Passenger")
                        {
                            User passenger = new User();
                            passenger.Id = reader.GetInt32("Id_User");
                            passenger.First_name = reader.GetString("First_name");
                            passenger.Last_name = reader.GetString("Last_name");
                            passenger.Phone_number = reader.GetInt32("Phone_number");
                            passenger.Username = reader.GetString("Username");
                            offer.Passengers.Add(passenger);
                        }
                        
                    }
                }
            }
            return offer;
        }

    }
}
