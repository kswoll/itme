using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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

            await authManager.ProcessLogin(personLogin.Person.Name, personLogin.Person.Email, personLogin.Person.Id);
            return RedirectToPage("/Index");
        }
    }
}
