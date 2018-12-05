using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItMe.Pages
{
    [Authorize]
    public class ContactEmailModel : PageModel
    {
        public string Name { get; set; }
        public string Body { get; set; }

        public void OnGet()
        {
        }
    }
}