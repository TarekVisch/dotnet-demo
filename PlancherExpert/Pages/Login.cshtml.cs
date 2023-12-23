using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using PlancherExpert.Models;

namespace PlancherExpert.Pages
{
	public class LoginModel : PageModel
	{
		public UserModel User { get; set; } = new UserModel();
		public string ErrorMessage { get; set; } = "";

		public void OnGet()
		{
			HttpContext.Session.Remove("userNotExist");
			HttpContext.Session.Remove("userPasswordError");
		}

		public void OnPost()
		{
			User.Username = Request.Form["username"];
			User.Password = Request.Form["password"];

			if (User.Username == "" || User.Password == "")
			{
				ErrorMessage = "Invalid username/password.";
				return;
			}

			if (!User.DoesUserExist())
			{
				HttpContext.Session.SetString("userNotExist", "User doesn't exist.");
				return;
			}

			if (!User.VerifyUserCredentials())
			{
				HttpContext.Session.SetString("userPasswordError", "Invalid Password.");
				return;
			} else
			{
				HttpContext.Session.SetString("userAuthenticated", "true");
				HttpContext.Session.SetString("userId", Convert.ToString(User.Id));
				HttpContext.Session.SetString("username",Convert.ToString(User.Username));
				HttpContext.Session.SetString("userEmail", Convert.ToString(User.Email));
				HttpContext.Session.SetString("userRole", Convert.ToString(User.Role));

				Response.Redirect("/Index");
			}
		}
	}
}
