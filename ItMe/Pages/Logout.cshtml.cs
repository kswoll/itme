using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ItMe.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly TokenManager tokenManager;

        public LogoutModel(TokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }

        public async Task<IActionResult> OnGet()
        {
            tokenManager.ProcessLogout();
            return RedirectToPage("/Index");
        }
    }
}
