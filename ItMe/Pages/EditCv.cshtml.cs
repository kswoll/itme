using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Pages
{
    [Authorize]
    public class EditCvModel : PageModel
    {
        [BindProperty]
        public string Blurb { get; set; }

        private readonly ItMeDb db;

        public EditCvModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task OnGetAsync()
        {
            var cv = await db.Cvs.SingleAsync(x => x.PersonId == User.GetId());
            Blurb = cv.Blurb;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var cv = await db.Cvs.SingleAsync(x => x.PersonId == User.GetId());
            cv.Blurb = Blurb;
            await db.SaveChangesAsync();

            return RedirectToPage("Cv");
        }
    }
}