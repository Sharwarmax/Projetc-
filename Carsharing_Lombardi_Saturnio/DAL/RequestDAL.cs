﻿using Carsharing_Lombardi_Saturnio.IDAL;
using Carsharing_Lombardi_Saturnio.Models;
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

        public List<Request> GetRequests()
        {
            List<Request> requests = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Saturnio_Lombardi.dbo.[Request]", connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requests = new List<Request>();
                        Request request = new Request();
                        request.Date = reader.GetDateTime("Date");
                        request.Id_Request = reader.GetInt32("Id_Request");
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
                        request.Id_Request = reader.GetInt32("Id_Request");
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
                cmd.Parameters.AddWithValue("Id_Request", request.Id_Request);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                result = res > 0;
            }
            return result;
        }

    }
}