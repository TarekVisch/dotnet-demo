using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PlancherExpert.Models;

namespace PlancherExpert.Pages.Order
{
    public class InvoiceModel : PageModel
    {
		public OrderModel Order {  get; set; } = new OrderModel();
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
				string Id = Request.Query["id"];
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";

				using (SqlConnection connection = new SqlConnection(connectionString)) 
				{
					connection.Open();
					String sql = "SELECT * FROM Orders WHERE id=@Id;"; 

					using (SqlCommand cmd = new SqlCommand(sql, connection)) 
					{
						cmd.Parameters.AddWithValue("@Id", Id);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								Order.Id = reader.GetInt32(0);
								Order.ClientId= reader.GetInt32(1);
								Order.ProductId = reader.GetInt32(2);
								Order.Width= Convert.ToSingle(reader.GetDouble(3));
								Order.Height = Convert.ToSingle(reader.GetDouble(4));
								Order.CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
								Order.UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"));
							}
							else
							{
								Response.Redirect("/NotFound");
								return;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorMessage = "Something Went Wrong";
				Console.WriteLine("Exception: " + ex.ToString());
			}
		}
    }
}
