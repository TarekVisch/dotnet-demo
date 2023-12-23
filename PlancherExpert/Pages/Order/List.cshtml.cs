using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PlancherExpert.Models;

namespace PlancherExpert.Pages.Order
{
    public class ListModel : PageModel
    {
        public List<OrderModel> Orders { get; set; } = new List<OrderModel>();
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
                string connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PlancherExpert;Integrated Security=True;TrustServerCertificate=true;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Orders;";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrderModel Order = new OrderModel();

                                Order.Id = reader.GetInt32(0);
                                Order.ClientId = reader.GetInt32(1);
                                Order.ProductId = reader.GetInt32(2);
                                Order.Width = Convert.ToSingle(reader.GetDouble(3));
                                Order.Height = Convert.ToSingle(reader.GetDouble(4));
                                Order.CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
                                Order.UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"));

                                Orders.Add(Order);
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
