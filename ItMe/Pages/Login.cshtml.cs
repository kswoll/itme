using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ItMe.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        private readonly ItMeDb db;
        private readonly AuthManager authManager;

        public LoginModel(ItMeDb db, AuthManager authManager)
        {
            this.db = db;
            this.authManager = authManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var personLogin = await db.PersonLogins
                .Include(x => x.Person)
                .SingleAsync(x => x.Person.Email == Email);

            if (personLogin == null || !PasswordUtils.IsValid(Password, personLogin.PasswordHash))
                return new UnauthorizedResult();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, personLogin.Person.Name),
                new Claim(ClaimTypes.Email, personLogin.Person.Email),
                new Claim(ClaimTypes.NameIdentifier, personLogin.Person.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
//            tokenManager.ProcessLogin(GenerateToken(personLogin));
            return RedirectToPage("/Index");
        }

        private string GenerateToken(DbPersonLogin personLogin)
        {
            var person = personLogin.Person;

            var featureClaims = db.Features
                .Where(x => x.PersonId == personLogin.PersonId)
                .Select(x => new Claim($"feature:{x.Type.ToString()}", "enabled"))
                .ToArray();

            var baseClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddYears(1)).ToUnixTimeSeconds().ToString())
            };

            var claims = baseClaims.Concat(featureClaims).ToArray();

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LaddlerLaddlerLaddler")), SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
