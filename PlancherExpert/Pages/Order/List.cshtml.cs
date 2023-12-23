using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlancherExpert.Pages.Order
{
    public class ListModel : PageModel
    {
        public void OnGet()
        {
			if (HttpContext.Session.GetString("userAuthenticated") != "true" || HttpContext.Session.GetString("userRole") == "user")
			{
				Response.Redirect("/Forbidden");
				return;
			}
		}
    }
}
