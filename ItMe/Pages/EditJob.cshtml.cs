using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Models;
using ItMe.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Pages
{
    [Authorize]
    public class EditJobModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public string Company { get; set; }

        [BindProperty]
        public List<JobRole> Roles { get; set; }

        private readonly ItMeDb db;

        public EditJobModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task<IActionResult> OnGetAsync(int? id = null)
        {
            if (id != null)
            {
                var dbJob = await db.Jobs.Where(x => x.Id == id).SingleAsync();
                Id = id.Value;
                Company = dbJob.Company;
                var cv = await db.Cvs.Where(x => x.Id == dbJob.CvId).Select(Mappers.MapCv).SingleAsync();
                Roles = cv.Jobs.Single(x => x.Id == id).Roles;

                return Page();
            }
            else
            {
                var cv = await db.Cvs.SingleAsync(x => x.PersonId == User.GetId());
                var dbJob = new DbJob
                {
                    Cv = cv
                };
                db.Jobs.Add(dbJob);
                await db.SaveChangesAsync();

                return RedirectToPage("EditJob", new { dbJob.Id });
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            DbJob dbJob = await db.Jobs.Where(x => x.Id == id).SingleAsync();

            dbJob.Company = Company;

            await db.SaveChangesAsync();

            return RedirectToPage("Cv");
        }
    }
}