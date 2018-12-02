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
    public class EditSkillModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Description { get; set; }

        private readonly ItMeDb db;

        public EditSkillModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task OnGetAsync(int? id = null)
        {
            if (id != null)
            {
                var dbSkill = await db.Skills.SingleAsync(x => x.Id == id);
                Name = dbSkill.Name;
                Description = dbSkill.Description;
            }
        }

        public async Task<IActionResult> OnPostAsync(int? id = null)
        {
            DbSkill dbSkill;
            if (id == null)
            {
                var cv = await db.Cvs.SingleAsync(x => x.PersonId == User.GetId());
                dbSkill = new DbSkill
                {
                    Cv = cv
                };

                db.Skills.Add(dbSkill);
            }
            else
            {
                dbSkill = await db.Skills.SingleAsync(x => x.Id == id);
            }

            dbSkill.Name = Name;
            dbSkill.Description = Description;
            await db.SaveChangesAsync();

            return RedirectToPage("cv");
        }
    }
}