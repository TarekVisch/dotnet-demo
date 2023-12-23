using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PlancherExpert.Models;

namespace PlancherExpert.Pages.Order
{
    public class CreateModel : PageModel
    {
		public List<ProductModel> Products = new List<ProductModel>();
		public OrderModel Order { get; set; } = new OrderModel();
		public string ErrorMessage { get; set; } = "";

		public void OnGet()
		{
			if (HttpContext.Session.GetString("userAuthenticated") != "true")
			{
				Response.Redirect("/Login");
				return;
			}

			try
			{
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM products;";
					using (SqlCommand cmd = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								ProductModel product = new ProductModel();
								product.Id = reader.GetInt32(0);
								product.Name = reader.GetString(1);
								product.Price = Convert.ToSingle(reader.GetDouble(2));
								product.Installation = Convert.ToSingle(reader.GetDouble(3));
								product.CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
								product.UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"));

								Products.Add(product);
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

		public void OnPost()
		{
			Order.ClientId = Convert.ToInt32(HttpContext.Session.GetString("userId"));
			Order.ProductId = Convert.ToInt32(Request.Form["floorcover"]);
			Order.Width = float.Parse(Request.Form["width"]);
			Order.Height = float.Parse(Request.Form["height"]);

			int insertedRowId = Order.Create();

			if (insertedRowId == -1)
			{
				ErrorMessage = "Unable to create order, try again later.";
				return;
			} else
			{
				Console.WriteLine(insertedRowId);
				Response.Redirect($"/Order/Invoice?id={insertedRowId}");
			}
		}
	}
}
