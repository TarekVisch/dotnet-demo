using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlancherExpert.Pages.Order
{
    public class InvoiceModel : PageModel
    {
        public void OnGet()
        {
			if (HttpContext.Session.GetString("userAuthenticated") != "true")
			{
				Response.Redirect("/Login");
				return;
			}
		}
    }
}
