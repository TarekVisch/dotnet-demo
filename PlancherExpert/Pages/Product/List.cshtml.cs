using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PlancherExpert.Models;

namespace PlancherExpert.Pages.Product
{
	public class ListModel : PageModel
	{
		public List<ProductModel> products = new List<ProductModel>();
		public void OnGet()
		{
			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM product;";
					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								ProductModel product = new ProductModel();
								product.Id = reader.GetString(0);
								product.Name = reader.GetString(1);
								product.Price = Convert.ToSingle(reader.GetDouble(2));
								product.Installation = Convert.ToSingle(reader.GetDouble(3));
								product.CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")); ;
								product.UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at")); ;

								products.Add(product);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}
	}
}
