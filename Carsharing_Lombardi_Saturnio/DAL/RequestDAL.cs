using Carsharing_Lombardi_Saturnio.DAL.IDAL;
using Carsharing_Lombardi_Saturnio.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Carsharing_Lombardi_Saturnio.DAL
{
    public class RequestDAL : IRequestDAL
    {
        private string connectionString;
        public RequestDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Request> GetRequests(User passenger)
        {
            List<Request> requests = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Saturnio_Lombardi.dbo.[Request] WHERE Date >= CAST(GETDATE() As Date) AND Id_User != @Id_User", connection);
                cmd.Parameters.AddWithValue("Id_User", passenger.Id);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requests = new List<Request>();
                        Request request = new Request();
                        request.Date = reader.GetDateTime("Date");
                        request.Id = reader.GetInt32("Id_Request");
                        request.StartPoint = reader.GetString("StartPoint");
                        request.Destination = reader.GetString("Destination");
                        request.DepartureTime = reader.GetDateTime("Departure_Time");
                        requests.Add(request);
                    }
                }
            }
            return requests;
        }
        public Request GetRequest(int Id_Request)
        {
            Request request;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT r.*, u.Username, u.First_name, u.Last_name " +
                    "FROM Saturnio_Lombardi.dbo.[Request] r " +
                    "INNER JOIN Saturnio_Lombardi.dbo.[User] u " +
                    "ON r.Id_User = u.Id_User" +
                    " WHERE Id_Request = @Id_Request", connection);
                cmd.Parameters.AddWithValue("Id_Request", Id_Request);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        request = new Request();
                        request.Date = reader.GetDateTime("Date");
                        request.Id = reader.GetInt32("Id_Request");
                        request.StartPoint = reader.GetString("StartPoint");
                        request.Destination = reader.GetString("Destination");
                        request.DepartureTime = reader.GetDateTime("Departure_Time");
                        request.User.Id = reader.GetInt32("Id_User");
                        request.User.Username = reader.GetString("Username");
                        request.User.First_name = reader.GetString("First_name");
                        request.User.Last_name = reader.GetString("Last_name");
                        return request;
                    }
                }
            }
            return null;
        }
        public bool RemoveRequest(Request request)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Saturnio_Lombardi.dbo.[Request] " +
                    "WHERE Id_Request = @Id_Request", connection);
                cmd.Parameters.AddWithValue("Id_Request", request.Id);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                result = res > 0;
            }
            return result;
        }
        public bool InsertRequest(Request request)
        {
			bool flag = false;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlCommand cmd = new SqlCommand("INSERT INTO Saturnio_Lombardi.dbo.[Request] (Id_User , Destination, Date, Departure_Time, StartPoint) " +
					"VALUES(@Id_User, @Destination, @Date, @Departure_Time, @StartPoint);", connection);
				cmd.Parameters.AddWithValue("Id_User", request.User.Id);
				cmd.Parameters.AddWithValue("Destination", request.Destination);
				cmd.Parameters.AddWithValue("StartPoint", request.StartPoint);
				cmd.Parameters.AddWithValue("Date", request.Date);
				cmd.Parameters.AddWithValue("Departure_Time", request.DepartureTime.TimeOfDay);
				connection.Open();
                int res = cmd.ExecuteNonQuery();
                flag = res > 0;
            }
			return flag;
        }
    }
}
