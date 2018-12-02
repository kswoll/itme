using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Pages
{
    public class EditLanguageModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        private readonly ItMeDb db;

        public EditLanguageModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task OnGetAsync(int? id = null)
        {
            if (id != null)
            {
                var dbLanguage = await db.Languages.SingleAsync(x => x.Id == id);
                Name = dbLanguage.Name;
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id = null)
        {
            DbLanguage dbLanguage;
            if (id == null)
            {
                var cv = await db.Cvs.SingleAsync(x => x.PersonId == User.GetId());
                dbLanguage = new DbLanguage
                {
                    Cv = cv
                };

                db.Languages.Add(dbLanguage);
            }
            else
            {
                dbLanguage = await db.Languages.SingleAsync(x => x.Id == id);
            }

            dbLanguage.Name = Name;
            await db.SaveChangesAsync();

            return RedirectToPage("cv");
        }
    }
}