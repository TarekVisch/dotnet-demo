using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PlancherExpert.Models;

namespace PlancherExpert.Pages.Product
{
    public class CreateModel : PageModel
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
		}

        public void OnPost() {
            Product.Name = Request.Form["name"];
            Product.Price = float.Parse(Request.Form["price"]);
			Product.Installation = float.Parse(Request.Form["installation"]);

			if (!Product.Create())
			{
				ErrorMessage = "Unable to create floor cover, try again later.";
				return;
			} else
			{
				Response.Redirect("/Product/List");
			}
		}
	}
}
