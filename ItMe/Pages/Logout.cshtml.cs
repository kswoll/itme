using System.Threading.Tasks;
using ItMe.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItMe.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly AuthManager authManager;

        public LogoutModel(AuthManager authManager)
        {
            this.authManager = authManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await authManager.ProcessLogout();
            return RedirectToPage("/Index");
        }
    }
}
