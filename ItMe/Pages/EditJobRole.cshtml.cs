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
    public class EditJobRoleModel : PageModel
    {
        [BindProperty]
        public string Start { get; set; }

        [BindProperty]
        public string End { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        private readonly ItMeDb db;

        public EditJobRoleModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task<IActionResult> OnGetAsync(int jobId, int? id = null)
        {
            DbJobRole dbJobRole;
            if (id == null)
            {
                dbJobRole = new DbJobRole
                {
                    JobId = jobId
                };
                db.JobRoles.Add(dbJobRole);
                await db.SaveChangesAsync();

                return RedirectToPage("EditJobRole", new { dbJobRole.Id });
            }
            else
            {
                dbJobRole = await db.JobRoles.SingleAsync(x => x.Id == id);

                Title = dbJobRole.Title;
                Description = dbJobRole.Description;
                Start = dbJobRole.Start.ToString();
                End = dbJobRole.End.ToString();

                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(int jobId, int id)
        {
            var dbJobRole = await db.JobRoles.SingleAsync(x => x.Id == id);

            dbJobRole.Title = Title;
            dbJobRole.Description = Description;
            dbJobRole.Start = PartialDate.Parse(Start);
            dbJobRole.End = PartialDate.Parse(End);

            await db.SaveChangesAsync();

            return RedirectToPage("EditJob", new { id = jobId });
        }
    }
}