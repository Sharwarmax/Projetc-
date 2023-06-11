using Carsharing_Lombardi_Saturnio.IDAL;
using Carsharing_Lombardi_Saturnio.Models;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Carsharing_Lombardi_Saturnio.DAL
{
    public class OfferDAL : IOfferDAL
    {
        private string connectionString;
        public OfferDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }
        //Voir les offres du driver
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
                    while (reader.Read())
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
            if (offers_driver.Count == 0)
                return null;
            return offers_driver;
        }
		//Récupérer une seul offre
		public Offer GetOffer(int id)
        {
            Offer offer = new Offer();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT o.Date, o.Departure_Time, o.Id_Offer, o.NbPassengersMax, o.StartPoint, o.Destination, o.Price, o.Completed, o.NumKm, " +
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
                        offer.DepartureTime = reader.GetDateTime("Departure_Time");
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
                if (offer.Id_Offer == 0)
                    return null;
            }
            return offer;
        }
        //Voir plusieur offre
        public List<Offer> ViewOffers(User passenger)
        {
            List<Offer> offers_passenger = new List<Offer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Id_Offer,StartPoint, Destination FROM Saturnio_Lombardi.[dbo].[Offer]", connection);
                cmd.Parameters.AddWithValue("Id_User", passenger.Id);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Offer offer = new Offer();
                        offer.Id_Offer = reader.GetInt32("Id_Offer");
                        offer.StartPoint = reader.GetString("StartPoint");
                        offer.Destination = reader.GetString("Destination");
                        offers_passenger.Add(offer);
                    }
                }
            }
            return offers_passenger;
        }
        //Récupérer plusieurs offres
		public List<Offer> GetOffers()
		{
			List<Offer> offers = new List<Offer>();
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("SELECT o.Date, o.Id_Offer, o.NbPassengersMax, o.StartPoint, o.Destination, o.Price, o.Completed, o.NumKm" +
					"ou.Id_User, uf.First_name, uf.Last_name, uf.Phone_number, uf.Username, ou.Type " +
					"FROM Saturnio_Lombardi.[dbo].[Offer] o " +
					"INNER JOIN Saturnio_Lombardi.[dbo].[Users_Offers] ou " +
					"ON o.Id_Offer = ou.Id_Offer " +
					"INNER JOIN Saturnio_Lombardi.[dbo].[User] uf " +
					"ON ou.Id_User = uf.Id_User", connection);
				connection.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
                        Offer offer = new Offer();
						offer.Date = reader.GetDateTime("Date");
						offer.Id_Offer = reader.GetInt32("Id_Offer");
						offer.NbPassengerMax = reader.GetInt32("NbPassengersMax");
						offer.StartPoint = reader.GetString("StartPoint");
						offer.Destination = reader.GetString("Destination");
						offer.Price = Convert.ToSingle(reader.GetDouble("Price"));
						offer.Completed = reader.GetBoolean("Completed");
						offer.Numkm = Convert.ToSingle(reader.GetDouble("NumKm"));
						if (reader.GetString("Type") == "Driver")
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
                        offers.Add(offer);
					}
				}
			}
			return offers;
		}
        //Inserer une offre dans le passenger
        public void AddPassenger(Offer offer, User passenger)
        {
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
                SqlCommand cmd = new SqlCommand("INSERT INTO Saturnio_Lombardi.[dbo].[Users Offers]" +
                                                "(Id_Offer,Id_User,Type) VALUES (@Id_offer,@Id_User,@Type))", connection);
                cmd.Parameters.AddWithValue("@Id_offer", offer.Id_Offer);
				cmd.Parameters.AddWithValue("@Id_User", passenger.Id);
				cmd.Parameters.AddWithValue("@Type", "Passenger");
                connection.Open();
            }

        }
        public List<Offer> ViewAcceptedOffers(User passenger)
        {
            List<Offer> offers = new List<Offer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT o.Date, o.Id_Offer, o.NbPassengersMax, " +
                    "o.StartPoint, o.Destination, o.Price, o.Completed, o.NumKm, o.Departure_Time, " +
                    "ou.Id_User, uf.First_name, uf.Last_name, uf.Phone_number, uf.Username, ou.Type " +
                    "FROM Saturnio_Lombardi.[dbo].[Offer] o " +
                    "INNER JOIN Saturnio_Lombardi.[dbo].[Users_Offers] ou " +
                    "ON o.Id_Offer = ou.Id_Offer " +
                    "INNER JOIN Saturnio_Lombardi.[dbo].[User] uf " +
                    "ON ou.Id_User = uf.Id_User ", connection);

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Offer offer = new Offer();
                        offer.Date = reader.GetDateTime("Date");
                        offer.DepartureTime = reader.GetDateTime("Departure_Time");
                        offer.Id_Offer = reader.GetInt32("Id_Offer");
                        offer.NbPassengerMax = reader.GetInt32("NbPassengersMax");
                        offer.StartPoint = reader.GetString("StartPoint");
                        offer.Destination = reader.GetString("Destination");
                        offer.Price = Convert.ToSingle(reader.GetDouble("Price"));
                        offer.Completed = reader.GetBoolean("Completed");
                        offer.Numkm = Convert.ToSingle(reader.GetDouble("NumKm"));
                        if (reader.GetString("Type") == "Driver")
                        {
                            offer.Driver.Id = reader.GetInt32("Id_User");
                            offer.Driver.First_name = reader.GetString("First_name");
                            offer.Driver.Last_name = reader.GetString("Last_name");
                            offer.Driver.Phone_number = reader.GetInt32("Phone_number");
                            offer.Driver.Username = reader.GetString("Username");
                        }

                        if (reader.GetString("Type") == "Passenger")
                        {
                            passenger.Id = reader.GetInt32("Id_User");
                            passenger.First_name = reader.GetString("First_name");
                            passenger.Last_name = reader.GetString("Last_name");
                            passenger.Phone_number = reader.GetInt32("Phone_number");
                            passenger.Username = reader.GetString("Username");
                            offer.Passengers.Add(passenger);
                        }
                        offers.Add(offer);
                    }
                }
                if (offers.Count == 0)
                    return null;
            }
            return offers;
        }

        public bool RemoveOffer(Offer offer)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE o FROM Saturnio_Lombardi.dbo.[Offer] o " +
                    "INNER JOIN Saturnio_Lombardi.dbo.[Users_Offers] uo " +
                    "ON o.Id_Offer = uo.Id_Offer WHERE o.Id_Offer = @Id_Offer", connection);
                cmd.Parameters.AddWithValue("Id_Offer", offer.Id_Offer);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                result = res > 0;
            }
            return result;
        }

        public bool UpdateOffer(Offer offer)
        {
            bool result = false;
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Saturnio_Lombardi.dbo.[Offer] SET NumKm = @NumKm, Price = @Price, " +
                    "Destination = @Destination, StartPoint = @StartPoint, Date = @Date, Departure_Time = @Departure_Time " +
                    "WHERE Id_Offer = @Id_Offer", connection);
                cmd.Parameters.AddWithValue("Id_Offer", offer.Id_Offer);
                cmd.Parameters.AddWithValue("NumKm", offer.Numkm);
                cmd.Parameters.AddWithValue("Price", offer.Price);
                cmd.Parameters.AddWithValue("Destination", offer.Destination);
                cmd.Parameters.AddWithValue("StartPoint", offer.StartPoint);
                cmd.Parameters.AddWithValue("Date", offer.Date);
                cmd.Parameters.AddWithValue("Departure_Time", offer.DepartureTime.TimeOfDay);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                result = res > 0;
            }
            return result;
        }

        public bool InsertOffer(Offer offer)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Saturnio_Lombardi.dbo.[Offer] (NumKm, Price, NbPassengersMax, " +
                    "Completed, Id_User , Destination, Date, Departure_Time, StartPoint) VALUES(@NumKm, @Price, @NbPassengersMax, " +
                    "@Completed, @Id_User, @Destination, @Date, @Departure_Time, @StartPoint);" +
                    "INSERT INTO Saturnio_Lombardi.dbo.[Users_Offers] (Type, Id_Offer, Id_User) " +
                    "VALUES(@Type, ident_current('Saturnio_Lombardi.dbo.[Offer]'), @Id_User)", connection);
                cmd.Parameters.AddWithValue("NumKm", offer.Numkm);
                cmd.Parameters.AddWithValue("Price", offer.Price);
                cmd.Parameters.AddWithValue("NbPassengersMax", offer.NbPassengerMax);
                cmd.Parameters.AddWithValue("Completed", 0);
                cmd.Parameters.AddWithValue("Id_User", offer.Driver.Id);
                cmd.Parameters.AddWithValue("Destination", offer.Destination);
                cmd.Parameters.AddWithValue("StartPoint", offer.StartPoint);
                cmd.Parameters.AddWithValue("Date", offer.Date);
                cmd.Parameters.AddWithValue("Departure_Time", offer.DepartureTime.TimeOfDay);
                cmd.Parameters.AddWithValue("Type", "Driver");
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                result = res > 0;

            }
            return result;
        }

        public bool InsertOfferAndUser(Offer offer)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Saturnio_Lombardi.dbo.[Offer] (NumKm, Price, NbPassengersMax, " +
                    "Completed, Id_User , Destination, Date, Departure_Time, StartPoint) VALUES(@NumKm, @Price, @NbPassengersMax, " +
                    "@Completed, @Id_Driver, @Destination, @Date, @Departure_Time, @StartPoint);" +
                    "INSERT INTO Saturnio_Lombardi.dbo.[Users_Offers] (Type, Id_Offer, Id_User) " +
                    "VALUES(@TypeD, ident_current('Saturnio_Lombardi.dbo.[Offer]'), @Id_Driver) " +
                    "INSERT INTO Saturnio_Lombardi.dbo.[Users_Offers] (Type, Id_Offer, Id_User) " +
                    "VALUES(@TypeP, ident_current('Saturnio_Lombardi.dbo.[Offer]'), @Id_Passenger)", connection);
                cmd.Parameters.AddWithValue("NumKm", offer.Numkm);
                cmd.Parameters.AddWithValue("Price", offer.Price);
                cmd.Parameters.AddWithValue("NbPassengersMax", offer.NbPassengerMax);
                cmd.Parameters.AddWithValue("Completed", 0);
                cmd.Parameters.AddWithValue("Id_Driver", offer.Driver.Id);
                cmd.Parameters.AddWithValue("Id_Passenger", offer.Passengers[0].Id);
                cmd.Parameters.AddWithValue("Destination", offer.Destination);
                cmd.Parameters.AddWithValue("StartPoint", offer.StartPoint);
                cmd.Parameters.AddWithValue("Date", offer.Date);
                cmd.Parameters.AddWithValue("Departure_Time", offer.DepartureTime.TimeOfDay);
                cmd.Parameters.AddWithValue("TypeD", "Driver");
                cmd.Parameters.AddWithValue("TypeP", "Passenger");
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                result = res > 0;

            }
            return result;
        }

    }
}
