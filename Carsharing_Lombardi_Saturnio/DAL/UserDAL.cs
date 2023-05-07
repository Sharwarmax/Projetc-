using Carsharing_Lombardi_Saturnio.IDAL;
using Carsharing_Lombardi_Saturnio.Models;
using System.Data;
using System.Data.SqlClient;

namespace Carsharing_Lombardi_Saturnio.DAL
{
    public class UserDAL : IUserDAL
    {
        private string connectionString;
        public UserDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool CheckUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Username FROM Saturnio_Lombardi.[dbo].[User] WHERE Username = @Username", connection);
                cmd.Parameters.AddWithValue("Username", username);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return true;
                }
            }
            return false;
        }

        public bool Register(User user)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Saturnio_Lombardi.[dbo].[User] (First_name, Last_name, Phone_number, Username, Password) " +
                    "VALUES(@First_name, @Last_Name, @Phone_number, @Username, @Password)", connection);
                cmd.Parameters.AddWithValue("First_name", user.First_name);
                cmd.Parameters.AddWithValue("Last_name", user.Last_name);
                cmd.Parameters.AddWithValue("Phone_number", user.Phone_number);
                cmd.Parameters.AddWithValue("Username", user.Username);
                cmd.Parameters.AddWithValue("Password", user.Password);
                connection.Open();
                int res = cmd.ExecuteNonQuery();
                result = res > 0;
            }
            return result;
        }

        public User Login(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.[User] WHERE Username = @Username AND Password = @Password", connection);
                cmd.Parameters.AddWithValue("Username", user.Username);
                cmd.Parameters.AddWithValue("Password", user.Password);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.Id = reader.GetInt32("Id_User");
                        user.Phone_number = reader.GetInt32("Phone_number");
                        user.First_name = reader.GetString("First_name");
                        user.Last_name = reader.GetString("Last_name");
                        user.Password = "";
                    }
                }
            }
            return user;
        }
    }
}