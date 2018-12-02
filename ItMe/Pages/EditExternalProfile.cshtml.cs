using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Pages
{
    public class EditExternalProfileModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Uri { get; set; }

        private readonly ItMeDb db;

        public EditExternalProfileModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task OnGetAsync(int? id = null)
        {
            if (id != null)
            {
                var dbExternalProfile = await db.ExternalProfiles.SingleAsync(x => x.Id == id);
                Name = dbExternalProfile.Name;
                Uri = dbExternalProfile.Uri;
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id = null)
        {
            DbExternalProfile dbExternalProfile;
            if (id != null)
            {
                dbExternalProfile = await db.ExternalProfiles.SingleAsync(x => x.Id == id);
            }
            else
            {
                var cv = await db.Cvs.SingleAsync(x => x.PersonId == User.GetId());
                dbExternalProfile = new DbExternalProfile
                {
                    Cv = cv
                };
                db.ExternalProfiles.Add(dbExternalProfile);
            }

            dbExternalProfile.Name = Name;
            dbExternalProfile.Uri = Uri;
            await db.SaveChangesAsync();

            return RedirectToPage("Cv");
        }
    }
}