using Microsoft.Data.SqlClient;

namespace PlancherExpert.Models
{
	public class ProductModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public float Installation { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public bool Create()
		{
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "INSERT INTO Products (name, price, installation, created_at, updated_at) VALUES (@Name, @Price, @Installation, GETDATE(), GETDATE())";

					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@Name", this.Name);
						cmd.Parameters.AddWithValue("@Price", this.Price);
						cmd.Parameters.AddWithValue("@Installation", this.Installation);
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

		public bool Update()
		{
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "UPDATE Products SET name = @Name, price = @Price, installation = @Installation, updated_at=GETDATE() WHERE id = @Id";

					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						Console.WriteLine(this.ToString());
						cmd.Parameters.AddWithValue("@Name", this.Name);
						cmd.Parameters.AddWithValue("@Price", this.Price);
						cmd.Parameters.AddWithValue("@Installation", this.Installation);
						cmd.Parameters.AddWithValue("@Id", this.Id);
						cmd.ExecuteNonQuery();
						Console.WriteLine("Updated");
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


		public override string ToString()
		{
			return $"Product({Id}, {Name}, {Price}, {Installation}, {CreatedAt}, {UpdatedAt})";
		}
	}
}
