using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PlancherExpert.Pages.Product
{
    public class CreateModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost() {
            Console.WriteLine("Submitted");
        }
    }
}
