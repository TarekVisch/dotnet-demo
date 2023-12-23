using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PlancherExpert.Models;

namespace PlancherExpert.Pages.Product
{
	public class EditModel : PageModel
	{
		public ProductModel Product { get; set; } = new ProductModel();
		public string ErrorMessage { get; set; } = "";

		public void OnGet()
		{

			if (HttpContext.Session.GetString("userAuthenticated") != "true" || HttpContext.Session.GetString("userRole") == "user")
			{
				Response.Redirect("/Forbidden");
				return;
			}

			try
			{
				string Id = Request.Query["id"];
				string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";

				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					String sql = "SELECT * FROM Products WHERE id=@Id;"; 

					using (SqlCommand cmd = new SqlCommand(sql, connection)) 
					{
						cmd.Parameters.AddWithValue("@Id", Id);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								Product.Id = reader.GetInt32(0);
								Product.Name = reader.GetString(1);
								Product.Price = Convert.ToSingle(reader.GetDouble(2));
								Product.Installation = Convert.ToSingle(reader.GetDouble(3));
								Product.CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
								Product.UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"));
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
		
		public void OnPost()
		{
			Product.Id = Convert.ToInt32(Request.Query["id"]);
			Product.Name = Request.Form["name"];
			Product.Price = float.Parse(Request.Form["price"]);
			Product.Installation = float.Parse(Request.Form["installation"]);

			if (!Product.Update())
			{
				ErrorMessage = "Unable to update floor cover, try again later.";
				return;
			} else
			{
				Response.Redirect("/Product/List");
			}
		}
	}
}
