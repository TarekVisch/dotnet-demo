using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;

namespace PlancherExpert.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Role { get; set; } = "user";

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public bool DoesUserExist()
		{
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM Users WHERE username=@Username";
					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@Username", this.Username);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
								return true;
						}
					}
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
				return false;
			}
		}

		public bool VerifyUserCredentials()
		{
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM users WHERE username=@Username AND password=@Password";
					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@Username", Username);
						cmd.Parameters.AddWithValue("@Password", Password);

						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								this.Id = reader.GetInt32(0);
								this.Username = reader.GetString(1);
								this.Email = reader.GetString(2);
								this.Role = reader.GetString(4);
								return true;
							}
						}
					}
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
				return false;
			}
		}

		public bool CreateUser()
		{
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "INSERT INTO Users (username, email, password, created_at, updated_at) VALUES (@Username, @Email, @Password, GETDATE(), GETDATE());";

					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@Username", this.Username);
						cmd.Parameters.AddWithValue("@Email", this.Email);
						cmd.Parameters.AddWithValue("@Password", this.Password);
						cmd.ExecuteNonQuery();
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
				return false;
			}
		}
	}
}
