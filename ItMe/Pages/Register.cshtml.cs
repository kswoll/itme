using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItMe.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Host { get; set; }

        [BindProperty]
        public int Port { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string InvitationCode { get; set; }

        private readonly ItMeDb db;
        private readonly AuthManager authManager;

        public RegisterModel(ItMeDb db, AuthManager authManager)
        {
            this.db = db;
            this.authManager = authManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (InvitationCode != "awerawerarhuiaewryarukalsdf")
            {
                return Unauthorized();
            }

            var dbPerson = new DbPerson
            {
                Email = Email,
                Name = Name,
                Host = Host,
                Port = Port
            };
            var dbPersonLogin = new DbPersonLogin
            {
                Person = dbPerson,
                PasswordHash = PasswordUtils.HashPassword(Password)
            };
            db.Persons.Add(dbPerson);
            db.PersonLogins.Add(dbPersonLogin);
            await db.SaveChangesAsync();

            await authManager.ProcessLogin(Name, Email, dbPerson.Id);
            return RedirectToPage("/Index");
        }
    }
}