using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlancherExpert.Models;

namespace PlancherExpert.Pages
{
    public class RegisterModel : PageModel
    {
		public UserModel User { get; set; } = new UserModel();
		public string ErrorMessage { get; set; } = "";

		public void OnGet()
        {
        }

        public void OnPost() 
        { 
            User.Username = Request.Form["username"];
            User.Email = Request.Form["email"];
            User.Password = Request.Form["password"];
            string repassword = Request.Form["repassword"];

            if (User.Username == "" || User.Email == "" || User.Password == "" || repassword == "")
            {
                ErrorMessage = "Invalid form";
                return;
            }

            if (User.Password != repassword)
            {
				ErrorMessage = "Passwords don't match";
				return;
			}

			if (!User.CreateUser())
			{
				HttpContext.Session.SetString("userNotAdded", "User couldn't be created");
			}
			else
			{
				Response.Redirect("/Login");
			}
		}
    }
}
