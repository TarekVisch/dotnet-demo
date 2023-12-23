using Microsoft.Data.SqlClient;

namespace PlancherExpert.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public int ProductId { get; set; }
		public float Width { get; set; }
		public float Height { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public int Create()
		{
			try
			{
				if (ClientId == 0 || ProductId == 0 || Width == 0 || Height == 0)
				{
					Console.WriteLine("Error: One or more required properties are 0.");
					return -1; 
				}

				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "INSERT INTO Orders (client_id, product_id, width, height, created_at, updated_at) OUTPUT INSERTED.ID VALUES (@ClientId, @ProductId, @Width, @Height, GETDATE(), GETDATE())";

					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						cmd.Parameters.AddWithValue("@ClientId", this.ClientId);
						cmd.Parameters.AddWithValue("@ProductId", this.ProductId);
						cmd.Parameters.AddWithValue("@Width", this.Width);
						cmd.Parameters.AddWithValue("@Height", this.Height);

						int insertedId = (int)cmd.ExecuteScalar();

						return insertedId;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
				return -1;
			}
		}
	}
}
