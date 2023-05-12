using Carsharing_Lombardi_Saturnio.IDAL;
using Carsharing_Lombardi_Saturnio.Models;
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
                    "INNER JOIN Saturnio_Lombardi.[dbo].[Users_Offers] ou ON o.Id_Offer = ou.Id_Offer WHERE o.Id_User = 1 AND ou.Type = 'Driver'", connection);
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

    }
}
